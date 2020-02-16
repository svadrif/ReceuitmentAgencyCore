using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecruitmentAgencyCore.Data.Models;

namespace RecruitmentAgencyCore.Data.Mappings
{
    public class JobSeekerForeignLanguageMap : IEntityTypeConfiguration<JobSeekerForeignLanguage>
    {
        public void Configure(EntityTypeBuilder<JobSeekerForeignLanguage> builder)
        {
            builder.HasKey(jf => jf.Id);

            builder.HasOne(jf => jf.JobSeeker)
                   .WithMany(j => j.JobSeekerForeignLanguages)
                   .HasForeignKey(jf => jf.JobSeekerId);

            builder.HasOne(jf => jf.ForeignLanguage)
                   .WithMany()
                   .HasForeignKey(jf => jf.ForeignLanguageId);

            builder.HasOne(jf => jf.LanguageLevel)
                   .WithMany()
                   .HasForeignKey(jf => jf.LanguageLevelId);


        }
    }
}
