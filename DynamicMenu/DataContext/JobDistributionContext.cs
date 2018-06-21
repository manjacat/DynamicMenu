using DynamicMenu.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DynamicMenu.DataContext
{
    public class JobDistributionContext
    {
        public static JobDistribution Get(string NoAduan)
        {
            SQLHelper helper = new DataContext.SQLHelper();
            string sqlString = "select Id, NoAduan, CreatedTime, Nama, AcceptedTime, ArrivalTime, ClosedTime, [Location] from JobDistribution "
                + "WHERE NoAduan = @NoAduan;";
            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("@NoAduan", NoAduan)
            };
            DataTable dt = helper.QueryTable(sqlString, parameter);
            JobDistribution jd = new JobDistribution();
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                jd = ConvertToModel(dr);
            }
            return jd;
        }

        private static JobDistribution ConvertToModel(DataRow dr)
        {
            JobDistribution jd = new JobDistribution();
            jd.Id = Convert.ToInt32(dr["Id"]);
            jd.Location = dr["Location"].ToString();
            jd.Nama = dr["Nama"].ToString();
            jd.NoAduan = dr["NoAduan"].ToString();
            if (dr["CreatedTime"] != DBNull.Value)
                jd.TimeStamp.CreatedTime = Convert.ToDateTime(dr["CreatedTime"]);

            if (dr["AcceptedTime"] != DBNull.Value)
                jd.TimeStamp.AcceptedTime = Convert.ToDateTime(dr["AcceptedTime"]);

            if (dr["ArrivalTime"] != DBNull.Value)
                jd.TimeStamp.ArrivalTime = Convert.ToDateTime(dr["ArrivalTime"]);

            if (dr["ClosedTime"] != DBNull.Value)
                jd.TimeStamp.ClosedTime = Convert.ToDateTime(dr["ClosedTime"]);
            return jd;
        }

        public static List<JobDistribution> GetList()
        {
            SQLHelper helper = new SQLHelper();
            string sqlString = "select Id,  Nama, NoAduan, CreatedTime, ArrivalTime, AcceptedTime, ClosedTime, [Location] from JobDistribution ";
            DataTable dt = helper.QueryTable(sqlString);
            List<JobDistribution> listJd = new List<JobDistribution>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    JobDistribution jd = ConvertToModel(dr);
                    listJd.Add(jd);
                }
            }
            return listJd;
        }
    }
}