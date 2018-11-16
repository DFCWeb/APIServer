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
        public class DistrictModel : BasicModel
        {
            public int StateID { get; set; }
            public string StateName { get; set; }
        }
    }

    namespace Entities
    {
        public class District : Basic
        {
            public virtual int StateID { get; set; }
            public virtual State State { get; set; }
        }
    }

    namespace DbMappings
    {
        public class DistrictMap : ClassMap<District>
        {
            public DistrictMap()
            {
                Id(s => s.ID);
                Map(s => s.Name);
                Table("Districts");

                References(s => s.State, "StateID");
                Map(s => s.CreatedDateTime).Insert().Not.Update();
                Map(s => s.UpdatedDateTime).Not.Insert();
                References(s => s.CreatedBy, "CreatedByUserID").Insert().Not.Update();
                References(s => s.UpdatedBy, "UpdatedByUserID").Not.Insert();
            }
        }

    }

    namespace AutoMappings
    {
        public class DistrictMapperProfile : MappingProfileBase
        {
            public DistrictMapperProfile()
            {
                var map2 = CreateMap<District, DistrictModel>(MemberList.None)
                    .ForMember(dest => dest.StateID,
                        opts => opts.MapFrom(src => src.State.ID))
                    .ForMember(dest => dest.StateName,
                        opts => opts.MapFrom(src => src.State.Name));
                map2.ReverseMap();
            }
        }
    }



}
