using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebInterfaceDataMeasurement.Data
{
    public partial class SqlprobeContext : DbContext
    {
        public SqlprobeContext()
        {
        }

        public SqlprobeContext(DbContextOptions<SqlprobeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Message> Messages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=sqldatabaseserver1htlneufelden.database.windows.net;Initial Catalog=sqlprobe;Persist Security Info=True;User ID=Gruppe2;Password=AsDf1234");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Message");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Co2).HasColumnName("co2");

                entity.Property(e => e.DeviceId).HasColumnName("deviceID");

                entity.Property(e => e.Dust).HasColumnName("dust");

                entity.Property(e => e.Humidity).HasColumnName("humidity");

                entity.Property(e => e.Ldr).HasColumnName("ldr");

                entity.Property(e => e.Noise).HasColumnName("noise");

                entity.Property(e => e.Temp).HasColumnName("temp");

                entity.Property(e => e.Timesent)
                    .HasColumnName("timesent")
                    .HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}