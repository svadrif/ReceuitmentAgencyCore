using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecruitmentAgencyCore.Data.Models;


namespace RecruitmentAgencyCore.Data.Mappings
{
    public class LoggingMap : IEntityTypeConfiguration<Logging>
    {
        public void Configure(EntityTypeBuilder<Logging> builder)
        {
            builder.HasKey(l => l.Id);

            builder.HasOne(l => l.User)
                .WithMany()
                .HasForeignKey(l => l.UserId);
        }
    }
}
