using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DevTimer.Models
{
    public class TimeTracker
    {
        [DisplayName("Date")]
        public DateTime Date { get; set; }

        [DisplayName ("New Dev")]
        public double NewDev { get; set; }

        [DisplayName("Maint")]
        public double Maint { get; set; }

        [DisplayName("Admin/Other")]
        public double Admin { get; set; }

        [DisplayName("PTO")]
        public double PTO { get; set; }

        [DisplayName("Day Total")]
        public double DayTotal { get; set; }

        [DisplayName("Notes")]
        public string Notes { get; set; }
    }
}