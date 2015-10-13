﻿using System;
using System.ComponentModel.DataAnnotations;

namespace DevTimer.Domain.Entities
{
    public partial class Work
    {
        public int ID { get; set; }

        public int ProjectID { get; set; }

        public int WorkTypeID { get; set; }

        [Required]
        [StringLength(128)]
        public string UserID { get; set; }

        public TimeSpan? StartTime { get; set; }

        public TimeSpan? EndTime { get; set; }

        public decimal? Hours { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }

        public virtual Project Project { get; set; }

        public virtual WorkType WorkType { get; set; }
    }
}