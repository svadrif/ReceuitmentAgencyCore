using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecruitmentAgencyCore.Data.Models;

namespace RecruitmentAgencyCore.Data.Mappings
{
    public class CitizenshipMap : IEntityTypeConfiguration<Citizenship>
    {
        public void Configure(EntityTypeBuilder<Citizenship> builder)
        {
            builder.HasKey(c => c.Id);
        }
    }
}
