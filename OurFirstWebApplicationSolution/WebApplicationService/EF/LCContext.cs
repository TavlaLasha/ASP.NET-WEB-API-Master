namespace WebApplicationService.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class LCContext : DbContext
    {
        public LCContext()
            : base("name=LCContext")
        {
        }

        public virtual DbSet<CourseLocation> CourseLocations { get; set; }
        public virtual DbSet<Cours> Courses { get; set; }
        public virtual DbSet<CourseStream> CourseStreams { get; set; }
        public virtual DbSet<Curriculum> Curricula { get; set; }
        public virtual DbSet<Lecturer> Lecturers { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Registration> Registrations { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cours>()
                .HasMany(e => e.CourseStreams)
                .WithRequired(e => e.Cours)
                .HasForeignKey(e => e.CourseID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CourseStream>()
                .HasMany(e => e.CourseLocations)
                .WithRequired(e => e.CourseStream)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CourseStream>()
                .HasMany(e => e.Registrations)
                .WithRequired(e => e.CourseStream)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Curriculum>()
                .HasMany(e => e.Courses)
                .WithRequired(e => e.Curriculum)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Lecturer>()
                .HasMany(e => e.Curricula)
                .WithRequired(e => e.Lecturer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Location>()
                .HasMany(e => e.CourseLocations)
                .WithRequired(e => e.Location)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Location>()
                .HasMany(e => e.Locations1)
                .WithOptional(e => e.Location1)
                .HasForeignKey(e => e.ParentID);

            modelBuilder.Entity<Subject>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<Subject>()
                .HasMany(e => e.Curricula)
                .WithRequired(e => e.Subject)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.IDNumber)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Registrations)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.StudentID)
                .WillCascadeOnDelete(false);
        }
    }
}
