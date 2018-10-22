using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StaffPositions.Core.Contracts;
using StaffPositions.Core.Models;
using StaffPositions.Core.ViewModels;
using StaffPositions.DataAccess.SQL;
using System.Runtime.InteropServices;

namespace StaffPositions.WebUI.Controllers
{
    
    public class DeveloperManagerController : Controller
    {
        IDeveloperRepository<Developer> context;
        IPositionRepository<DeveloperPosition> developerPositions;

        public DeveloperManagerController(IDeveloperRepository<Developer> developerContext, IPositionRepository<DeveloperPosition> developerPositionContext)
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
        //[CustomAuthorize(Email = "gaspard@dev.com")]
        [Authorize(Roles = "Admin")]
        public ActionResult Create()//to display the page only
        {
            Developer model = new Developer();
            return View(model);
        }

        [HttpPost]//getting info from Create view
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Developer model, HttpPostedFileBase file)//to fill in the details
        {
            
            Developer developer = new Developer()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                FullName = model.FirstName + " " + model.LastName,
                Position = model.Position,
                Photo = model.Photo,

                Email = model.Email,
                Id = Guid.NewGuid().ToString(),
        };

            if (!ModelState.IsValid)
            {
                return View(model);//stay on the current page
            }
            else
            {
                //from postedfile, dave profile picture
                if (file != null)
                {
                    developer.Photo = developer.FullName + Path.GetExtension(file.FileName);//remane to always have a unique file reference
                    file.SaveAs(Server.MapPath("//Content//DeveloperProfiles//") + developer.Photo);//save the developer image into the DeveloperProfiles folder
                }
                
                context.Insert(developer);//add developer to cache memory
                context.Commit();//refresh memory

                return RedirectToAction("Index");//redirect to Index page, to view the updated list
            }

        }

        //edit developer
        //[CustomAuthorize(Email = "gaspard@dev.com")]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int DeveloperId)//to find the developer
        {
            //find the developer
            Developer developer = context.Find(DeveloperId);


            if (developer == null)
            {
                return HttpNotFound();
            }
            else
            {
                DeveloperManagerViewModel ViewModel = new DeveloperManagerViewModel();
                ViewModel.Developer = developer;
                ViewModel.DeveloperId = DeveloperId;
                ViewModel.DeveloperPositions = developerPositions.Collection();

                //Get developers potential supervisors from the database based on his role
                using (var dbContext = new DataContext())
                {
                    //3 Cases
                    if (developer.Position == "Developer")
                    { 
                        var potentialSuperiorsList = dbContext.Developers
                                        .SqlQuery("SELECT * FROM [StaffPositions].[dbo].[Developers] where Position = 'Team Lead' ORDER BY FullName ASC")
                                        .ToList<Developer>();
                       
                        ViewModel.PotentialSuperiors = potentialSuperiorsList;
                        
                    }
                    else if (developer.Position == "Team Lead")
                    {
                        var potentialSuperiorsList = dbContext.Developers
                                        .SqlQuery("SELECT * FROM [StaffPositions].[dbo].[Developers] where Position = 'Manager' ORDER BY FullName ASC")
                                        .ToList<Developer>();
                        
                        ViewModel.PotentialSuperiors = potentialSuperiorsList;
                    }
                    else //Manager has no superior
                    {
                       
                        List<Developer> potentialSuperiorsList = new List<Developer>();
                        potentialSuperiorsList.Add(new Developer { FirstName ="aaa",LastName="aaa",FullName= "Not Supervised", Email="aaaa@aaaa.aaa",Position="aaa",Photo ="aaa",DeveloperId=1000,SuperiorID=10000,SuperiorName = "Not Supervised" });

                        ViewModel.PotentialSuperiors = potentialSuperiorsList;
                    }
                }

                return View(ViewModel);
                
            }

        }

        [HttpPost]//getting info from a page
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(DeveloperManagerViewModel DeveloperViewModel, HttpPostedFileBase file)//to edit the developer
        {
            int DeveloperId;
            Developer developer = DeveloperViewModel.Developer;
            DeveloperId = developer.DeveloperId;


            Developer developerToEdit = context.Find(DeveloperId);
            if (developerToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    //return View(developer);//stay on the current page
                    //do nothing
                }

                //update developer to edit//
                developerToEdit.FirstName = developer.FirstName;
                developerToEdit.LastName = developer.LastName;
                developerToEdit.FullName = developer.FirstName + " " + developer.LastName;
                developerToEdit.Photo = developer.Photo;
                developerToEdit.Position = DeveloperViewModel.Position;
                developerToEdit.SuperiorName = DeveloperViewModel.SuperiorName;

                //from postedfile
                if (file != null)
                {
                    developerToEdit.Photo = developerToEdit.FullName + Path.GetExtension(file.FileName);//remane to always have a unique file reference
                    file.SaveAs(Server.MapPath("//Content//DeveloperProfiles//") + developerToEdit.Photo);//save the product image into the ProductImages folder
                }

                //get superiorID from SQL in the future
                // get supervisor's name from the view
                //read SQL to find supervisor developerId and 
                //if supervisor is manager, then set devtoedit managerid = supervisor dev Id and teamlead id to null
                //elseif supervisor is teamlead, then set devtoedit teamleadid = supervisor dev Id and manager id to null

                context.Commit();//refresh  memory
                return RedirectToAction("Index");//redirect to Index page, to view the updated list
            }
        }

        //delete a developer
        //[CustomAuthorize(Email = "gaspard@dev.com")]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int DeveloperId)//to find the developer to delete
        {
            Developer developerToDelete = context.Find(DeveloperId);
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
        [Authorize(Roles = "Admin")]
        public ActionResult ConfirmDelete(Developer developer, int DeveloperId)//to find the developer to delete
        {
            Developer developerToDelete = context.Find(DeveloperId);
            if (developerToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(DeveloperId);
                context.Commit();//refresh cache memory
                return RedirectToAction("Index");//redirect to Index page, to view the updated list
            }

        }

        //to update the superiors dropdown list based on the developer's position
        public JsonResult GetSuperiorList(string Position, int DeveloperId)
        {
            List<Developer> potentialSuperiorsList = new List<Developer>();
            using (var dbContext = new DataContext())
            {
                dbContext.Configuration.ProxyCreationEnabled = false;
                if (Position == "Developer")
                {
                    potentialSuperiorsList = dbContext.Developers.Where(x => x.Position == "Team Lead" && x.DeveloperId != DeveloperId).ToList();
                }
                else if (Position == "Team Lead")
                {
                    potentialSuperiorsList = dbContext.Developers.Where(x => x.Position == "Manager" && x.DeveloperId != DeveloperId).ToList();
                }
                else
                {
                    potentialSuperiorsList.Add(new Developer { FirstName = "aaa", LastName = "aaa", FullName = "Not Supervised", Email = "aaaa@aaaa.aaa", Position = "aaa", Photo = "aaa", DeveloperId = 1000, SuperiorID = 10000, SuperiorName = "Not Supervised" });
                }

                bool isEmpty = !potentialSuperiorsList.Any();
                if (isEmpty)
                {
                    potentialSuperiorsList.Add(new Developer { FirstName = "aaa", LastName = "aaa", FullName = "Not Supervised", Email = "aaaa@aaaa.aaa", Position = "aaa", Photo = "aaa", DeveloperId = 1000, SuperiorID = 10000, SuperiorName = "No one found!" });
                }
                else
                {
                    //do nothing
                }
            }
            return Json(potentialSuperiorsList, JsonRequestBehavior.AllowGet);
        }
    }
}