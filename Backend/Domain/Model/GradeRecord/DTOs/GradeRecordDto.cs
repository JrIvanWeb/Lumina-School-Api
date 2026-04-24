namespace LuminiSchool.Domain.Model.GradeRecord.DTOs
{
    public class GradeRecordDto {
        public Guid Id{get;set;} 
        public Guid StudentId{get;set;} 
        public string StudentName{get;set;}=string.Empty; 
        public Guid SubjectId{get;set;} 
        public string SubjectName{get;set;}=string.Empty; 
        public Guid AcademicPeriodId{get;set;} 
        public decimal Score{get;set;} 
        public decimal? Average{get;set;} 
        public string? Observations{get;set;} }
}
