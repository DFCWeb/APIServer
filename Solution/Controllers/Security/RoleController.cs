using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Solution.Services.MasterData;
using Common;
using Solution.DataDefinitions.Entities;
using Solution.DataDefinitions.Models;
using NHibernate.Criterion;
using Solution.Exceptions;

namespace Solution.Controllers.Security
{
    [Authorize]
    [Route("api/security/[controller]")]
    [ApiController]

    public class RoleController : BaseController
    {
        private IMapper _mapper;

        public RoleController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return new ControllerHelper<Role, RoleModel>(this, _mapper).GetAll();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return new ControllerHelper<Role, RoleModel>(this, _mapper).GetById(id);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return new ControllerHelper<Role, RoleModel>(this, _mapper).Delete(id);
        }

        [HttpPost("deletemany")]
        public IActionResult Delete([FromBody]List<RoleModel> itemsDtos)
        {
            var ids = itemsDtos.Select(item => (object)item.ID);
            return new ControllerHelper<Role, RoleModel>(this, _mapper).Delete(ids);
        }
        [HttpPost]
        public IActionResult Create([FromBody]RoleModel itemDto)
        {
            return new ControllerHelper<Role, RoleModel>(this, _mapper)
                .Create(itemDto, (session, item) =>
                {
                    var exists = session.CreateCriteria<Role>()
                        .Add(Restrictions.Eq("Name", item.Name)).List<Role>().Any();
                    if (exists)
                    {
                        throw new AppException($"Role name {item.Name} already exists!");
                    }
                    TrackCreation(item);
                }, (session, item) =>
                {
                    item = session.Get<Role>(item.ID);
                    return _mapper.Map<RoleModel>(item);
                });
        }


        [HttpPut]
        public IActionResult Update([FromBody]RoleModel itemDto)
        {
            return new ControllerHelper<Role, RoleModel>(this, _mapper)
                .Update(itemDto, (session, item) =>
                {
                    var itemEx = session.Get<Role>(item.ID);
                    if (itemEx == null)
                        throw new AppException("Record not found");

                    session.Evict(itemEx);

                    var exists = session.CreateCriteria<Role>()
                                    .Add(Restrictions.Eq("Name", item.Name))
                                    .Add(!Restrictions.Eq("ID", item.ID))
                                    .List<Role>().Any();
                    if (exists)
                    {
                        throw new AppException($"Role name {item.Name} already exists!");
                    }
                    TrackModification(item);
                }, (session, item) =>
                {
                    item = session.Get<Role>(item.ID);
                    return _mapper.Map<RoleModel>(item);
                });
        }
    }

}
