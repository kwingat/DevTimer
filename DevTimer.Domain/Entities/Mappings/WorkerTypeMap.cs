using System.Data.Entity.ModelConfiguration;

namespace DevTimer.Domain.Entities.Mappings
{
    public class WorkerTypeMap : EntityTypeConfiguration<WorkerType>
    {
        public WorkerTypeMap()
        {
            ToTable("WorkerType");

            HasKey(t => t.ID);

            Property(e => e.Type).IsUnicode(false);
            
            HasMany(e => e.Worker)
                .WithOptional(e => e.WorkerType1)
                .HasForeignKey(e => e.WorkerType);
        }
    }
}