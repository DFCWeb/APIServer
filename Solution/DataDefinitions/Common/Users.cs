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
        public class UserModel
        {
            public int ID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string UserName { get; set; }
            public string EMail { get; set; }
            public string Mobile { get; set; }
            public string AlternateMobile { get; set; }
            public string HomePhone { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public int DistrictID { get; set; }

            public bool Active { get; set; }
            public string UserPassword { get; set; }

            public string Token { get; set; }
        }
    }

    namespace Entities
    {
        public class User
        {
            public virtual int ID { get; set; }
            public virtual string FirstName { get; set; }
            public virtual string LastName { get; set; }
            public virtual string UserName { get; set; }
            public virtual bool Active { get; set; }

            public virtual int DetailID { get; set; }
            public virtual int PasswordID { get; set; }
            public virtual UserDetail Detail { get; set; }
            public virtual Password Password { get; set; }
        }

        public class UserDetail
        {
            public virtual int ID { get; set; }
            public virtual string EMail { get; set; }
            public virtual string Mobile { get; set; }
            public virtual string AlternateMobile { get; set; }
            public virtual string HomePhone { get; set; }
            public virtual string Address1 { get; set; }
            public virtual string Address2 { get; set; }
            public virtual int DistrictID { get; set; }
        }

        public class Password
        {
            public virtual int ID { get; set; }
            public virtual string PasswordHash { get; set; }
            public virtual string PasswordSalt { get; set; }
        }

    }

    namespace DbMappings
    {
        public class UserMap : ClassMap<User>
        {
            public UserMap()
            {
                LazyLoad();
                Id(s => s.ID);
                Map(s => s.FirstName);
                Map(s => s.LastName);
                Map(s => s.UserName);
                Map(s => s.Active);
                Table("Users");

                References(s => s.Detail, "DetailID").Cascade.All();
                References(s => s.Password, "PasswordID").Cascade.All();
            }
        }

        public class UserDetailMap : ClassMap<UserDetail>
        {
            public UserDetailMap()
            {
                Id(s => s.ID);
                Map(s => s.Mobile);
                Map(s => s.AlternateMobile);
                Map(s => s.HomePhone);
                Map(s => s.Address1);
                Map(s => s.Address2);
                Map(s => s.DistrictID);

                Table("UserDetails");
            }
        }

        public class PasswordMap : ClassMap<Password>
        {
            public PasswordMap()
            {
                Id(s => s.ID);
                Map(s => s.PasswordHash);
                Map(s => s.PasswordSalt);
                Table("Passwords");

            }
        }
    }

    namespace AutoMappings
    {
        public class UserMappingProfile : MappingProfileBase
        {
            public UserMappingProfile()
            {
                CreateMap<User, UserModel>();
                CreateMap<UserModel, User>();
                CreateMap<UserModel, UserDetail>();
            }
        }
    }

}
