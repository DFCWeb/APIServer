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

    public class ScreenController : BaseController
    {
        private IMapper _mapper;

        public ScreenController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return new ControllerHelper<AppScreen, AppScreenModel>(this, _mapper).GetAll();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return new ControllerHelper<AppScreen, AppScreenModel>(this, _mapper).GetById(id);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return new ControllerHelper<AppScreen, AppScreenModel>(this, _mapper).Delete(id);
        }

        [HttpPost("deletemany")]
        public IActionResult Delete([FromBody]List<AppScreenModel> itemsDtos)
        {
            var ids = itemsDtos.Select(item => (object)item.ID);
            return new ControllerHelper<AppScreen, AppScreenModel>(this, _mapper).Delete(ids);
        }
        /*
        [HttpPost]
        public IActionResult Create([FromBody]AppScreenModel itemDto)
        {
            return new ControllerHelper<AppScreen, AppScreenModel>(this, _mapper)
                .Create(itemDto, (session, item) =>
                {
                    var exists = session.CreateCriteria<AppScreen>()
                        .Add(Restrictions.Eq("Name", item.Name)).List<Screen>().Any();
                    if (exists)
                    {
                        throw new AppException($"Screen name {item.Name} already exists!");
                    }
                    TrackCreation(item);
                }, (session, item) =>
                {
                    item = session.Get<Screen>(item.ID);
                    return _mapper.Map<AppScreenModel>(item);
                });
        }


        [HttpPut]
        public IActionResult Update([FromBody]AppScreenModel itemDto)
        {
            return new ControllerHelper<AppScreen, AppScreenModel>(this, _mapper)
                .Update(itemDto, (session, item) =>
                {
                    var itemEx = session.Get<Screen>(item.ID);
                    if (itemEx == null)
                        throw new AppException("Record not found");

                    session.Evict(itemEx);

                    var exists = session.CreateCriteria<AppScreen>()
                                    .Add(Restrictions.Eq("Name", item.Name))
                                    .Add(!Restrictions.Eq("ID", item.ID))
                                    .List<Screen>().Any();
                    if (exists)
                    {
                        throw new AppException($"Screen name {item.Name} already exists!");
                    }
                    TrackModification(item);
                }, (session, item) =>
                {
                    item = session.Get<AppScreen>(item.ID);
                    return _mapper.Map<AppScreenModel>(item);
                });
        }
        */
    }

}
