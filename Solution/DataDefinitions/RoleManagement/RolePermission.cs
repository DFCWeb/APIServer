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
        public class RolePermissionModel : BasicModel
        {
            public virtual int GroupID { get; set; }
            public virtual string GroupName { get; set; }
            public virtual int ScreenID { get; set; }
            public virtual string ScreenName { get; set; }
            public virtual int? ElementID { get; set; }
            public virtual string ElementName { get; set; }
            public virtual bool Allowed { get; set; }
        }
    }

    namespace Entities
    {
        public class RolePermission : Basic
        {
            public virtual Role Role { get; set; }
            public virtual ScreenGroup Group { get; set; }
            public virtual AppScreen Screen { get; set; }
            public virtual ScreenElement Element { get; set; }
            public virtual bool Allowed { get; set; }
        }
    }

    namespace DbMappings
    {
        public class RolePermissionMap : ClassMap<RolePermission>
        {
            public RolePermissionMap()
            {
                Id(s => s.ID);
                Map(s => s.Allowed);
                Table("RolePermissions");

                References(s => s.Role, "RoleID");
                References(s => s.Group, "ScreenGroupID");
                References(s => s.Screen, "ScreenID");
                References(s => s.Element, "ScreenElementID");
            }
        }
    }

    namespace AutoMappings
    {

        public class RolePermissionMapperProfile : MappingProfileBase
        {
            public RolePermissionMapperProfile()
            {
                var map = CreateMap<RolePermission, RolePermissionModel>(MemberList.None)
                  .ForMember(dest => dest.GroupID,
                      opts => opts.MapFrom(src => src.Group.ID))
                  .ForMember(dest => dest.GroupName,
                      opts => opts.MapFrom(src => src.Group.Name))
                  .ForMember(dest => dest.ScreenID,
                      opts => opts.MapFrom(src => src.Screen.ID))
                  .ForMember(dest => dest.ScreenName,
                      opts => opts.MapFrom(src => src.Screen.Name))
                  .ForMember(dest => dest.ElementID,
                      opts => opts.MapFrom(src => src.Element.ID))
                  .ForMember(dest => dest.ElementName,
                      opts => opts.MapFrom(src => src.Element.Name));
                map.ReverseMap();

            }
        }
    }
}
