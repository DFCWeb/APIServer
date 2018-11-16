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
        public class ScreenElementModel : BasicModel
        {
            public virtual string ControlID { get; set; }
            public virtual string Description { get; set; }
        }
    }

    namespace Entities
    {
        public class ScreenElement : Basic
        {
            public virtual string Description { get; set; }
            public virtual string ControlID { get; set; }
            public virtual AppScreen Screen { get; set; }
        }
    }

    namespace DbMappings
    {
        public class ScreenElementMap : ClassMap<ScreenElement>
        {
            public ScreenElementMap()
            {
                Id(s => s.ID);
                Map(s => s.ControlID);
                Map(s => s.Name);
                Map(s => s.Description);
                Table("ScreenElements");
                References(s => s.Screen, "ScreenID").Not.Insert().Not.Update();
            }
        }
    }

    namespace AutoMappings
    {

        public class ScreenElementMapperProfile : MappingProfileBase
        {
            public ScreenElementMapperProfile()
            {
                CreateBidirectionalMap<ScreenElement, ScreenElementModel>();
            }
        }
    }
}
