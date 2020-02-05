using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlayBall.GroupManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlayBall.GroupManagement.Data.Configurations
{
    internal class GroupEntityConfiguration : IEntityTypeConfiguration<GroupEntity>
    {
        public void Configure(EntityTypeBuilder<GroupEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder
                .Property(e => e.Id)
                .HasColumnName("Id");
                //.UseNpgsqlSerialColumn();
        }
    }
}
