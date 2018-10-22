using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StaffPositions.Core.Contracts;
using StaffPositions.Core.Models;

namespace StaffPositions.WebUI.Controllers
{
    
    public class DeveloperPositionManagerController : Controller
    {
        IPositionRepository<DeveloperPosition> context;

        public DeveloperPositionManagerController(IPositionRepository<DeveloperPosition> context)
        {
            this.context = context;
        }
        // GET: DeveloperPositionManager
        public ActionResult Index()
        {
            List<DeveloperPosition> developerPositions = context.Collection().ToList();
            return View(developerPositions);
        }


        //cretae new developer position
        public ActionResult Create()//to display the page only
        {
            DeveloperPosition developerPosition = new DeveloperPosition();
            return View(developerPosition);
        }

        [HttpPost]//getting info from a page
        public ActionResult Create(DeveloperPosition developerPosition)//to fill in the details
        {
            if (!ModelState.IsValid)
            {
                return View(developerPosition);//stay on the current page
            }
            else
            {
                context.Insert(developerPosition);//add developer position to cache memory
                context.Commit();//refresh cache memory

                return RedirectToAction("Index");//redirect to Index page, to view the updated list
            }
        }

        //edit developer position
        public ActionResult Edit(string Id)//to find the developer position
        {
            //find the developer position
            DeveloperPosition developerPosition = context.Find(Id);
            if (developerPosition == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(developerPosition);
            }
        }

        [HttpPost]//getting info from a page
        public ActionResult Edit(DeveloperPosition developerPosition, string Id)//to edit the developer
        {
            DeveloperPosition developerPositionToEdit = context.Find(Id);
            if (developerPositionToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(developerPositionToEdit);//stay on the current page
                }
                else
                {
                    //update developer position to edit
                    developerPositionToEdit.Position = developerPosition.Position;
                 
                    context.Commit();//refresh cache memory

                    return RedirectToAction("Index");//redirect to Index page, to view the updated list
                }
            }
        }

        //delete a developer position
        public ActionResult Delete(string Id)//to find the developer position to delete
        {
            DeveloperPosition developerPositionToDelete = context.Find(Id);
            if (developerPositionToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(developerPositionToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(DeveloperPosition developerPosition, string Id)//to find the developer position to delete
        {
            DeveloperPosition developerPositionToDelete = context.Find(Id);
            if (developerPositionToDelete == null)
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