using NHibernate;
using NHibernate.Criterion;
using Solution.DataDefinitions;
using Solution.DataDefinitions.Entities;
using Solution.Exceptions;
using Solution.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        User GetById(object id);
        User Create(User user, string password);
        void Update(User user, string password = null);
        void Delete(object id);
    }

    public class UserService : DataService<User>, IUserService
    {
        public UserService()
        {
        }

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            User user = null;
            using (var session = SessionFactory.Instance.OpenSession())
            {
                user = session.CreateCriteria<User>()
                    .Add(Restrictions.Eq("UserName", username))
                    .SetFetchMode("Detail", FetchMode.Eager)
                    .SetFetchMode("Password", FetchMode.Eager)
                    .List<User>()
                    .FirstOrDefault();
            }

            // check if username exists
            if (user == null)
            {
                return null;
            }
            else
            {
                //check if password is correct
                var hash = Convert.FromBase64String(user.Password.PasswordHash);
                var salt = Convert.FromBase64String(user.Password.PasswordSalt);

                if (!VerifyPasswordHash(password, hash, salt))
                {
                    return null;
                }
            }

            // authentication successful
            return user;
        }

        //public IEnumerable<User> GetAll()
        //{
        //    using (var session = SessionFactory.Instance.OpenSession())
        //    {
        //        var users = session.CreateCriteria<User>()
        //            .SetFetchMode("Detail", FetchMode.Eager)
        //            .List<User>();

        //        return users;
        //    }
        //}

        //public User GetById(int id)
        //{
        //    using (var session = SessionFactory.Instance.OpenSession())
        //    {
        //        var user = session.CreateCriteria<User>()
        //            .Add(Restrictions.Eq("ID", id))
        //            .SetFetchMode("Detail", FetchMode.Eager)
        //            .List<User>()
        //            .FirstOrDefault();

        //        return user;
        //    }
        //}

        public User Create(User user, string password)
        {
            using (var session = SessionFactory.Instance.OpenSession())
            {
                // validation
                if (string.IsNullOrWhiteSpace(password))
                    throw new AppException("Password is required");

                if (!session.CreateCriteria<User>()
                    .Add(Restrictions.Eq("UserName", user.UserName))
                    .List<User>()
                    .Any())
                {
                    byte[] passwordHash, passwordSalt;
                    CreatePasswordHash(password, out passwordHash, out passwordSalt);

                    user.Password = new Password()
                    {
                        PasswordHash = Convert.ToBase64String(passwordHash),
                        PasswordSalt = Convert.ToBase64String(passwordSalt)
                    };

                    using (var transaction = session.BeginTransaction())
                    {
                        session.SaveOrUpdate(user);
                        transaction.Commit();

                        return user;
                    }
                }
                else
                {
                    throw new AppException($"User name '{user.UserName}' already exists!");
                }
            }
        }

        public void Update(User userParam, string password = null)
        {
            using (var session = SessionFactory.Instance.OpenSession())
            {
                var user = session.Get<User>(userParam.ID);
                if (user == null)
                    throw new AppException("User not found");

                // update user properties
                user.FirstName = userParam.FirstName;
                user.LastName = userParam.LastName;
                user.Detail.Mobile = userParam.Detail.Mobile;
                user.Detail.AlternateMobile = userParam.Detail.AlternateMobile;
                user.Detail.HomePhone = userParam.Detail.HomePhone;
                user.Detail.EMail = userParam.Detail.EMail;

                if (!string.IsNullOrWhiteSpace(password))
                {
                    byte[] passwordHash, passwordSalt;
                    CreatePasswordHash(password, out passwordHash, out passwordSalt);

                    user.Password.PasswordHash = Encoding.ASCII.GetString(passwordHash);
                    user.Password.PasswordSalt = Encoding.ASCII.GetString(passwordSalt);
                }

                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(user);
                    transaction.Commit();
                }
            }
        }

        //public void Delete(int id)
        //{
        //    using (var session = SessionFactory.Instance.OpenSession())
        //    {
        //        var user = session.Get<User>(id);
        //        if (user == null)
        //            throw new AppException("User not found");

        //        using (var transaction = session.BeginTransaction())
        //        {
        //            session.Delete(user);
        //            transaction.Commit();
        //        }
        //    }
        //}

        // private helper methods
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
