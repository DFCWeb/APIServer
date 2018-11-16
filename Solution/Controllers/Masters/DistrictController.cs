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

namespace Solution.Controllers.Masters
{
    [Authorize]
    [Route("api/masterdata/[controller]")]
    [ApiController]

    public class DistrictController : BaseController
    {
        private IMapper _mapper;

        public DistrictController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return new ControllerHelper<District, DistrictModel>(this, _mapper).GetAll();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return new ControllerHelper<District, DistrictModel>(this, _mapper).GetById(id);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return new ControllerHelper<District, DistrictModel>(this, _mapper).Delete(id);
        }

        [HttpPost("deletemany")]
        public IActionResult Delete([FromBody]List<DistrictModel> itemsDtos)
        {
            var ids = itemsDtos.Select(item => (object)item.ID);
            return new ControllerHelper<District, DistrictModel>(this, _mapper).Delete(ids);
        }

        [HttpPost]
        public IActionResult Create([FromBody]DistrictModel itemDto)
        {
            return new ControllerHelper<District, DistrictModel>(this, _mapper)
                .Create(itemDto, (session, item) =>
                {
                    var exists = session.CreateCriteria<District>()
                        .Add(Restrictions.Eq("Name", item.Name)).List<District>().Any();
                    if (exists)
                    {
                        throw new AppException($"District name {item.Name} already exists!");
                    }
                    TrackCreation(item);
                }, (session, item) =>
                {
                    item = session.Get<District>(item.ID);
                    return _mapper.Map<DistrictModel>(item);
                });
        }


        [HttpPut]
        public IActionResult Update([FromBody]DistrictModel itemDto)
        {
            return new ControllerHelper<District, DistrictModel>(this, _mapper)
                .Update(itemDto, (session, item) =>
                {
                    var itemEx = session.Get<District>(item.ID);
                    if (itemEx == null)
                        throw new AppException("Record not found");

                    session.Evict(itemEx);

                    var exists = session.CreateCriteria<District>()
                                    .Add(Restrictions.Eq("Name", item.Name))
                                    .Add(!Restrictions.Eq("ID", item.ID))
                                    .List<District>().Any();
                    if (exists)
                    {
                        throw new AppException($"District name {item.Name} already exists!");
                    }
                    TrackModification(item);
                }, (session, item) =>
                {
                    item = session.Get<District>(item.ID);
                    return _mapper.Map<DistrictModel>(item);
                });
        }

    }

}
