using PeopleManager.Model;
using System;
using System.Collections.Generic;

namespace PeopleManager.Persistence
{
    public interface IRepository<T>
        where T : EntityBase
    {
        T Add(T entity);

        T Get(Guid id);

        IEnumerable<T> GetAll();

        void Remove(Guid id);

        void Update(T entity);

        void SaveChanges();
    }
}