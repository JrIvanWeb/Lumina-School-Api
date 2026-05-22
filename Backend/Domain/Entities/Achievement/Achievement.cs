using LuminiSchool.Domain.Entities.AcademicPeriod;
using LuminiSchool.Domain.Entities.Grade;
using LuminiSchool.Domain.Entities.Subject;

namespace LuminiSchool.Domain.Entities.Achievement
{
    /// <summary>
    /// Niveles de desempeño según el sistema colombiano MEN (escala 1.0 – 5.0).
    /// Los valores del enum coinciden con las claves usadas en el frontend Angular.
    /// </summary>
    public enum PerformanceLevel
    {
        Superior = 0,   // 4.6 – 5.0
        Alto     = 1,   // 4.0 – 4.5
        Basico   = 2,   // 3.0 – 3.9
        Bajo     = 3    // 1.0 – 2.9
    }

    /// <summary>
    /// Logro académico. Agrupa un enunciado de logro e indicador para una
    /// combinación única de Período · Grado · Asignatura · Nivel de desempeño.
    /// </summary>
    public class AchievementEntity
    {
        public Guid   Id          { get; set; } = Guid.NewGuid();

        // ── Claves foráneas ──────────────────────────────────────────────────
        public Guid   PeriodId    { get; set; }
        public Guid   GradeId     { get; set; }
        public Guid   SubjectId   { get; set; }

        // ── Desempeño ────────────────────────────────────────────────────────
        public PerformanceLevel Performance { get; set; }

        /// <summary>Nota mínima del rango de este desempeño (ej. 3.0).</summary>
        public decimal NoteMin    { get; set; }

        /// <summary>Nota máxima del rango de este desempeño (ej. 3.9).</summary>
        public decimal NoteMax    { get; set; }

        // ── Contenido ────────────────────────────────────────────────────────
        /// <summary>Enunciado del logro (máx. 800 caracteres, redactado en infinitivo).</summary>
        public string  Achievement { get; set; } = string.Empty;

        /// <summary>Indicador de logro (máx. 400 caracteres, opcional).</summary>
        public string? Indicator   { get; set; }

        // ── Auditoría ────────────────────────────────────────────────────────
        public bool     IsActive   { get; set; } = true;
        public DateTime CreatedAt  { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt  { get; set; } = DateTime.UtcNow;

        // ── Navegación ───────────────────────────────────────────────────────
        public AcademicPeriodEntity? Period  { get; set; }
        public GradeEntity?          Grade   { get; set; }
        public SubjectEntity?        Subject { get; set; }
    }
}
