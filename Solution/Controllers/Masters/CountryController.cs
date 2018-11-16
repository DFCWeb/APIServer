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
using Solution.Exceptions;
using NHibernate.Criterion;

namespace Solution.Controllers.Masters
{
    [Authorize]
    [Route("api/masterdata/[controller]")]
    [ApiController]

    public class CountryController : BaseController
    {
        private IMapper _mapper;

        public CountryController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return new ControllerHelper<Country, CountryModel>(this, _mapper).GetAll();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return new ControllerHelper<Country, CountryModel>(this, _mapper).GetById(id);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return new ControllerHelper<Country, CountryModel>(this, _mapper).Delete(id);
        }

        [HttpPost("deletemany")]
        public IActionResult Delete([FromBody]List<CountryModel> itemsDtos)
        {
            var ids = itemsDtos.Select(item => (object)item.ID);
            return new ControllerHelper<Country, CountryModel>(this, _mapper).Delete(ids);
        }

        [HttpPost]
        public IActionResult Create([FromBody]CountryModel itemDto)
        {
            return new ControllerHelper<Country, CountryModel>(this, _mapper)
                .Create(itemDto, (session, item) =>
                {
                    var exists = session.CreateCriteria<Country>()
                        .Add(Restrictions.Eq("Name", item.Name)).List<Country>().Any();
                    if (exists)
                    {
                        throw new AppException($"Country name {item.Name} already exists!");
                    }
                    TrackCreation(item);
                }, (session, item) =>
                {
                    item = session.Get<Country>(item.ID);
                    return _mapper.Map<CountryModel>(item);
                });
        }


        [HttpPut]
        public IActionResult Update([FromBody]CountryModel itemDto)
        {
            return new ControllerHelper<Country, CountryModel>(this, _mapper)
                .Update(itemDto, (session, item) =>
                {
                    var itemEx = session.Get<Country>(item.ID);
                    if (itemEx == null)
                        throw new AppException("Record not found");

                    session.Evict(itemEx);

                    var exists = session.CreateCriteria<Country>()
                                    .Add(Restrictions.Eq("Name", item.Name))
                                    .Add(!Restrictions.Eq("ID", item.ID))
                                    .List<Country>().Any();
                    if (exists)
                    {
                        throw new AppException($"Country name {item.Name} already exists!");
                    }
                    TrackModification(item);
                }, (session, item) =>
                {
                    item = session.Get<Country>(item.ID);
                    return _mapper.Map<CountryModel>(item);
                });
        }

    }

}
