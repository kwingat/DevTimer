using System.Collections.Generic;

namespace DevTimer.Domain.Entities
{
    public partial class Project
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ClientID { get; set; }

        public virtual Client Client { get; set; }
    }
}