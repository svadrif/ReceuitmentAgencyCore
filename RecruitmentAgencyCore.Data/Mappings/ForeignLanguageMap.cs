using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecruitmentAgencyCore.Data.Models;

namespace RecruitmentAgencyCore.Data.Mappings
{
    public class ForeignLanguageMap : IEntityTypeConfiguration<ForeignLanguage>
    {
        public void Configure(EntityTypeBuilder<ForeignLanguage> builder)
        {
            builder.HasKey(f => f.Id);
        }
    }
}
