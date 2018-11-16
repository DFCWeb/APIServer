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
    public class StateController : BaseController
    {
        private IMapper _mapper;

        public StateController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return new ControllerHelper<State, StateModel>(this, _mapper).GetAll();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return new ControllerHelper<State, StateModel>(this, _mapper).GetById(id);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return new ControllerHelper<State, StateModel>(this, _mapper).Delete(id);
        }

        [HttpPost("deletemany")]
        public IActionResult Delete([FromBody]List<StateModel> itemsDtos)
        {
            var ids = itemsDtos.Select(item => (object)item.ID);
            return new ControllerHelper<State, StateModel>(this, _mapper).Delete(ids);
        }

        [HttpPost]
        public IActionResult Create([FromBody]StateModel itemDto)
        {
            return new ControllerHelper<State, StateModel>(this, _mapper)
                .Create(itemDto, (session, item) =>
                {
                    var exists = session.CreateCriteria<State>()
                        .Add(Restrictions.Eq("Name", item.Name)).List<State>().Any();
                    if (exists)
                    {
                        throw new AppException($"State name {item.Name} already exists!");
                    }
                    TrackCreation(item);
                }, (session, item) =>
                {
                    item = session.Get<State>(item.ID);
                    return _mapper.Map<StateModel>(item);
                });
        }


        [HttpPut]
        public IActionResult Update([FromBody]StateModel itemDto)
        {
            return new ControllerHelper<State, StateModel>(this, _mapper)
                .Update(itemDto, (session, item) =>
                {
                    var itemEx = session.Get<State>(item.ID);
                    if (itemEx == null)
                        throw new AppException("Record not found");

                    session.Evict(itemEx);

                    var exists = session.CreateCriteria<State>()
                                    .Add(Restrictions.Eq("Name", item.Name))
                                    .Add(!Restrictions.Eq("ID", item.ID))
                                    .List<State>().Any();
                    if (exists)
                    {
                        throw new AppException($"State name {item.Name} already exists!");
                    }
                    TrackModification(item);
                }, (session, item) =>
                {
                    item = session.Get<State>(item.ID);
                    return _mapper.Map<StateModel>(item);
                });
        }

    }
}