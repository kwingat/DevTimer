using System.Data.Entity.ModelConfiguration;

namespace DevTimer.Domain.Entities.Mappings
{
    public class ProjectMap : EntityTypeConfiguration<Project>
    {
        public ProjectMap()
        {
            ToTable("Project");

            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(e => e.Name).IsUnicode(false);
            Property(e => e.Description).IsUnicode(false);

            HasMany(e => e.Works)
                .WithRequired(e => e.Project)
                .WillCascadeOnDelete(false);
        }
    }
}