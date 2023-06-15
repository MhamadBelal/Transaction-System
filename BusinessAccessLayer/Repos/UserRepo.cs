using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;
namespace BusinessAccessLayer
{
    public class UserRepo : IBaseRepo<User>
    {
        #region PrivateVariable
        private ApplicationContext _dc;
        #endregion

        #region Constructor
        public UserRepo()
        {
            _dc = new ApplicationContext();
        }
        #endregion

        #region Delete
        public void Delete(User user)
        {
            _dc.Users.Remove(user);
            _dc.SaveChanges();

        }
        #endregion

        #region GetMethods
        public User Get(int UserId)
        {
            return _dc.Users.Find(UserId);
        }

        public List<User> GetAll()
        {
            return _dc.Users.ToList();
        }
        #endregion

        #region SaveMethod
        public int Save(User user)
        {
            _dc.Users.Add(user);
            _dc.SaveChanges();
            return user.Id;
        }

        public int Update(User entity)
        {
            _dc.Users.Update(entity);
            _dc.SaveChanges();
            return entity.Id;
        }
        #endregion


    }
}
