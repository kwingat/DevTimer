using System.Data.Entity.ModelConfiguration;

namespace DevTimer.Domain.Entities.Mappings
{
    public class StateMap : EntityTypeConfiguration<State>
    {
        public StateMap()
        {
            ToTable("State");

            HasKey(t => t.ID);

            Property(e => e.StateCode).IsFixedLength();
        }
    }
}