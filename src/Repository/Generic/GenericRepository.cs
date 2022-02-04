using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RestWith.NET.Context;
using RestWith.NET.Model.Base;

namespace RestWith.NET.Repository.Generic
{   
    public class GenericRepository<T> : IRepository<T> where T:BaseEntity
    {
        protected readonly DataContext _context;
        private DbSet<T> dataset;

        public GenericRepository(DataContext context)
        {
            _context = context;
            dataset = _context.Set<T>();
            
        }
        public T Create(T item)
        {
             try
            {
                dataset.Add(item);
                _context.SaveChanges(); 
                return item;
            }
            catch(Exception e)
            {
                
            throw e;
            }
        }

        public void Delete(long id)
        {
            var deleted = dataset.SingleOrDefault(it=>it.Id==id);
            dataset.Remove(deleted);
            _context.SaveChanges();
        }

        public bool Exists(long id)
        {
            return dataset.Any(it=>it.Id==id);
        }

        public List<T> FindAll()
        {
            return dataset.ToList();
        }

        public T FindById(long id)
        {
            return  dataset.SingleOrDefault(it=>it.Id==id);
        }

        public List<T> FindWithPagedSearch(string query)
        {
           return dataset.FromSqlRaw(query).ToList();
        }

        public int getCount(string query)
        {
            var result = "";
            using (var connection = _context.Database.GetDbConnection()) 
            {
                connection.Open();
                using( var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    result = command.ExecuteScalar().ToString();
                }
            }
            return int.Parse(result);
        }

        public T Update(T item, long id)
        {
           
            var toBeUpdated =  dataset.SingleOrDefault(it=>it.Id==id);
            if(toBeUpdated==null)
                return null;
            dataset.Update(toBeUpdated).
            CurrentValues.
            SetValues(toBeUpdated);
            _context.SaveChanges();
            return toBeUpdated;
            
        }
    }
}