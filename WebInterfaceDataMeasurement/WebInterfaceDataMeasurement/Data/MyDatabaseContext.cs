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

        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<Device> Devices { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<RoomDevice> RoomDevices { get; set; }
        public virtual DbSet<RoomSubject> RoomSubjects { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=sqldatabaseserver1htlneufelden.database.windows.net;Initial Catalog=sqlprobe;Persist Security Info=True;User ID=Gruppe2;Password=AsDf1234; multipleactiveresultsets=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<Class>(entity =>
            {
                entity.ToTable("Class");

                entity.Property(e => e.ClassId)
                    .HasColumnName("ClassID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ClassName).HasMaxLength(50);
            });

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

                entity.Property(e => e.FkDeviceId).HasColumnName("fk_DeviceID");

                entity.Property(e => e.Humidity).HasColumnName("humidity");

                entity.Property(e => e.Ldr).HasColumnName("ldr");

                entity.Property(e => e.Noise).HasColumnName("noise");

                entity.Property(e => e.Noisemax).HasColumnName("noisemax");

                entity.Property(e => e.Noisemin).HasColumnName("noisemin");

                entity.Property(e => e.Noisequartal1).HasColumnName("noisequartal1");

                entity.Property(e => e.Noisequartal3).HasColumnName("noisequartal3");

                entity.Property(e => e.Temp).HasColumnName("temp");

                entity.Property(e => e.Timesent).HasColumnName("timesent");

                entity.HasOne(d => d.FkDevice)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.FkDeviceId)
                    .HasConstraintName("FK_Message_Device");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("Room");

                entity.Property(e => e.RoomId)
                    .HasColumnName("Room_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.NoiseConstant).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.RoomNr).IsRequired();
            });

            modelBuilder.Entity<RoomDevice>(entity =>
            {
                entity.ToTable("Room_Device");

                entity.Property(e => e.RoomDeviceId).HasColumnName("Room_Device_ID");

                entity.Property(e => e.FkDeviceId).HasColumnName("fk_DeviceID");

                entity.Property(e => e.FkRoomId).HasColumnName("fk_RoomID");

                entity.Property(e => e.ValidFrom).HasColumnType("date");

                entity.Property(e => e.ValidUntil).HasColumnType("date");

                entity.HasOne(d => d.FkDevice)
                    .WithMany(p => p.RoomDevices)
                    .HasForeignKey(d => d.FkDeviceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Room_Device_Device");

                entity.HasOne(d => d.FkRoom)
                    .WithMany(p => p.RoomDevices)
                    .HasForeignKey(d => d.FkRoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Room_Device_Room");
            });

            modelBuilder.Entity<RoomSubject>(entity =>
            {
                entity.ToTable("RoomSubject");

                entity.Property(e => e.RoomSubjectId).HasColumnName("RoomSubjectID");

                entity.Property(e => e.Day).HasColumnType("date");

                entity.Property(e => e.FkClassId).HasColumnName("fk_ClassID");

                entity.Property(e => e.FkRoomId).HasColumnName("fk_RoomID");

                entity.Property(e => e.FkSubjectId).HasColumnName("fk_SubjectID");

                entity.Property(e => e.FkTeacherId).HasColumnName("fk_TeacherID");

                entity.Property(e => e.FkTeachers2Id).HasColumnName("fk_Teachers2ID");

                entity.HasOne(d => d.FkClass)
                    .WithMany(p => p.RoomSubjects)
                    .HasForeignKey(d => d.FkClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoomSubject_Class");

                entity.HasOne(d => d.FkRoom)
                    .WithMany(p => p.RoomSubjects)
                    .HasForeignKey(d => d.FkRoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoomSubject_Room");

                entity.HasOne(d => d.FkSubject)
                    .WithMany(p => p.RoomSubjects)
                    .HasForeignKey(d => d.FkSubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoomSubject_Subject");

                entity.HasOne(d => d.FkTeacher)
                    .WithMany(p => p.RoomSubjectFkTeachers)
                    .HasForeignKey(d => d.FkTeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoomSubject_Teacher");

                entity.HasOne(d => d.FkTeachers2)
                    .WithMany(p => p.RoomSubjectFkTeachers2)
                    .HasForeignKey(d => d.FkTeachers2Id)
                    .HasConstraintName("FK_RoomSubject_Teacher2");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.ToTable("Subject");

                entity.Property(e => e.SubjectId)
                    .HasColumnName("SubjectID")
                    .ValueGeneratedNever();

                entity.Property(e => e.SubjectNameshort).HasMaxLength(50);
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.ToTable("Teacher");

                entity.Property(e => e.TeacherId)
                    .HasColumnName("TeacherID")
                    .ValueGeneratedNever();

                entity.Property(e => e.TeacherName).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}