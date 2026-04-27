using LuminiSchool.Domain.Entities.Parent;
using LuminiSchool.Domain.Entities.Student;

namespace LuminiSchool.Domain.Model.Parent.DTOs
{
    /// <summary>Datos de un padre o madre (opcional en la ficha).</summary>
    public class CreateParentDto
    {
        public string       FullName       { get; set; } = string.Empty;
        public DocumentType DocumentType   { get; set; } = DocumentType.CC;
        public string       DocumentNumber { get; set; } = string.Empty;
        public string?      Phone          { get; set; }
        public string?      Occupation     { get; set; }
        public string?      Email          { get; set; }
        public ParentRole   Role           { get; set; }
    }

    public class ParentDto
    {
        public Guid         Id             { get; set; }
        public string       FullName       { get; set; } = string.Empty;
        public DocumentType DocumentType   { get; set; }
        public string       DocumentNumber { get; set; } = string.Empty;
        public string?      Phone          { get; set; }
        public string?      Occupation     { get; set; }
        public string?      Email          { get; set; }
        public ParentRole   Role           { get; set; }
    }
}
