namespace LuminiSchool.Domain.Model.Schedule.DTOs
{
    public class ScheduleDto { 
        public Guid Id{get;set;} 
        public Guid GradeId{get;set;} 
        public string GradeName{get;set;}=string.Empty; 
        public Guid SubjectId{get;set;} 
        public string SubjectName{get;set;}=string.Empty; 
        public Guid TeacherId{get;set;} 
        public string TeacherName{get;set;}=string.Empty; 
        public DayOfWeek DayOfWeek{get;set;} public TimeSpan StartTime{get;set;} 
        public TimeSpan EndTime{get;set;} public string? Classroom{get;set;} 
    }
}
