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
        public class StateModel : BasicModel
        {
            public int CountryID { get; set; }
            public string CountryName { get; set; }
        }
    }

    namespace Entities
    {
        public class State : Basic
        {
            public virtual int CountryID { get; set; }
            public virtual Country Country { get; set; }

        }
    }

    namespace DbMappings
    {
        public class StateMap : ClassMap<State>
        {
            public StateMap()
            {
                Id(s => s.ID);
                Map(s => s.Name);
                Table("States");

                References(s => s.Country, "CountryID");
                Map(s => s.CreatedDateTime).Insert().Not.Update();
                Map(s => s.UpdatedDateTime).Not.Insert();
                References(s => s.CreatedBy, "CreatedByUserID").Insert().Not.Update();
                References(s => s.UpdatedBy, "UpdatedByUserID").Not.Insert();
            }
        }
    }

    namespace AutoMappings
    {
        public class StateMapperProfile : MappingProfileBase
        {
            public StateMapperProfile()
            {
                var map1 = CreateMap<State, StateModel>(MemberList.None)
                    .ForMember(dest => dest.CountryID,
                        opts => opts.MapFrom(src => src.Country.ID))
                    .ForMember(dest => dest.CountryName,
                        opts => opts.MapFrom(src => src.Country.Name));
                map1.ReverseMap();
            }
        }
    }

}
