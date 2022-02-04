using RestWith.NET.Model.Base;
using System.Collections.Generic;

namespace RestWith.NET.Repository
{
    public interface IRepository<T> where T: BaseEntity
    {
        T Create(T item);
        T FindById(long id);
        T Update(T item, long id);
        void Delete(long id);
        List<T> FindAll();
        bool Exists(long id);
        List<T> FindWithPagedSearch(string query);
        int getCount(string query);
    }
}
