using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevTimer.Domain.Entities
{
    public partial class Client
    {
        public Client()
        {
            Projects = new HashSet<Project>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
            
        public virtual ICollection<Project> Projects { get; set; }
    }
}
