using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Solution.Helpers
{
    public class SessionFactoryBuilder
    {
        public static ISessionFactory BuildSessionFactory(string connectionString, bool create = false, bool update = false)
        {
            return Fluently.Configure()
                    .Database(MySQLConfiguration.Standard.ConnectionString(connectionString))
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<SessionFactoryBuilder>())
                    .BuildSessionFactory();
        }
    }
}
