using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecruitmentAgencyCore.Data.Models;

namespace RecruitmentAgencyCore.Data.Mappings
{
    public class MenuRolePermissionMap : IEntityTypeConfiguration<MenuRolePermission>
    {
        public void Configure(EntityTypeBuilder<MenuRolePermission> builder)
        {
            builder.HasKey(mrp => mrp.Id);

            builder.HasOne(mrp => mrp.Role)
                   .WithMany()
                   .HasForeignKey(mrp => mrp.RoleId);

            builder.HasOne(mrp => mrp.Menu)
                   .WithMany()
                   .HasForeignKey(mrp => mrp.MenuId);

            builder.HasOne(mrp => mrp.Permission)
                   .WithMany()
                   .HasForeignKey(mrp => mrp.PermissionId);
        }
    }
}
