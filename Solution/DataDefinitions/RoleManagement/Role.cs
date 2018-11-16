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
        public class RoleModel : BasicModel
        {
            public virtual string Description { get; set; }
        }
    }

    namespace Entities
    {
        public class Role : Basic
        {
            public virtual string Description { get; set; }
        }
    }

    namespace DbMappings
    {
        public class RoleMap : ClassMap<Role>
        {
            public RoleMap()
            {
                Id(s => s.ID);
                Map(s => s.Name);
                Map(s => s.Description);
                Map(s => s.Active);
                Table("Roles");
            }
        }
    }

    namespace AutoMappings
    {

        public class RoleMapperProfile : MappingProfileBase
        {
            public RoleMapperProfile()
            {
                CreateBidirectionalMap<Role, RoleModel>();
            }
        }
    }
}
