using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer;
using System.Linq;
namespace BusinessAccessLayer
{
    public class TransactionRepo:IBaseRepo<Transaction>
    {
        #region PrivateVariable
        private ApplicationContext _dc;
        #endregion

        #region Constructor
        public TransactionRepo()
        {
            _dc = new ApplicationContext();
        }
        #endregion

        #region Delete
        public void Delete(Transaction transaction)
        {
            _dc.Transactions.Remove(transaction);
            _dc.SaveChanges();

        }
        #endregion

        #region GetMethods
        public Transaction Get(int TransactionId)
        {
            return _dc.Transactions.Find(TransactionId);
        }

        public List<Transaction> GetAll()
        {
            return _dc.Transactions.ToList();
        }
        #endregion

        #region SaveMethod
        public int Save(Transaction transaction)
        {
            _dc.Transactions.Add(transaction);
            _dc.SaveChanges();
            return transaction.Id;
        }

        public int Update(Transaction entity)
        {
            _dc.Transactions.Update(entity);
            _dc.SaveChanges();
            return entity.Id;
        }
        #endregion
    }
}
