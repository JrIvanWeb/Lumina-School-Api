namespace LuminiSchool.Domain.Model.Grade.DTOs
{
    public class CreateGradeDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int? Order { get; set; }
    }
}
