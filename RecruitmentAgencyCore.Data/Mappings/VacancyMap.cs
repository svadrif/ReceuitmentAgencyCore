using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecruitmentAgencyCore.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitmentAgencyCore.Data.Mappings
{
    public class VacancyMap : IEntityTypeConfiguration<Vacancy>
    {
        public void Configure(EntityTypeBuilder<Vacancy> builder)
        {
            builder.HasKey(v => v.Id);

            builder.HasOne(v => v.Currency)
                   .WithMany()
                   .HasForeignKey(v => v.CurrencyId);

            builder.HasOne(v => v.Employer)
                   .WithMany(e => e.Vacancies)
                   .HasForeignKey(v => v.EmployerId);

            builder.HasOne(v => v.Branch)
                 .WithMany()
                 .HasForeignKey(v => v.BranchId);

            builder.HasOne(v => v.Experience)
                 .WithMany()
                 .HasForeignKey(v => v.ExperienceId);

            builder.HasOne(v => v.Schedule)
                .WithMany()
                .HasForeignKey(v => v.ScheduleId);

            builder.HasOne(v => v.EducationType)
                .WithMany()
                .HasForeignKey(v => v.EducationTypeId);
        }
    }
}
