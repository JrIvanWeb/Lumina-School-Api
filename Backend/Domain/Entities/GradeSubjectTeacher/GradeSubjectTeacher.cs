using LuminiSchool.Domain.Entities.Grade;
using LuminiSchool.Domain.Entities.Subject;
using LuminiSchool.Domain.Entities.Teacher;

namespace LuminiSchool.Domain.Entities.GradeSubjectTeacher
{
    public class GradeSubjectTeacherEntity
    {
        public Guid Id        { get; set; }
        public Guid GradeId   { get; set; }
        public Guid SubjectId { get; set; }
        public Guid TeacherId { get; set; }

        public GradeEntity   Grade   { get; set; } = null!;
        public SubjectEntity Subject { get; set; } = null!;
        public TeacherEntity Teacher { get; set; } = null!;
    }
}
