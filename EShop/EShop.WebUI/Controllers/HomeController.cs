using System.Collections.Generic;
using System.Linq;
using EShop.Core.Contracts;
using System.Web.Mvc;
using Antlr.Runtime.Misc;
using EShop.Core.Models;
using EShop.Core.ViewModels;

namespace EShop.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Product> _productContext;
        private readonly IRepository<ProductCategory> _productCategoryContext;


        public HomeController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext)
        {
            this._productContext = productContext;
            this._productCategoryContext = productCategoryContext;
        }

        public ActionResult Index(string Category=null)
        {
            List<Product> products;
            List<ProductCategory> productCategories = _productCategoryContext.Collection().ToList();

            if (Category == null)
            {
                products = _productContext.Collection().ToList();
            }
            else
            {
                products = _productContext.Collection().Where(p => p.Category == Category).ToList();
            }

            ProductListViewModel model = new ProductListViewModel();
            model.Products = products;
            model.ProductCategories = productCategories;

            return View(model);
        }

        public ActionResult Details(string Id)
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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}