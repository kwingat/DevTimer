using System;
using AutoMapper;
using DevTimer.Core;
using DevTimer.Domain.Entities;

namespace DevTimer.Models
{
    public class WorkListViewModel : IHasCustomMapping
    {
        public int ID { get; set; }

        public int ProjectID { get; set; }

        public int WorkTypeID { get; set; }

        public string UserID { get; set; }

        public TimeSpan? StartTime { get; set; }

        public TimeSpan? EndTime { get; set; }

        public decimal? Hours { get; set; }

        public string AspNetUser { get; set; }

        public string Project { get; set; }

        public string WorkType { get; set; }

        public void CreateMapping(IConfiguration configuration)
        {
            throw new System.NotImplementedException();
        }
    }
}