using System.Data.Entity.ModelConfiguration;

namespace DevTimer.Domain.Entities.Mappings
{
    public class WorkTypeMap : EntityTypeConfiguration<WorkType>
    {
        public WorkTypeMap()
        {
            ToTable("WorkType");

            HasKey(t => t.ID);

            Property(e => e.Name).IsUnicode(false);

            HasMany(e => e.Works)
                .WithRequired(e => e.WorkType)
                .WillCascadeOnDelete(false);
        }
    }
}