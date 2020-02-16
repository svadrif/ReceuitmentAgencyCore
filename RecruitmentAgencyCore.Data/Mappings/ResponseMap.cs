using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecruitmentAgencyCore.Data.Models;

namespace RecruitmentAgencyCore.Data.Mappings
{
    public class ResponseMap : IEntityTypeConfiguration<Response>
    {
        public void Configure(EntityTypeBuilder<Response> builder)
        {
            builder.HasKey(r => r.Id);

            builder.HasOne(r => r.Vacancy)
                   .WithMany(v => v.Responses)
                   .HasForeignKey(r => r.VacancyId);

            builder.HasOne(r => r.JobSeeker)
                   .WithMany(j => j.Responses)
                   .HasForeignKey(r => r.JobSeekerId);
        }
    }
}
