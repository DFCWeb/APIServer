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
using Solution.Helpers;
using NHibernate.Criterion;
using Solution.Exceptions;

namespace Solution.Controllers.Masters
{
    [Authorize]
    [Route("api/masterdata/[controller]")]
    [ApiController]
    public class CityController : BaseController
    {
        private IMapper _mapper;

        public CityController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return  new ControllerHelper<City, CityModel>(this, _mapper).GetAll();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return new ControllerHelper<City, CityModel>(this, _mapper).GetById(id);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return new ControllerHelper<City, CityModel>(this, _mapper).Delete(id);
        }

        [HttpPost("deletemany")]
        public IActionResult Delete([FromBody]List<CityModel> itemsDtos)
        {
            var ids = itemsDtos.Select(item => (object)item.ID);
            return new ControllerHelper<City, CityModel>(this, _mapper).Delete(ids);
        }

        [HttpPost]
        public IActionResult Create([FromBody]CityModel itemDto)
        {
            return new ControllerHelper<City, CityModel>(this, _mapper)
                .Create(itemDto, (session, item) =>
                {
                    var exists = session.CreateCriteria<City>()
                        .Add(Restrictions.Eq("Name", item.Name)).List<City>().Any();
                    if (exists)
                    {
                        throw new AppException($"City name {item.Name} already exists!");
                    }
                    TrackCreation(item);
                }, (session, item) =>
                {
                    item = session.Get<City>(item.ID);
                    return _mapper.Map<CityModel>(item);
                });
        }


        [HttpPut]
        public IActionResult Update([FromBody]CityModel itemDto)
        {
            return new ControllerHelper<City, CityModel>(this, _mapper)
                .Update(itemDto, (session, item) =>
                {
                    var itemEx = session.Get<City>(item.ID);
                    if (itemEx == null)
                        throw new AppException("Record not found");

                    session.Evict(itemEx);

                    var exists = session.CreateCriteria<City>()
                                    .Add(Restrictions.Eq("Name", item.Name))
                                    .Add(!Restrictions.Eq("ID", item.ID))
                                    .List<City>().Any();
                    if (exists)
                    {
                        throw new AppException($"City name {item.Name} already exists!");
                    }
                    TrackModification(item);
                }, (session, item) =>
                {
                    item = session.Get<City>(item.ID);
                    return _mapper.Map<CityModel>(item);
                });
        }

    }
}
