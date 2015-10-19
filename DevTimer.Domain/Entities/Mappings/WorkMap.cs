using System.Data.Entity.ModelConfiguration;

namespace DevTimer.Domain.Entities.Mappings
{
    public class WorkMap : EntityTypeConfiguration<Work>
    {
        public WorkMap()
        {
            ToTable("Work");

            HasKey(t => t.ID);

            Property(e => e.StartTime).HasPrecision(0);
            Property(e => e.EndTime).HasPrecision(0);
            Property(e => e.Hours).HasPrecision(2, 0);
        }
    }
}