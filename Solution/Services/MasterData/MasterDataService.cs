using NHibernate;
using NHibernate.Criterion;
using Solution.DataDefinitions;
using Solution.Exceptions;
using Solution.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solution.Services.MasterData
{
    public interface IMasterDataService<T>
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(params string[] subEntities);

        IEnumerable<T> Search(List<SearchInput> searchParameters);

        T GetById(object id);
        void Delete(object id);
        void DeleteMany(IEnumerable<object> ids);

        T Create(T item, string uniqueField, string uniqueFieldValue);
        T Update(T item, object id, string uniqueField, string uniqueFieldValue);
    }


    public class MasterDataService<T> : DataService<T>, IMasterDataService<T> where T : class
    {
        string uniqueField, uniqueFieldValue;
        string[] subEntities;
        List<SearchInput> searchInputs;

        public IEnumerable<T> GetAll(params string[] subEntities)
        {
            this.subEntities = subEntities;
            return base.GetAll();
        }
        public override ICriteria BuildCriteriaToGetAll(ISession session)
        {
            var criteria = base.BuildCriteriaToGetAll(session);
            if (subEntities != null)
            {
                foreach (var entity in subEntities)
                {
                    criteria.SetFetchMode(entity, FetchMode.Eager);
                }
            }
            return criteria;
        }

        public IEnumerable<T> Search(List<SearchInput> searchInputs)
        {
            this.searchInputs = searchInputs;
            return base.Search();
        }

        public override ICriteria BuildCriteriaToSearch(ISession session)
        {
            var criteria = base.BuildCriteriaToGetAll(session);
            if (searchInputs != null)
            {
                foreach (var input in searchInputs)
                {
                    var subCriteria = criteria.CreateCriteria(input.Entity);
                    foreach (var param in input.Parameters)
                    {
                        subCriteria.Add(Restrictions.Eq(param.Name, param.Value));
                    }
                }
            }
            return criteria;
        }


        public T Create(T item, string uniqueField, string uniqueFieldValue)
        {
            this.uniqueField = uniqueField;
            this.uniqueFieldValue = uniqueFieldValue;
            return base.Create(item);
        }

        public override void ValidateBeforeCreate(ISession session, T item)
        {
            if (session.CreateCriteria<T>()
                .Add(Restrictions.Eq(uniqueField, uniqueFieldValue))
                .List<T>()
                .Any())
            {
                throw new AppException($"Record already exists!");
            }
        }


        public T Update(T item, object id, string uniqueField, string uniqueFieldValue)
        {
            this.uniqueField = uniqueField;
            this.uniqueFieldValue = uniqueFieldValue;
            return base.Update(id, item);
        }
        public override void ValidateBeforeUpdate(ISession session, T objectToUpdate, T existingObject)
        {
            if (objectToUpdate is TrackableEntity && existingObject is TrackableEntity)
            {
                var destination = objectToUpdate as TrackableEntity;
                var source = existingObject as TrackableEntity;

                destination.CreatedBy = source.CreatedBy;
                destination.CreatedDateTime = source.CreatedDateTime;
            }
        }
    }
}
