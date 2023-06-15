using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;
namespace BusinessAccessLayer
{
    public class AccountRepo:IBaseRepo<Account>
    {
        #region PrivateVariable
        private ApplicationContext _dc;
        #endregion

        #region Constructor
        public AccountRepo()
        {
            _dc = new ApplicationContext();
        }
        #endregion

        #region Delete
        public void Delete(Account account)
        {
            _dc.Accounts.Remove(account);
            _dc.SaveChanges();

        }
        #endregion

        #region GetMethods
        public Account Get(int AccountId)
        {
            return _dc.Accounts.Find(AccountId);
        }

        public List<Account> GetAll()
        {
            return _dc.Accounts.ToList();
        }
        #endregion

        #region SaveMethod
        public int Save(Account account)
        {
            _dc.Accounts.Add(account);
            _dc.SaveChanges();
            return account.Id;
        }

        public int Update(Account entity)
        {
            _dc.Accounts.Update(entity);
            _dc.SaveChanges();
            return entity.Id;
        }
        #endregion
    }
}
