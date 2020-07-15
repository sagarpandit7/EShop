using System.Collections.Generic;
using System.Linq;
using EShop.Core.Contracts;
using System.Web.Mvc;
using Antlr.Runtime.Misc;
using EShop.Core.Models;

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

        public ActionResult Index()
        {
            List<Product> products = _productContext.Collection().ToList(); 
            return View(products);
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