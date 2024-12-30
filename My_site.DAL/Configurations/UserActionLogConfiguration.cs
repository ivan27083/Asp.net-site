using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using My_site.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_site.DAL.Configurations
{
    public partial class UserActionLogConfiguration : IEntityTypeConfiguration<UserActionLog>
    {
        public void Configure(EntityTypeBuilder<UserActionLog> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

            builder.Property(x => x.Username)
                .IsRequired();

            builder.Property(x => x.Action)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Controller)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Method)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.Timestamp)
                .IsRequired()
                .HasDefaultValueSql("NOW()");

            builder.Property(x => x.Details)
                .HasMaxLength(500);

            builder.ToTable("UserActionLogs");
        }
    }
}
