using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlayBall.GroupManagement.Data.Entities;

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

            builder
                .Property(e => e.RowVersion)
                .HasColumnName("RowVersion")
                .IsRowVersion()
                .IsConcurrencyToken();
                
                //.HasColumnType("xid")
                //.ValueGeneratedOnAddOrUpdate()
                
        }
    }
}
