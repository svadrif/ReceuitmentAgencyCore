using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecruitmentAgencyCore.Data.Models;

namespace RecruitmentAgencyCore.Data.Mappings
{
    public class ResourceMap : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            builder.HasKey(r => r.Id);

            builder.HasOne(r => r.Culture)
                .WithMany(c => c.Resources)
                .HasForeignKey(r => r.CultureId);
        }
    }
}
