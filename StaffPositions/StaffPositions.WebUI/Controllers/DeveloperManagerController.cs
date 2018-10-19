using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StaffPositions.Core.Contracts;
using StaffPositions.Core.Models;
using StaffPositions.Core.ViewModels;
using StaffPositions.DataAccess.InMemory;

namespace StaffPositions.WebUI.Controllers
{
    public class DeveloperManagerController : Controller
    {
        IRepository<Developer> context;
        IRepository<DeveloperPosition> developerPositions;

        public DeveloperManagerController(IRepository<Developer> developerContext, IRepository<DeveloperPosition> developerPositionContext)
        {
            context = developerContext;
            developerPositions = developerPositionContext;
        }
        // GET: DeveloperManager
        public ActionResult Index()
        {
            List<Developer> developers = context.Collection().ToList();
            return View(developers);
        }

        //cretae new developer
        public ActionResult Create()//to display the page only
        {
            DeveloperManagerViewModel ViewModel = new DeveloperManagerViewModel();
            ViewModel.Developer = new Developer();
            //get from the database
            ViewModel.DeveloperPositions = developerPositions.Collection();
            return View(ViewModel);
        }

        [HttpPost]//getting info from a page
        public ActionResult Create(Developer developer, HttpPostedFileBase file)//to fill in the details
        {
            if (!ModelState.IsValid)
            {
                return View(developer);//stay on the current page
            }
            else
            {
                //from postedfile
                if (file != null)
                {
                    developer.Photo = developer.Id + Path.GetExtension(file.FileName);//remane to always have a unique file reference
                    file.SaveAs(Server.MapPath("//Content//DeveloperProfiles//") + developer.Photo);//save the developer image into the DeveloperProfiles folder
                }
                //
                context.Insert(developer);//add developer to cache memory
                context.Commit();//refresh cache memory

                return RedirectToAction("Index");//redirect to Index page, to view the updated list
            }

        }

        //edit developer
        public ActionResult Edit(string Id)//to find the developer
        {
            //find the developer
            Developer developer = context.Find(Id);
            if (developer == null)
            {
                return HttpNotFound();
            }
            else
            {
                DeveloperManagerViewModel ViewModel = new DeveloperManagerViewModel();
                ViewModel.Developer = developer;
                //get from the database
                ViewModel.DeveloperPositions = developerPositions.Collection();

                return View(ViewModel);
            }

        }

        [HttpPost]//getting info from a page
        public ActionResult Edit(Developer developer, string Id, HttpPostedFileBase file)//to edit the developer
        {

            Developer developerToEdit = context.Find(Id);
            if (developerToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(developer);//stay on the current page
                }

                //update developer to edit//come back here
                developerToEdit.Position = developer.Position;
                developerToEdit.TeamLeadID = developer.TeamLeadID;
                developerToEdit.ManagerID = developer.ManagerID;

                context.Commit();//refresh  memory

                return RedirectToAction("Index");//redirect to Index page, to view the updated list


            }
        }

        public ActionResult Delete(string Id)//to find the developer to delete
        {
            Developer developerToDelete = context.Find(Id);
            if (developerToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(developerToDelete);
            }

        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(Developer developer, string Id)//to find the developer to delete
        {
            Developer developerToDelete = context.Find(Id);
            if (developerToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();//refresh cache memory
                return RedirectToAction("Index");//redirect to Index page, to view the updated list
            }

        }
    }
}