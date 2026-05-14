namespace LuminiSchool.Domain.Model.AcademicPeriod.DTOs
{
    public class UpdateAcademicPeriodDto
    {
        public int AcademicYear { get; set; }
        public int PeriodNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}
