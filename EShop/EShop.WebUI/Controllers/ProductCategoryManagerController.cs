using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EShop.Core.Contracts;
using EShop.DataAccess.InMemory;
using EShop.Core.Models;

namespace EShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        private readonly IRepository<ProductCategory> _productCategoryRepository;

        public ProductCategoryManagerController(IRepository<ProductCategory> productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        // GET: ProductCategoryManager
        public ActionResult Index()
        {
            List<ProductCategory> productCategories = _productCategoryRepository.Collection().ToList();
            return View(productCategories);
        }

        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(ProductCategory productCategoryToAdd)
        {
            _productCategoryRepository.Insert(productCategoryToAdd);
            _productCategoryRepository.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            ProductCategory productToEdit = _productCategoryRepository.Find(id);

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
            ProductCategory productCategory = _productCategoryRepository.Find(productCategoryToEdit.Id);
            productCategory.Category = productCategoryToEdit.Category;
            _productCategoryRepository.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            ProductCategory productToDelete = _productCategoryRepository.Find(id);

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
            ProductCategory productCategoryToDelete = _productCategoryRepository.Find(id);

            if (productCategoryToDelete != null)
            {
                _productCategoryRepository.Delete(id);
                _productCategoryRepository.Commit();
                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound();
            }
        }
        
    }
}