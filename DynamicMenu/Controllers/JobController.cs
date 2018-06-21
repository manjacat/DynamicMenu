using DynamicMenu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DynamicMenu.Controllers
{
    public class JobController : Controller
    {
        // GET: Job
        public ActionResult Index()
        {
            ViewBag.Dropdown = JobDistribution.GetList();
            return View();
        }

        [HttpPost]
        public ActionResult Index(string noAduan)
        {
            return RedirectToAction("Details", new { id = 1, aduan = noAduan });
        }

        public ActionResult Details(string id, string aduan)
        {
            string noAduan = aduan;
            JobDistribution jd = JobDistribution.Get(noAduan);
            return View(jd);
        }
    }
}