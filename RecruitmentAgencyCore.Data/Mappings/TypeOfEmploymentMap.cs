using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecruitmentAgencyCore.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitmentAgencyCore.Data.Mappings
{
    public class TypeOfEmploymentMap : IEntityTypeConfiguration<TypeOfEmployment>
    {
        public void Configure(EntityTypeBuilder<TypeOfEmployment> builder)
        {
            builder.HasKey(t => t.Id);
        }
    }
}
