using AutoMapper;
using Solution.DataDefinitions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solution.DataDefinitions
{
    public class TrackableEntity
    {
        public virtual bool Active { get; set; }

        public virtual User CreatedBy { get; set; }
        public virtual int? CreatedByUserID { get; set; }
        public virtual DateTime? CreatedDateTime { get; set; }


        public virtual User UpdatedBy { get; set; }
        public virtual int? UpdatedByUserID { get; set; }
        public virtual DateTime? UpdatedDateTime { get; set; }

    }

    public class Basic : TrackableEntity
    {
        public virtual int ID { get; set; }
        public virtual string Name { get; set; }
    }

    public class BasicModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }

    }

    public class MappingProfileBase : Profile
    {
        protected void CreateBidirectionalMap<TSource, TDistination>(bool mapToBasic = false)
        {
            CreateMap<TSource, TDistination>();
            CreateMap<TSource, TDistination>().ReverseMap();
            if (mapToBasic)
            {
                CreateBidirectionalMap<TSource, BasicModel>();
            }
        }
    }


}
