using DynamicMenu.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DynamicMenu.DataContext
{
    public class MenuContext
    {
        public static List<Menu> GetMenu(string user)
        {
            List<Menu> menus = new List<Menu>();
            SQLHelper helper = new SQLHelper();
            string sqlString = "SELECT Id, Name, Hyperlink, TotalRecord, IsVisible, ParentId, SortId FROM Menu WHERE ParentId = 0 ORDER BY SortId;";
            sqlString += "SELECT Id, Name, Hyperlink, TotalRecord, IsVisible, ParentId, SortId FROM Menu WHERE ParentId > 0 ORDER BY ParentId, SortId; ";
            DataSet ds = new DataSet();
            ds = helper.QuerySet(sqlString);
            DataTable dtParent = ds.Tables[0];
            DataTable dtSubMenu = ds.Tables[1];
            foreach (DataRow dr in dtParent.Rows)
            {
                Menu m = new Menu()
                {
                    ID = Convert.ToInt32(dr["ID"]),
                    Name = dr["Name"].ToString(),
                    Hyperlink = dr["Hyperlink"].ToString(),
                    TotalRecord = Convert.ToInt32(dr["TotalRecord"]),
                    IsVisible = Convert.ToBoolean(dr["IsVisible"])
                };
                DataView dv = new DataView(dtSubMenu);
                dv.RowFilter = "ParentId = " + m.ID;
                foreach (DataRowView drSub in dv)
                {
                    Menu sub = new Menu()
                    {
                        ID = Convert.ToInt32(drSub["ID"]),
                        Name = drSub["Name"].ToString(),
                        Hyperlink = drSub["Hyperlink"].ToString(),
                        TotalRecord = Convert.ToInt32(drSub["TotalRecord"]),
                        IsVisible = Convert.ToBoolean(drSub["IsVisible"])
                    };
                    m.SubMenu.Add(sub);
                }
                menus.Add(m);
            }
            return menus;
        }
    }
}