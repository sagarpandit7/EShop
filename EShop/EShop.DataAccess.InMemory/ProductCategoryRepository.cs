using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using EShop.Core.Models;

namespace EShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        private ObjectCache cache = MemoryCache.Default;

        private List<ProductCategory> productCategories = null;

        public ProductCategoryRepository()
        {
            productCategories = cache["productCategory"] as List<ProductCategory> ?? new List<ProductCategory>();
        }

        public void Commit()
        {
            cache["productCategory"] = productCategories;
        }

        public void Insert(ProductCategory productCategory)
        {
            productCategories.Add(productCategory);
        }

        public void Update(ProductCategory productCategory)
        {
            ProductCategory productCategoryToUpdate = productCategories.Find(p => p.Id == productCategory.Id);

            if (productCategoryToUpdate != null)
            {
                productCategoryToUpdate = productCategory;
            }
            else
            {
                throw new Exception("Product Category Not Found");
            }
        }

        public ProductCategory Find(string id)   
        {
            var productCategory = productCategories.Find(p => p.Id == id);

            if (productCategory != null)
            {
                return productCategory;
            }
            else
            {
                throw new Exception("Product Category Not Found");
            }
        }

        public IQueryable<ProductCategory> Collection()
        {
            return productCategories.AsQueryable();
        }

        public void Delete(string id)
        {
            var productToDelete = productCategories.Find(p => p.Id == id);

            if (productToDelete != null)
            {
                productCategories.Remove(productToDelete);
            }
        }
    }
}
