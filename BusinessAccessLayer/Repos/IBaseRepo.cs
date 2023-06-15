using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer;
namespace BusinessAccessLayer
{
    public interface IBaseRepo<TEntity>
    {
        TEntity Get(int EntityId);
        List<TEntity> GetAll();
        int Save(TEntity entity);
        void Delete(TEntity EntityId);
        int Update(TEntity entity);
    }
}
