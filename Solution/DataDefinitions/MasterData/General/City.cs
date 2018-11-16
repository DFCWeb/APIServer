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
        public class CityModel : BasicModel
        {
            public string CityType { get; set; }
            public int DistrictID { get; set; }
            public string DistrictName { get; set; }
            public string StateName { get; set; }
        }
    }

    namespace Entities
    {
        public class City : Basic
        {
            public virtual string CityType { get; set; }
            public virtual int DistrictID { get; set; }
            public virtual District District { get; set; }
        }
    }

    namespace DbMappings
    {
        public class CityMap : ClassMap<City>
        {
            public CityMap()
            {
                Id(s => s.ID);
                Map(s => s.Name);
                Map(s => s.CityType);
                Map(s => s.Active);
                Table("Cities");
                References(s => s.District, "DistrictID");

                Map(s => s.CreatedDateTime).Insert().Not.Update();
                Map(s => s.UpdatedDateTime).Not.Insert();
                References(s => s.CreatedBy, "CreatedByUserID").Insert().Not.Update();
                References(s => s.UpdatedBy, "UpdatedByUserID").Not.Insert();
            }
        }
    }

    namespace AutoMappings
    {
        public class CityMapperProfile : MappingProfileBase
        {
            public CityMapperProfile()
            {
                var map3 = CreateMap<City, CityModel>(MemberList.None)
                    .ForMember(dest => dest.DistrictID,
                        opts => opts.MapFrom(src => src.District.ID))
                    .ForMember(dest => dest.DistrictName,
                        opts => opts.MapFrom(src => src.District.Name))
                    .ForMember(dest => dest.StateName,
                        opts => opts.MapFrom(src => src.District.State.Name));
                map3.ReverseMap();
            }
        }
    }



}
