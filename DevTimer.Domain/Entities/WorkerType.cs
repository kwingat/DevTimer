using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevTimer.Domain.Entities
{
    public class WorkerType
    {
        public WorkerType()
        {
            Worker = new HashSet<Worker>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Type { get; set; }
        
        public virtual ICollection<Worker> Worker { get; set; }
    }
}
