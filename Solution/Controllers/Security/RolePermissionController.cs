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

    public class RolePermissionController : BaseController
    {
        private IMapper _mapper;

        public RolePermissionController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return new ControllerHelper<RolePermission, RolePermissionModel>(this, _mapper).GetAll();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return new ControllerHelper<RolePermission, RolePermissionModel>(this, _mapper).GetById(id);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return new ControllerHelper<RolePermission, RolePermissionModel>(this, _mapper).Delete(id);
        }

        [HttpPost("deletemany")]
        public IActionResult Delete([FromBody]List<RolePermissionModel> itemsDtos)
        {
            var ids = itemsDtos.Select(item => (object)item.ID);
            return new ControllerHelper<RolePermission, RolePermissionModel>(this, _mapper).Delete(ids);
        }
        [HttpPost]
        public IActionResult Create([FromBody]RolePermissionModel itemDto)
        {
            return new ControllerHelper<RolePermission, RolePermissionModel>(this, _mapper)
                .Create(itemDto, (session, item) =>
                {
                    var exists = session.CreateCriteria<RolePermission>()
                        .Add(Restrictions.Eq("Name", item.Name)).List<RolePermission>().Any();
                    if (exists)
                    {
                        throw new AppException($"RolePermission name {item.Name} already exists!");
                    }
                    TrackCreation(item);
                }, (session, item) =>
                {
                    item = session.Get<RolePermission>(item.ID);
                    return _mapper.Map<RolePermissionModel>(item);
                });
        }


        [HttpPut]
        public IActionResult Update([FromBody]RolePermissionModel itemDto)
        {
            return new ControllerHelper<RolePermission, RolePermissionModel>(this, _mapper)
                .Update(itemDto, (session, item) =>
                {
                    var itemEx = session.Get<RolePermission>(item.ID);
                    if (itemEx == null)
                        throw new AppException("Record not found");

                    session.Evict(itemEx);

                    var exists = session.CreateCriteria<RolePermission>()
                                    .Add(Restrictions.Eq("Name", item.Name))
                                    .Add(!Restrictions.Eq("ID", item.ID))
                                    .List<RolePermission>().Any();
                    if (exists)
                    {
                        throw new AppException($"RolePermission name {item.Name} already exists!");
                    }
                    TrackModification(item);
                }, (session, item) =>
                {
                    item = session.Get<RolePermission>(item.ID);
                    return _mapper.Map<RolePermissionModel>(item);
                });
        }
    }

}
