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
        
        IDeveloperRepository<Developer> context;
        IRepository<DeveloperPosition> developerPositions;

        public HomeController(IDeveloperRepository<Developer> developerContext, IRepository<DeveloperPosition> developerPositionContext)
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
        public ActionResult Details(int DeveloperId)
        {

            Developer developer = context.Find(DeveloperId);
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