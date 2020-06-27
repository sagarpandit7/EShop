using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using EShop.Core.Models;


namespace EShop.DataAccess.InMemory
{
   public class InMemoryRepository<T> where T : BaseEntity
   {
       private readonly ObjectCache _cache = MemoryCache.Default;
       private readonly List<T> _items;
       private readonly string _className;

       public InMemoryRepository()
       {
           _className = typeof(T).Name;  
           _items = _cache[_className] as List<T> ?? new List<T>();
       }

        public IQueryable<T> Collection()
        {
            return _items.AsQueryable();
        }

        public void Commit()
        {
            _cache[_className] = _items;
        }

        public void Delete(string id)
        {
            var t = _items.Find(i => i.Id == id);

            if (t == null)
            {
                throw new Exception(_className + " Not Found");
            }
            _items.Remove(t);
        }

        public T Find(string id)
        {
            var t = _items.Find(i => i.Id == id);

            if (t == null)
            {
                throw new Exception(_className + " Not Found");
            }
            return t;
        }

        public void Insert(T t)
        {
           _items.Add(t);
        }

        public void Update(T t)
        {
            var tToUpdate = _items.Find(i => i.Id == t.Id);

            tToUpdate = t ?? throw new Exception(_className + " Not Found");

        }
   }
}
