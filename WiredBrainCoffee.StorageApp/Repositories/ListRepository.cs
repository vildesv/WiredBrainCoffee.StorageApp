using System;
using System.Collections.Generic;
using System.Linq;
using WiredBrainCoffee.StorageApp.Entities;

namespace WiredBrainCoffee.StorageApp.Repositories
{
    public class ListRepository<T> : IRepository<T>  where T : IEntity
    {
        // public TKey? Key { get; set; }
        private readonly List<T> _items = new();

        public IEnumerable<T> GetAll()
        {
            return _items.ToList();
        }

        public T GetById(int id)
        {
            return _items.Single(item => item.Id == id);
        }

        public void Add(T item)
        {
            item.Id = _items.Count + 1;
            _items.Add(item);
        }

        public void Remove(T item)
        {
            _items.Remove(item);
        }

        public void Save()
        {
            // Everything is saved already in the List<T>
        }

    }

    /* public class GenericRepositoryWithRemove<T> : GenericRepository<T,string>
{
    public void Remove(T item)
    {
        _items.Remove(item);
    }
} */

}
