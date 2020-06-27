using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using EShop.Core.Contracts;
using EShop.Core.Models;
using EShop.DataAccess.InMemory;

namespace EShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        private readonly IRepository<Product> _context;

        public ProductManagerController(IRepository<Product> context)
        {
            _context = context;
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = _context.Collection().ToList();
            return View(products);
        }

        public ActionResult Create()
        {
            Product product = new Product();
            return View(product);
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
                _context.Insert(product);
                _context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            Product product = _context.Find(Id);

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
            Product productToBeEdit = _context.Find(Id);

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

                _context.Commit();

                return RedirectToAction("Index"); 
            }
        }

        public ActionResult Delete(string Id)
        {
            Product productToBeDelete = _context.Find(Id);

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
            Product productToBeDelete = _context.Find(Id);

            if (productToBeDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                _context.Delete(Id);
                _context.Commit();
                return RedirectToAction("Index");
            }
        }

    }
}