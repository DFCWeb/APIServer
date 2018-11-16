using NHibernate;
using NHibernate.Criterion;
using Solution.DataDefinitions;
using Solution.Exceptions;
using Solution.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solution.Services
{
    public interface IDataService<T>
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> Search();
        T GetById(object id);
        T Create(T item);
        T Update(object id, T item);
        void Delete(object id);
        void DeleteMany(IEnumerable<object> ids);
    }

    public class DataService<T> : IDataService<T> where T : class
    {
        public T Create(T item)
        {
            using (var session = SessionFactory.Instance.OpenSession())
            {
                session.CacheMode = CacheMode.Refresh;
                ValidateBeforeCreate(session, item);
                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(item);
                    transaction.Commit();
                    return item;
                }

            }
        }

        public virtual void ValidateBeforeCreate(ISession session, T item)
        {

        }
        public void Delete(object id)
        {
            using (var session = SessionFactory.Instance.OpenSession())
            {
                var item = session.Get<T>(id);
                if (item == null)
                    throw new AppException("Record not found");

                using (var transaction = session.BeginTransaction())
                {
                    session.Delete(item);
                    transaction.Commit();
                }
            }
        }

        public void DeleteMany(IEnumerable<object> ids)
        {
            using (var session = SessionFactory.Instance.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    foreach (object id in ids)
                    {
                        var item = session.Get<T>(id);
                        session.Delete(item);
                    }
                    transaction.Commit();
                }
            }
        }


        public IEnumerable<T> GetAll()
        {
            using (var session = SessionFactory.Instance.OpenSession())
            {
                var criteria = BuildCriteriaToGetAll(session);
                var items = criteria.List<T>();
                return items;
            }
        }
        public virtual ICriteria BuildCriteriaToGetAll(ISession session)
        {
            return session.CreateCriteria<T>();
        }

        public IEnumerable<T> Search()
        {
            using (var session = SessionFactory.Instance.OpenSession())
            {
                var criteria = BuildCriteriaToSearch(session);
                var items = criteria.List<T>();
                return items;
            }
        }

        public virtual ICriteria BuildCriteriaToSearch(ISession session)
        {
            return session.CreateCriteria<T>();
        }

        public T GetById(object id)
        {
            using (var session = SessionFactory.Instance.OpenSession())
            {
                var item = session.Get<T>(id);
                NHibernateUtil.Initialize(item);
                return item;
            }
        }

        public T Update(object id, T item)
        {
            using (var session = SessionFactory.Instance.OpenSession())
            {
                session.CacheMode = CacheMode.Refresh;
                var itemEx = session.Get<T>(id);
                if (itemEx == null)
                    throw new AppException("Record not found");

                session.Evict(itemEx);

                ValidateBeforeUpdate(session, item, itemEx);

                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(item);
                    transaction.Commit();

                    return item;
                }
            }
        }

        public virtual void ValidateBeforeUpdate(ISession session, T objectToUpdate, T existingObject)
        {

        }

    }
}
