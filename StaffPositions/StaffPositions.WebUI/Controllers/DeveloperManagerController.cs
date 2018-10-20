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
using StaffPositions.DataAccess.SQL;

namespace StaffPositions.WebUI.Controllers
{
    public class DeveloperManagerController : Controller
    {
        IDeveloperRepository<Developer> context;
        IRepository<DeveloperPosition> developerPositions;

        public DeveloperManagerController(IDeveloperRepository<Developer> developerContext, IRepository<DeveloperPosition> developerPositionContext)
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
                    developer.Photo = developer.DeveloperId + Path.GetExtension(file.FileName);//remane to always have a unique file reference
                    file.SaveAs(Server.MapPath("//Content//DeveloperProfiles//") + developer.Photo);//save the developer image into the DeveloperProfiles folder
                }
                //
                context.Insert(developer);//add developer to cache memory
                context.Commit();//refresh cache memory

                return RedirectToAction("Index");//redirect to Index page, to view the updated list
            }

        }

        //edit developer
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
                //get from the database
                ViewModel.DeveloperPositions = developerPositions.Collection();

                //Get developers potential supervisors from the database
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
                    //return RedirectToAction("Index");//redirect to Index page, to view the updated list
                }

                //update developer to edit//come back here
                
                developerToEdit.FirstName = developer.FirstName;
                developerToEdit.LastName = developer.LastName;
                developerToEdit.Position = developer.Position;
                developerToEdit.SuperiorName = developer.SuperiorName;

                //get superiorID from SQL
                //developerToEdit.SuperiorID = developer.Superior.DeveloperId;

                //reviens ici, get supervisor's name from the view
               
                //read SQL to find supervisor developerId and position
                

                //if supervisor is manager, then set devtoedit managerid = supervisor dev Id and teamlead id to null
                //elseif supervisor is teamlead, then set devtoedit teamleadid = supervisor dev Id and manager id to null

                //developerToEdit.Superior = developer.Superior;
                //developerToEdit.TeamLeadID = developer.TeamLeadID;
                //developerToEdit.ManagerID = developer.ManagerID;

                context.Commit();//refresh  memory

                return RedirectToAction("Index");//redirect to Index page, to view the updated list


            }
        }

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
    }
}