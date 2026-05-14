namespace LuminiSchool.Domain.Model.Subject.DTOs
{
    /// <summary>Datos resumidos de un grado asociado a la asignatura.</summary>
    public class SubjectGradeDto
    {
        public Guid   Id   { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class SubjectDto
    {
        public Guid    Id          { get; set; }
        public string  Name        { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int     WeeklyHours { get; set; }
        public bool    IsActive    { get; set; }

        /// <summary>Grados a los que está asociada esta asignatura.</summary>
        public List<SubjectGradeDto> Grades { get; set; } = new();
    }
}
