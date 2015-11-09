using System;

namespace DevTimer.Models
{
    public class TimeTracker
    {
        public DateTime Date { get; set; }
        public double NewDev { get; set; }
        public double Maint { get; set; }
        public double Admin { get; set; }
        public double PTO { get; set; }
        public double DayTotal { get; set; }
        public string Notes { get; set; }
    }
}