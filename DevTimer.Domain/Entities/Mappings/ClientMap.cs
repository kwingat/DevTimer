using System.Data.Entity.ModelConfiguration;

namespace DevTimer.Domain.Entities.Mappings
{
    public class ClientMap : EntityTypeConfiguration<Client>
    {
        public ClientMap()
        {
            ToTable("Client");

            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(e => e.Name).IsUnicode(false);
        }
    }
}
