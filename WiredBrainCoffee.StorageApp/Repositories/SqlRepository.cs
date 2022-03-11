using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System;
using WiredBrainCoffee.StorageApp.Entities;

namespace WiredBrainCoffee.StorageApp.Repositories
{
    // public delegate void ItemAdded<in T>(T item); - changed delegate from ItemAdded<T> to the Action<T>, see line 22.
    public class SqlRepository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public SqlRepository(DbContext dbContext)  // removed parameter Action<T>? itemAddedCallback = null  
        {
            _dbContext = dbContext;
            // ItemAdded = itemAddedCallback;
            _dbSet = _dbContext.Set<T>();
        }

        public event EventHandler<T>? ItemAdded; // changed from private readonly to public event - and then changed from Action<T> delegate to EventHandler<T> delegate.

        public IEnumerable<T> GetAll()
        {
            return _dbSet.OrderBy(item => item.Id).ToList();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Add(T item)
        {
            _dbSet.Add(item);
            ItemAdded?.Invoke(this, item);
        }

        public void Remove(T item)
        {
            _dbSet.Remove(item);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
