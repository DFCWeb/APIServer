using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solution.Helpers
{
    public class SessionFactory
    {
        private static ISessionFactory sessionFactory;
        static readonly object padlock = new object();


        private SessionFactory()
        {

        }

        public static ISessionFactory Instance
        {
            get
            {
                if (sessionFactory == null)
                {
                    lock (padlock)
                    {
                        if (sessionFactory == null)
                        {
                            sessionFactory = SessionFactoryBuilder.BuildSessionFactory(ConfigurationManager.Setting["AppSettings:Development:ConnectionString"], true, true);
                        }
                    }
                }
                return sessionFactory;
            }
        }
        
    }
}
