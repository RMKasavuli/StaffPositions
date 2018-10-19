using StaffPositions.Core.Contracts;
using StaffPositions.Core.Models;
using StaffPositions.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StaffPositions.WebUI.Controllers
{
    public class HomeController : Controller
    {
        //IRepository<Product> context;
        //IRepository<ProductCategory> productCategories;

        //public HomeController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext)
        //{
        //    context = productContext;
        //    productCategories = productCategoryContext;
        //}

        //public ActionResult Index(string Category=null)
        //{
        //    //get the list of products, send them to the main view
        //    List<Product> products = context.Collection().ToList();
        //    List<ProductCategory> categories = productCategories.Collection().ToList();

        //    if (Category == null)
        //    {
        //        products=context.Collection().ToList();
        //    }
        //    else
        //    {
        //        products = context.Collection().Where(p => p.Category == Category).ToList();
        //    }

        //    ProductListViewModel model = new ProductListViewModel();
        //    model.Products = products;
        //    model.ProductCategories = categories;

        //    return View(model);//return the list to the view
        //}

        ////create view products
        //public ActionResult Details (string Id)
        //{
        //    Product product = context.Find(Id);
        //    if (product == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    else
        //    {
        //        return View(product);
        //    }
        //}

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}




        IRepository<Developer> context;
        IRepository<DeveloperPosition> developerPositions;

        public HomeController(IRepository<Developer> developerContext, IRepository<DeveloperPosition> developerPositionContext)
        {
            context = developerContext;
            developerPositions = developerPositionContext;
        }

        public ActionResult Index(string Position = null)
        {
            //get the list of developers, send them to the main view
            List<Developer> developers = context.Collection().ToList();
            List<DeveloperPosition> positions = developerPositions.Collection().ToList();
          

            if (Position == null)
            {
                developers = context.Collection().ToList();
            }
            else
            {
                developers = context.Collection().Where(p => p.Position == Position).ToList();
            }

            DeveloperListViewModel model = new DeveloperListViewModel();
            model.Developers = developers;
            model.DeveloperPositions = positions;

            return View(model);//return the list to the view
        }

        //create view developers
        public ActionResult Details(string Id)
        {
            Developer developer = context.Find(Id);
            if (developer == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(developer);
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