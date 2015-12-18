using System.Data.Entity.ModelConfiguration;

namespace DevTimer.Domain.Entities.Mappings
{
    public class WorkerMap : EntityTypeConfiguration<Worker>
    {
        public WorkerMap()
        {
            ToTable("Worker");

            HasKey(t => t.ID);

            
            Property(e => e.Name).IsUnicode(false);
            Property(e => e.Address1).IsUnicode(false);
            Property(e => e.Address2).IsUnicode(false);
            Property(e => e.City).IsUnicode(false);
            Property(e => e.Zip).IsUnicode(false);
            Property(e => e.Phone).IsUnicode(false);
            Property(e => e.Fax).IsUnicode(false);
        }
    }
}
