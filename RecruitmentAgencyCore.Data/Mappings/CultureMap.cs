using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecruitmentAgencyCore.Data.Models;

namespace RecruitmentAgencyCore.Data.Mappings
{
    public class CultureMap : IEntityTypeConfiguration<Culture>
    {
        public void Configure(EntityTypeBuilder<Culture> builder)
        {
            builder.HasKey(c => c.Id);
        }
    }
}
