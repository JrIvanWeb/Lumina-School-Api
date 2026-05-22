using LuminiSchool.Domain.Entities.Achievement;

namespace LuminiSchool.Domain.Model.Achievement.DTOs
{
    public class UpdateAchievementDto
    {
        public Guid             PeriodId    { get; set; }
        public Guid             GradeId     { get; set; }
        public Guid             SubjectId   { get; set; }
        public PerformanceLevel Performance { get; set; }
        public decimal          NoteMin     { get; set; }
        public decimal          NoteMax     { get; set; }
        public string           Achievement { get; set; } = string.Empty;
        public string?          Indicator   { get; set; }
    }
}
