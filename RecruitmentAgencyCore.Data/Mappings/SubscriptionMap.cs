using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecruitmentAgencyCore.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitmentAgencyCore.Data.Mappings
{
    public class SubscriptionMap : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.HasKey(s => s.Id);

            builder.HasOne(s => s.JobSeeker)
                   .WithMany(j => j.Subscribes)
                   .HasForeignKey(s => s.JobSeekerId);

            builder.HasOne(s => s.Employer)
                   .WithMany(e => e.Subscribers)
                   .HasForeignKey(s => s.EmployerId);
        }
    }
}
