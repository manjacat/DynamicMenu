using DynamicMenu.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicMenu.Models
{
    public class Menu
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int TotalRecord { get; set; }
        public string Hyperlink { get; set; }
        public List<Menu> SubMenu { get; set; }
        public bool IsVisible { get; set; }
        public string ElementID
        {
            get
            {
                return string.Format("child{0}", ID.ToString());
            }
        }

        private int counter { get; set; }
        public Menu()
        {
            counter = counter + 1;
            ID = counter;
            Hyperlink = "/Home/Index";
            TotalRecord = 0;
            IsVisible = true;
            SubMenu = new List<Menu>();
            //Hello World
        }

        public static List<Menu> GetList(string user)
        {
            List<Menu> menus = MenuContext.GetMenu(user);
            return menus;
        }
    }
}