using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecruitmentAgencyCore.Data.Models;

namespace RecruitmentAgencyCore.Data.Mappings
{
    public class JobSeekerMap : IEntityTypeConfiguration<JobSeeker>
    {
        public void Configure(EntityTypeBuilder<JobSeeker> builder)
        {
            builder.HasKey(j => j.Id);

            builder.HasOne(j => j.User)
                   .WithMany(u => u.JobSeekers)
                   .HasForeignKey(j => j.UserId);

            builder.HasOne(j => j.Citizenship)
                   .WithMany(c => c.JobSeekers)
                   .HasForeignKey(j => j.CitizenshipId);

            builder.HasOne(j => j.SocialStatus)
                   .WithMany(s => s.JobSeekers)
                   .HasForeignKey(j => j.SocialStatusId);

            builder.HasOne(j => j.FamilyStatus)
                   .WithMany(f => f.JobSeekers)
                   .HasForeignKey(j => j.FamilyStatusId);

            builder.HasOne(j => j.Gender)
                   .WithMany(g => g.JobSeekers)
                   .HasForeignKey(j => j.GenderId);

            builder.HasOne(j => j.Country)
                   .WithMany(c => c.JobSeekers)
                   .HasForeignKey(j => j.CountryId);

            builder.HasOne(j => j.Region)
                   .WithMany(r => r.JobSeekers)
                   .HasForeignKey(j => j.RegionId);

            builder.HasOne(j => j.District)
                   .WithMany(d => d.JobSeekers)
                   .HasForeignKey(j => j.DistrictId);

        }
    }
}
