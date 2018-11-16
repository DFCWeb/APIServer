using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Solution.Services;
using Solution.DataDefinitions.Entities;
using Solution.DataDefinitions.Models;

namespace Solution.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]UserModel userDto)
        {
            var user = _userService.Authenticate(userDto.UserName, userDto.UserPassword);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(ConfigurationManager.Setting["AppSettings:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.ID.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);


            userDto = _mapper.Map<UserModel>(user);
            userDto.Mobile = user.Detail.Mobile;
            userDto.AlternateMobile = user.Detail.AlternateMobile;
            userDto.EMail = user.Detail.EMail;
            userDto.HomePhone = user.Detail.HomePhone;
            userDto.Address1 = user.Detail.Address1;
            userDto.Address2 = user.Detail.Address2;
            userDto.DistrictID = user.Detail.DistrictID;
            userDto.Token = tokenString;

            return Ok(userDto);
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            var userDtos = _mapper.Map<IList<UserModel>>(users);
            return Ok(userDtos);
        }

        [Authorize]
        [HttpPost("create")]
        public IActionResult Create([FromBody]UserModel userDto)
        {
            // map dto to entity
            var user = _mapper.Map<User>(userDto);
            user.Detail = _mapper.Map<UserDetail>(userDto);
            try
            {
                // save 
                _userService.Create(user, userDto.UserPassword);
                return Ok();
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userService.GetById(id);
            var userDto = _mapper.Map<UserModel>(user);
            userDto.Mobile = user.Detail.Mobile;
            userDto.AlternateMobile = user.Detail.AlternateMobile;
            userDto.EMail = user.Detail.EMail;
            userDto.HomePhone = user.Detail.HomePhone;
            return Ok(userDto);
        }

        [Authorize]
        [HttpPut]
        public IActionResult Update([FromBody]UserModel userDto)
        {
            // map dto to entity
            var user = _mapper.Map<User>(userDto);
            user.Detail = _mapper.Map<UserDetail>(userDto);
            try
            {
                // save 
                _userService.Update(user, userDto.UserPassword);
                return Ok();
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok();
        }
    }
}