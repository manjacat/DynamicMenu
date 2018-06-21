using DynamicMenu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DynamicMenu.Controllers
{
    public class SideBarController : Controller
    {
        // GET: SideBar
        public ActionResult Index()
        {
            List<Menu> menus = Menu.GetList("Azrul");
            return Json(menus, JsonRequestBehavior.AllowGet);
        }
    }
}