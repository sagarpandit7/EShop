using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using EShop.Core;
using EShop.Core.Models;

namespace EShop.DataAccess.InMemory
{
   public class ProductRepository
   {
       private ObjectCache cache = MemoryCache.Default;

       private List<Product> products = null;

       public ProductRepository()
       {
           products = cache["products"] as List<Product> ?? new List<Product>();
       }

       public void Commit()
       {
           cache["products"] = products;
       }

       public void Insert(Product product)
       {
           products.Add(product);
       }

       public void Update(Product product)
       {
           Product productToBeUpdate = products.Find(p => p.Id == product.Id);

           if (productToBeUpdate != null)
           {
               productToBeUpdate = product;
           }
           else
           {
               throw new Exception("Product Not Found");
           }
       }

       public Product Find(string Id)
       {
           Product product = products.Find(p => p.Id == Id);

           if (product != null)
           {
               return product;
           }
           else
           {
               throw new Exception("Product Not Found");
           }
       }

       public IQueryable<Product> Collection()
       {
           return products.AsQueryable();
       }

       public void Delete(string Id)
       {
           Product productToBeDelete = products.Find(p => p.Id == Id);

           if (productToBeDelete != null)
           {
               products.Remove(productToBeDelete);
           }
           else
           {
               throw new Exception("Product Not Found");
           }
       }
   }
}
