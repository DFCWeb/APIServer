using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NHibernate.Criterion;
using Solution.DataDefinitions.Entities;
using Solution.DataDefinitions.Models;
using Solution.Exceptions;
using Solution.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solution.Controllers
{
    public delegate IEnumerable<TModel> SearchActionHandler<TEntity, TModel>(NHibernate.ISession session) where TEntity : class where TModel : class;

    public delegate void ActionHandler<TEntity>(NHibernate.ISession session, TEntity item) where TEntity : class;
    public delegate TModel PostActionHandler<TEntity, TModel>(NHibernate.ISession session, TEntity item) where TEntity : class;

    public class ControllerHelper<TEntity, TModel> where TEntity : class where TModel : class
    {
        public IMapper _mapper { get; set; }
        public ControllerBase _source { get; set; }

        public ControllerHelper(ControllerBase source, IMapper mapper)
        {
            _source = source;
            _mapper = mapper;
        }

        public IActionResult GetAll()
        {
            using (var session = SessionFactory.Instance.OpenSession())
            {
                var items = session.CreateCriteria<TEntity>().List();
                return _source.Ok(_mapper.Map<IList<TModel>>(items));
            }
        }

        public IActionResult Search(SearchActionHandler<TEntity, TModel> actionHandler)
        {
            using (var session = SessionFactory.Instance.OpenSession())
            {
                return _source.Ok(actionHandler(session));
            }
        }


        public IActionResult GetById(int id)
        {
            using (var session = SessionFactory.Instance.OpenSession())
            {
                var item = session.Get<TEntity>(id);
                return _source.Ok(_mapper.Map<TModel>(item));
            }
        }


        public IActionResult Create(TModel itemDto, ActionHandler<TEntity> actionHandler, PostActionHandler<TEntity, TModel> postActionHandler)
        {
            var item = _mapper.Map<TEntity>(itemDto);
            using (var session = SessionFactory.Instance.OpenSession())
            {
                actionHandler(session, item);

                SaveAndRetrieve(session, item);

                session.Refresh(item);

                return _source.Ok(postActionHandler(session, item));
            }
        }


        public IActionResult Update(TModel itemDto, ActionHandler<TEntity> actionHandler, PostActionHandler<TEntity, TModel> postActionHandler)
        {
            var item = _mapper.Map<TEntity>(itemDto);
            using (var session = SessionFactory.Instance.OpenSession())
            {
                actionHandler(session, item);

                SaveAndRetrieve(session, item);

                session.Refresh(item);

                return _source.Ok(postActionHandler(session, item));
            }
        }

        private void SaveAndRetrieve(NHibernate.ISession session, TEntity item)
        {
            using (var transaction = session.BeginTransaction())
            {
                session.SaveOrUpdate(item);
                
                //session.Flush();
                transaction.Commit();
            }
        }

        public IActionResult Delete(int id)
        {
            using (var session = SessionFactory.Instance.OpenSession())
            {
                var item = session.Get<TEntity>(id);
                if (item == null)
                    throw new AppException("Record not found");

                using (var transaction = session.BeginTransaction())
                {
                    session.Delete(item);
                    transaction.Commit();
                }
            }
            return _source.Ok();
        }

        public IActionResult Delete(IEnumerable<object> ids)
        {
            using (var session = SessionFactory.Instance.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    foreach (object id in ids)
                    {
                        var item = session.Get<TEntity>(id);
                        if (item != null)
                        {
                            session.Delete(item);
                        }
                    }
                    transaction.Commit();
                }
            }
            return _source.Ok();
        }
    }
}
