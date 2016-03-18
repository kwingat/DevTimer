namespace DevTimer.Domain.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class State
    {
        public State()
        {
            Workers = new HashSet<Worker>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(2)]
        public string StateCode { get; set; }

        [Required]
        [StringLength(128)]
        public string StateName { get; set; }
        
        public virtual ICollection<Worker> Workers { get; set; }
    }
}
