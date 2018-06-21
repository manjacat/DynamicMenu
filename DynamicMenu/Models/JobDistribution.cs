using DynamicMenu.DataContext;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace DynamicMenu.Models
{
    public class JobDistribution
    {
        public JobDistribution()
        {
            TimeStamp = new TimeStamp();
        }

        public int Id { get; set; }

        [DisplayName("No Aduan")]
        public string NoAduan { get; set; }

        [DisplayName("Alamat")]
        public string Location { get; set; }

        [DisplayName("Nama Pengadu")]
        public string Nama { get; set; }
        public TimeStamp TimeStamp { get; set; }

        [DisplayName("Response Time")]
        public TimeSpan ResponseTime
        {
            get
            {
                if (TimeStamp.ArrivalTime != null && TimeStamp.CreatedTime != null)
                {
                    DateTime arrival = (DateTime)TimeStamp.ArrivalTime;
                    DateTime created = (DateTime)TimeStamp.CreatedTime;
                    return arrival.Subtract(created);
                }
                return new TimeSpan(0);
            }
        }

        public static JobDistribution Get(string noAduan)
        {
            JobDistribution jd = JobDistributionContext.Get(noAduan);
            return jd;
        }

        public static List<JobDistribution> GetList()
        {
            return JobDistributionContext.GetList();
        }

    }

    public class TimeStamp
    {
        [DisplayName("Created Time")]
        public DateTime? CreatedTime { get; set; }

        [DisplayName("Accepted Time")]
        public DateTime? AcceptedTime { get; set; }

        [DisplayName("Arrival Time")]
        public DateTime? ArrivalTime { get; set; }

        [DisplayName("Closed Time")]
        public DateTime? ClosedTime { get; set; }
    }

    public class Test
    {
        //TODO: Commit Test

    }
}