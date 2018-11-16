using FluentNHibernate.Mapping;
using Solution.DataDefinitions.Entities;
using Solution.DataDefinitions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solution.DataDefinitions
{
    namespace Models
    {
        public class CountryModel : BasicModel
        {
        }
    }

    namespace Entities
    {
        public class Country : Basic
        {
        }
    }

    namespace DbMappings
    {
        public class CountryMap : ClassMap<Country>
        {
            public CountryMap()
            {
                Id(s => s.ID);
                Map(s => s.Name);
                Table("Countries");

                Map(s => s.CreatedDateTime).Insert().Not.Update();
                Map(s => s.UpdatedDateTime).Not.Insert();
                References(s => s.CreatedBy, "CreatedByUserID").Insert().Not.Update();
                References(s => s.UpdatedBy, "UpdatedByUserID").Not.Insert();
            }
        }
    }

    namespace AutoMappings
    {

    public class CountryMapperProfile : MappingProfileBase
    {
        public CountryMapperProfile()
        {
            CreateBidirectionalMap<CountryModel, Country>(true);
        }
    }
    }


}
