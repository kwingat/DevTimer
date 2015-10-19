using System.Data.Entity.ModelConfiguration;

namespace DevTimer.Domain.Entities.Mappings
{
    public class AspNetUserMap : EntityTypeConfiguration<AspNetUser>
    {
        public AspNetUserMap()
        {
            ToTable("AspNetUsers");

            HasKey(t => t.Id);

            Property(e => e.Email).IsUnicode(false);
            Property(e => e.EmailConfirmed);
            Property(e => e.PasswordHash).IsUnicode(false);
            Property(e => e.SecurityStamp).IsUnicode(false);
            Property(e => e.PhoneNumber).IsUnicode(false);
            Property(e => e.PhoneNumberConfirmed);
            Property(e => e.LockoutEnabled);
            Property(e => e.LockoutEndDateUtc); 
            Property(e => e.LockoutEnabled);
            Property(e => e.AccessFailedCount);
            Property(e => e.UserName).IsUnicode(false);

            HasMany(e => e.Works)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

        }
    }
}