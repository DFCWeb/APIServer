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
        public class ScreenGroupModel : BasicModel
        {
            public virtual string Description { get; set; }
            public IList<AppScreenModel> Screens { get; set; }
        }
    }

    namespace Entities
    {
        public class ScreenGroup : Basic
        {
            public virtual string Description { get; set; }

            public virtual IList<AppScreen> Screens { get; set; }
        }
    }

    namespace DbMappings
    {
        public class ScreenGroupMap : ClassMap<ScreenGroup>
        {
            public ScreenGroupMap()
            {
                Id(s => s.ID);
                Map(s => s.Name);
                Map(s => s.Description);
                Table("ScreenGroups");

                HasMany(s => s.Screens)
                    .KeyColumn("ScreenGroupID")
                    .Cascade.All();
            }
        }
    }

    namespace AutoMappings
    {

        public class ScreenGroupMapperProfile : MappingProfileBase
        {
            public ScreenGroupMapperProfile()
            {
                var map = CreateMap<ScreenGroup, ScreenGroupModel>(MemberList.None)
                     .ForMember(dest => dest.Screens,
                         opts => opts.MapFrom(src => src.Screens));
            }
        }
    }
}
