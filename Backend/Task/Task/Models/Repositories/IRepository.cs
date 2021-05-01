using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task.Models
{
    public interface IRepository<TEntity>
    {
        //Get
        IList<TEntity> Get();

        //GetById
        TEntity Find(int id);

        //Post , Create
        void Add(TEntity entity);

        //Search 
        IList<TEntity> Search(string term);

        //Delete 
        void Delete(int id);

        //Put , Update
        void Update(int id, TEntity entity);

        TEntity Login(TEntity entity);
    }
}
