using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecruitmentAgencyCore.Data.Models;

namespace RecruitmentAgencyCore.Data.Mappings
{
    public class PreviousWorkMap : IEntityTypeConfiguration<PreviousWork>
    {
        public void Configure(EntityTypeBuilder<PreviousWork> builder)
        {
            builder.HasKey(pw => pw.Id);

            builder.HasOne(pw => pw.JobSeeker)
                .WithMany(j => j.PreviousWorkList)
                .HasForeignKey(pw => pw.JobSeekerId);

        }
    }
}
