using AutoMapper;
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
        public class AppScreenModel : BasicModel
        {
            public virtual string Description { get; set; }
            public IList<ScreenElementModel> Elements { get; set; }
        }
    }

    namespace Entities
    {
        public class AppScreen : Basic
        {
            public virtual string Description { get; set; }

            public virtual ScreenGroup Group { get; set; }
            public virtual IList<ScreenElement> Elements { get; set; }
        }
    }

    namespace DbMappings
    {
        public class AppScreenMap : ClassMap<AppScreen>
        {
            public AppScreenMap()
            {
                Id(s => s.ID);
                Map(s => s.Name);
                Map(s => s.Description);
                Table("Screens");

                References(s => s.Group, "ScreenGroupID").Not.Insert().Not.Update();
                HasMany(s => s.Elements).KeyColumn("ScreenID").Cascade.All();
            }
        }
    }

    namespace AutoMappings
    {

        public class AppScreenMapperProfile : MappingProfileBase
        {
            public AppScreenMapperProfile()
            {
                var map = CreateMap<AppScreen, AppScreenModel>(MemberList.None)
                     .ForMember(dest => dest.Elements,
                         opts => opts.MapFrom(src => src.Elements));
            }
        }
    }
}
