using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebInterfaceDataMeasurement.Data
{
    public partial class MyDatabaseContext : DbContext
    {
        public MyDatabaseContext()
        {
        }

        public MyDatabaseContext(DbContextOptions<MyDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Device> Devices { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<RaumDevice> RaumDevices { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=sqldatabaseserver1htlneufelden.database.windows.net;Initial Catalog=sqlprobe;Persist Security Info=True;User ID=Gruppe2;Password=AsDf1234");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<Device>(entity =>
            {
                entity.ToTable("Device");

                entity.Property(e => e.DeviceId).HasColumnName("Device_ID");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Message");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Co2).HasColumnName("co2");

                entity.Property(e => e.DeviceId).HasColumnName("deviceID");

                entity.Property(e => e.Dust).HasColumnName("dust");

                entity.Property(e => e.FkDeviceId).HasColumnName("fk_deviceID");

                entity.Property(e => e.Humidity).HasColumnName("humidity");

                entity.Property(e => e.Ldr).HasColumnName("ldr");

                entity.Property(e => e.Noise).HasColumnName("noise");

                entity.Property(e => e.Temp).HasColumnName("temp");

                entity.Property(e => e.Timesent)
                    .HasColumnName("timesent")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.FkDevice)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.FkDeviceId)
                    .HasConstraintName("FK_Message_Device");
            });

            modelBuilder.Entity<RaumDevice>(entity =>
            {
                entity.ToTable("Raum_Device");

                entity.Property(e => e.RaumDeviceId).HasColumnName("Raum_Device_ID");

                entity.Property(e => e.DeviceId).HasColumnName("DeviceID");

                entity.Property(e => e.RaumId).HasColumnName("RaumID");

                entity.Property(e => e.ValidFrom).HasColumnType("date");

                entity.Property(e => e.ValidUntil).HasColumnType("date");

                entity.HasOne(d => d.Device)
                    .WithMany(p => p.RaumDevices)
                    .HasForeignKey(d => d.DeviceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Raum_Device_Device");

                entity.HasOne(d => d.Raum)
                    .WithMany(p => p.RaumDevices)
                    .HasForeignKey(d => d.RaumId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Raum_Device_Raum_Device");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("Room");

                entity.Property(e => e.RoomId).HasColumnName("Room_ID");

                entity.Property(e => e.NoiseConstant).HasColumnType("decimal(18, 0)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}