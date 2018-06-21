using DynamicMenu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DynamicMenu.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<Menu> menus = new List<Menu>();
            menus = Menu.GetList("Azrul"); //test confict
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "This is my application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}