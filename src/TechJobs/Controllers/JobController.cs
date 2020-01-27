using Microsoft.AspNetCore.Mvc;
using TechJobs.Data;
using TechJobs.Models;
using TechJobs.ViewModels;


namespace TechJobs.Controllers
{
    public class JobController : Controller
    {

        // Our reference to the data store
        private static JobData jobData;

        static JobController()
        {
            jobData = JobData.GetInstance();
        }

        // The detail display for a given Job at URLs like /Job?id=17
        public IActionResult Index(int id)
        {
            Job thisJob = jobData.Find(id);
            return View();
        }

        public IActionResult New()
        {
            NewJobViewModel newJobViewModel = new NewJobViewModel();
            return View(newJobViewModel);
        }

        [HttpPost]
        public IActionResult New(NewJobViewModel newJobViewModel)
        {
       
            if (ModelState.IsValid)
            {

                Employer emp = jobData.Employers.Find(newJobViewModel.EmployerID);
                Location loc = jobData.Locations.Find(newJobViewModel.LocationID);
                CoreCompetency corecomp = jobData.CoreCompetencies.Find(newJobViewModel.CoreCompetencyID);
                PositionType pos = jobData.PositionTypes.Find(newJobViewModel.PositionID);

                Job newJob = new Job
                {


                    Name = newJobViewModel.Name,
                    Employer = emp,
                    Location = loc,
                    CoreCompetency = corecomp,
                    PositionType = pos

                };

                jobData.Jobs.Add(newJob);

                return Redirect("/Job?id=" + newJob.ID);
            }

            return View(newJobViewModel);
        }
    }
}

