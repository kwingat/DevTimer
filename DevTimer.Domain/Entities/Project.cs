﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevTimer.Domain.Entities
{
    public partial class Project
    {
        public Project()
        {
            Works = new HashSet<Work>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        public int ClientID { get; set; }

        public virtual Client Client { get; set; }

        public virtual ICollection<Work> Works { get; set; }
    }
}