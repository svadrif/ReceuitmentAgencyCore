using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecruitmentAgencyCore.Data.Models;

namespace RecruitmentAgencyCore.Data.Mappings
{
    public class ResumeMap : IEntityTypeConfiguration<Resume>
    {
        public void Configure(EntityTypeBuilder<Resume> builder)
        {
            builder.HasKey(r => r.Id);

            builder.HasOne(r => r.JobSeeker)
                   .WithMany(j => j.Resumes)
                   .HasForeignKey(r => r.JobSeekerId);

            builder.HasOne(r => r.Currency)
                   .WithMany()
                   .HasForeignKey(r => r.CurrencyId);

            builder.HasOne(r => r.Branch)
                   .WithMany()
                   .HasForeignKey(r => r.BranchId);

            builder.HasOne(r => r.Speciality)
                   .WithMany()
                   .HasForeignKey(r => r.SpecialityId);

            builder.HasOne(r => r.TypeOfEmployment)
                   .WithMany()
                   .HasForeignKey(r => r.TypeOfEmploymentId);

            builder.HasOne(r => r.Schedule)
                   .WithMany()
                   .HasForeignKey(r => r.ScheduleId);
        }
    }
}
