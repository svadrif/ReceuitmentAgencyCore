using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecruitmentAgencyCore.Data.Models;

namespace RecruitmentAgencyCore.Data.Mappings
{
    public class FamilyStatusMap : IEntityTypeConfiguration<FamilyStatus>
    {
        public void Configure(EntityTypeBuilder<FamilyStatus> builder)
        {
            builder.HasKey(f => f.Id);
        }
    }
}
