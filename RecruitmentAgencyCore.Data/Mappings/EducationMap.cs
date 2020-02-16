using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecruitmentAgencyCore.Data.Models;

namespace RecruitmentAgencyCore.Data.Mappings
{
    public class EducationMap : IEntityTypeConfiguration<Education>
    {
        public void Configure(EntityTypeBuilder<Education> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.JobSeeker)
                .WithMany(j => j.Educations)
                .HasForeignKey(e => e.JobSeekerId);

            builder.HasOne(e => e.University)
                .WithMany()
                .HasForeignKey(e => e.UniversityId);

            builder.HasOne(e => e.EducationType)
                .WithMany()
                .HasForeignKey(e => e.EducationTypeId);

        }
    }
}
