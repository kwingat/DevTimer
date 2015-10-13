using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevTimer.Domain.Entities
{
    public partial class WorkType
    {
        public WorkType()
        {
            Works = new HashSet<Work>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Work> Works { get; set; }
    }
}