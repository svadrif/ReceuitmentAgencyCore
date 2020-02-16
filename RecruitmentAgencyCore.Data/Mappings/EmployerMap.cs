using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecruitmentAgencyCore.Data.Models;

namespace RecruitmentAgencyCore.Data.Mappings
{
    public class EmployerMap : IEntityTypeConfiguration<Employer>
    {
        public void Configure(EntityTypeBuilder<Employer> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.User)
                .WithMany(u => u.Employers)
                .HasForeignKey(e => e.UserId);

            builder.HasOne(e => e.Country)
                .WithMany()
                .HasForeignKey(e => e.CountryId);


            builder.HasOne(e => e.Region)
                .WithMany()
                .HasForeignKey(e => e.RegionId);


            builder.HasOne(e => e.District)
                .WithMany()
                .HasForeignKey(e => e.DistrictId);
        }
    }
}
