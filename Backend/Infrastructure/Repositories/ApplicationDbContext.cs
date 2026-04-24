using LuminiSchool.Domain.Entities.User;
using LuminiSchool.Domain.Entities.Student;
using LuminiSchool.Domain.Entities.Teacher;
using LuminiSchool.Domain.Entities.Guardian;
using LuminiSchool.Domain.Entities.Subject;
using LuminiSchool.Domain.Entities.Grade;
using LuminiSchool.Domain.Entities.AcademicPeriod;
using LuminiSchool.Domain.Entities.Enrollment;
using LuminiSchool.Domain.Entities.ClassPlanner;
using LuminiSchool.Domain.Entities.Activity;
using LuminiSchool.Domain.Entities.Attendance;
using LuminiSchool.Domain.Entities.GradeRecord;
using LuminiSchool.Domain.Entities.Bulletin;
using LuminiSchool.Domain.Entities.Schedule;
using LuminiSchool.Domain.Entities.Observer;
using LuminiSchool.Domain.Entities.Achievement;
using LuminiSchool.Domain.Entities.Certificate;
using LuminiSchool.Domain.Entities.SchoolRepresentative;
using LuminiSchool.Domain.Entities.Notification;
using LuminiSchool.Domain.Entities.Message;
using LuminiSchool.Domain.Entities.Report;
using LuminiSchool.Domain.Entities.DiagnosticTest;
using LuminiSchool.Domain.Entities.IcfesSimulator;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LuminiSchool.Infrastructure.Repositories
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<StudentEntity>             Students              { get; set; }
        public DbSet<TeacherEntity>             Teachers              { get; set; }
        public DbSet<GuardianEntity>            Guardians             { get; set; }
        public DbSet<SubjectEntity>             Subjects              { get; set; }
        public DbSet<GradeEntity>               Grades                { get; set; }
        public DbSet<AcademicPeriodEntity>      AcademicPeriods       { get; set; }
        public DbSet<EnrollmentEntity>          Enrollments           { get; set; }
        public DbSet<ClassPlannerEntity>        ClassPlanners         { get; set; }
        public DbSet<ActivityEntity>            Activities            { get; set; }
        public DbSet<ActivitySubmissionEntity>  ActivitySubmissions   { get; set; }
        public DbSet<AttendanceEntity>          Attendances           { get; set; }
        public DbSet<GradeRecordEntity>         GradeRecords          { get; set; }
        public DbSet<BulletinEntity>            Bulletins             { get; set; }
        public DbSet<ScheduleEntity>            Schedules             { get; set; }
        public DbSet<ObserverEntity>            Observers             { get; set; }
        public DbSet<AchievementEntity>         Achievements          { get; set; }
        public DbSet<CertificateEntity>         Certificates          { get; set; }
        public DbSet<SchoolRepresentativeEntity> SchoolRepresentatives{ get; set; }
        public DbSet<NotificationEntity>        Notifications         { get; set; }
        public DbSet<MessageEntity>             Messages              { get; set; }
        public DbSet<ReportEntity>              Reports               { get; set; }
        public DbSet<DiagnosticTestEntity>      DiagnosticTests       { get; set; }
        public DbSet<DiagnosticResultEntity>    DiagnosticResults     { get; set; }
        public DbSet<IcfesSimulatorEntity>      IcfesSimulators       { get; set; }
        public DbSet<IcfesResultEntity>         IcfesResults          { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<ApplicationRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens");

            builder.Entity<GuardianEntity>().HasMany(g => g.Students).WithMany(s => s.Guardians).UsingEntity(j => j.ToTable("GuardianStudents"));
            builder.Entity<TeacherEntity>().HasMany(t => t.Subjects).WithMany(s => s.Teachers).UsingEntity(j => j.ToTable("TeacherSubjects"));
            builder.Entity<GradeEntity>().HasMany(g => g.Students).WithMany(s => s.Grades).UsingEntity(j => j.ToTable("GradeStudents"));
            builder.Entity<GradeEntity>().HasMany(g => g.Subjects).WithMany().UsingEntity(j => j.ToTable("GradeSubjects"));
            builder.Entity<GradeEntity>().HasMany(g => g.Teachers).WithMany().UsingEntity(j => j.ToTable("GradeTeachers"));

            builder.Entity<GradeRecordEntity>().Property(g => g.Score).HasPrecision(5, 2);
            builder.Entity<GradeRecordEntity>().Property(g => g.Average).HasPrecision(5, 2);
            builder.Entity<ActivityEntity>().Property(a => a.MaxScore).HasPrecision(5, 2);
            builder.Entity<ActivitySubmissionEntity>().Property(a => a.Score).HasPrecision(5, 2);
            builder.Entity<BulletinEntity>().Property(b => b.GeneralAverage).HasPrecision(5, 2);
            builder.Entity<DiagnosticResultEntity>().Property(d => d.Score).HasPrecision(5, 2);
            builder.Entity<IcfesResultEntity>().Property(i => i.TotalScore).HasPrecision(5, 2);
        }
    }
}
