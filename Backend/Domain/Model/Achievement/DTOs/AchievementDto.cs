using LuminiSchool.Domain.Entities.Achievement;

namespace LuminiSchool.Domain.Model.Achievement.DTOs
{
    public class AchievementDto
    {
        public Guid             Id               { get; set; }
        public Guid             PeriodId         { get; set; }
        public Guid             GradeId          { get; set; }
        public Guid             SubjectId        { get; set; }
        public string           PeriodName       { get; set; } = string.Empty;
        public string           GradeName        { get; set; } = string.Empty;
        public string           SubjectName      { get; set; } = string.Empty;
        public PerformanceLevel Performance      { get; set; }
        public string           PerformanceLabel => Performance.ToString();
        public decimal          NoteMin          { get; set; }
        public decimal          NoteMax          { get; set; }
        public string           Achievement      { get; set; } = string.Empty;
        public string?          Indicator        { get; set; }
        public bool             IsActive         { get; set; }
        public DateTime         CreatedAt        { get; set; }
        public DateTime         UpdatedAt        { get; set; }
    }
}
