namespace LuminiSchool.Domain.Model.GradeSubjectTeacher.DTOs
{
    public class GradeSubjectTeacherDto
    {
        public Guid   Id          { get; set; }
        public Guid   GradeId     { get; set; }
        public string GradeName   { get; set; } = string.Empty;
        public Guid   SubjectId   { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public Guid   TeacherId   { get; set; }
        public string TeacherName { get; set; } = string.Empty;
    }
}
