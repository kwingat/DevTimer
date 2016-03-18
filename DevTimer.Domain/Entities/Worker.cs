namespace DevTimer.Domain.Entities
{
    using System.ComponentModel.DataAnnotations;

    public partial class Worker
    {
        public int ID { get; set; }

        [Required]
        [StringLength(128)]
        public string UserID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(64)]
        public string Address1 { get; set; }

        [StringLength(64)]
        public string Address2 { get; set; }

        [StringLength(32)]
        public string City { get; set; }

        public int? StateID { get; set; }

        [StringLength(10)]
        public string Zip { get; set; }

        [StringLength(32)]
        public string Phone { get; set; }

        [StringLength(32)]
        public string Fax { get; set; }

        public int? WorkerType { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }

        public virtual State State { get; set; }

        public virtual WorkerType WorkerType1 { get; set; }
    }
}
