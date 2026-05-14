namespace LuminiSchool.Domain.Model.GradeSubjectTeacher.DTOs
{
    public class CreateGradeSubjectTeacherDto
    {
        public Guid GradeId   { get; set; }
        public Guid SubjectId { get; set; }
        public Guid TeacherId { get; set; }
    }

    public class UpdateGradeSubjectTeacherDto
    {
        public Guid TeacherId { get; set; }
    }
}
