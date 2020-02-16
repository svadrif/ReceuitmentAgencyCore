using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecruitmentAgencyCore.Data.Models;

namespace RecruitmentAgencyCore.Data.Mappings
{
    public class DistrictMap : IEntityTypeConfiguration<District>
    {
        public void Configure(EntityTypeBuilder<District> builder)
        {
            builder.HasKey(d => d.Id);

            builder.HasOne(d => d.Region)
                   .WithMany(r => r.Districts)
                   .HasForeignKey(d => d.RegionId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
