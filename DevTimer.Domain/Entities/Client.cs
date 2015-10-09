using System.ComponentModel.DataAnnotations;

namespace DevTimer.Domain.Entities
{
    public partial class Client
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
