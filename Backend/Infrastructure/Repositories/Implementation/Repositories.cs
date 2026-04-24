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
using LuminiSchool.Infrastructure.Repositories.Contract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LuminiSchool.Infrastructure.Repositories.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _ctx;
        protected readonly DbSet<T> _db;
        public GenericRepository(ApplicationDbContext ctx) { _ctx = ctx; _db = ctx.Set<T>(); }
        public async Task<T?> GetByIdAsync(Guid id) => await _db.FindAsync(id);
        public async Task<IEnumerable<T>> GetAllAsync() => await _db.ToListAsync();
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> p) => await _db.Where(p).ToListAsync();
        public async Task<T> AddAsync(T e) { await _db.AddAsync(e); await _ctx.SaveChangesAsync(); return e; }
        public async Task UpdateAsync(T e) { _db.Update(e); await _ctx.SaveChangesAsync(); }
        public async Task DeleteAsync(Guid id) { var e = await _db.FindAsync(id); if (e != null) { _db.Remove(e); await _ctx.SaveChangesAsync(); } }
        public async Task<bool> ExistsAsync(Guid id) => await _db.FindAsync(id) != null;
        public async Task<int> CountAsync(Expression<Func<T, bool>>? p = null) => p == null ? await _db.CountAsync() : await _db.CountAsync(p);
    }
}
