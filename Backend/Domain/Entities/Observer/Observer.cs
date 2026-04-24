using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminiSchool.Domain.Entities.Observer
{
    public enum ObserverType { Academic, Disciplinary, Recognition, General }

    public class ObserverEntity
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid TeacherId { get; set; }
        public ObserverType Type { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public Student.StudentEntity? Student { get; set; }
        public Teacher.TeacherEntity? Teacher { get; set; }
    }
}
