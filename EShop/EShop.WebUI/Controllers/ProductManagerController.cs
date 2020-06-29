using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using EShop.Core.Contracts;
using EShop.Core.Models;
using EShop.Core.ViewModels;
using EShop.DataAccess.InMemory;

namespace EShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        private readonly IRepository<Product> _productContext;
        private readonly IRepository<ProductCategory> _productCategoryContext;


        public ProductManagerController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext)
        {
            this._productContext = productContext;
            this._productCategoryContext = productCategoryContext;
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = _productContext.Collection().ToList();
            return View(products);
        }

        public ActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();

          viewModel.Product = new Product();
          viewModel.ProductCategories = _productCategoryContext.Collection();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                _productContext.Insert(product);
                _productContext.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            Product product = _productContext.Find(Id);

            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }
        }

        [HttpPost]
        public ActionResult Edit(Product product, string Id)
        {
            Product productToBeEdit = _productContext.Find(Id);

            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }

                productToBeEdit.Category = product.Category;
                productToBeEdit.Description = product.Description;
                productToBeEdit.Image = product.Image;
                productToBeEdit.Name = product.Name;
                productToBeEdit.Price = product.Price;

                _productContext.Commit();

                return RedirectToAction("Index"); 
            }
        }

        public ActionResult Delete(string Id)
        {
            Product productToBeDelete = _productContext.Find(Id);

            if (productToBeDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToBeDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Product productToBeDelete = _productContext.Find(Id);

            if (productToBeDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                _productContext.Delete(Id);
                _productContext.Commit();
                return RedirectToAction("Index");
            }
        }

    }
}