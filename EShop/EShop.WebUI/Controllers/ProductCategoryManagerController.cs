using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EShop.DataAccess.InMemory;
using EShop.Core.Models;

namespace EShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        private ProductCategoryRepository productCategoryRepository = null;

        public ProductCategoryManagerController()
        {
            productCategoryRepository = new ProductCategoryRepository();
        }

        // GET: ProductCategoryManager
        public ActionResult Index()
        {
            List<ProductCategory> productCategories = productCategoryRepository.Collection().ToList();
            return View(productCategories);
        }

        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(ProductCategory productCategoryToAdd)
        {
            productCategoryRepository.Insert(productCategoryToAdd);
            productCategoryRepository.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            ProductCategory productToEdit = productCategoryRepository.Find(id);

            if (productToEdit != null)
            {
                return View(productToEdit);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult Edit(ProductCategory productCategoryToEdit)
        {
            ProductCategory productCategory = productCategoryRepository.Find(productCategoryToEdit.Id);
            productCategory.Category = productCategoryToEdit.Category;
            productCategoryRepository.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            ProductCategory productToDelete = productCategoryRepository.Find(id);

            if (productToDelete != null)
            {
                return View(productToDelete);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string id)
        {
            ProductCategory productCategoryToDelete = productCategoryRepository.Find(id);

            if (productCategoryToDelete != null)
            {
                productCategoryRepository.Delete(id);
                productCategoryRepository.Commit();
                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound();
            }
        }
        
    }
}