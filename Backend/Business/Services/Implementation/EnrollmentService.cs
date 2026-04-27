using AutoMapper;
using LuminiSchool.Business.Exceptions;
using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Domain.Entities.Enrollment;
using LuminiSchool.Domain.Entities.Guardian;
using LuminiSchool.Domain.Entities.Parent;
using LuminiSchool.Domain.Entities.Student;
using LuminiSchool.Domain.Model.Enrollment.DTOs;
using LuminiSchool.Domain.Model.Parent.DTOs;
using LuminiSchool.Infrastructure.Repositories.Contract;

namespace LuminiSchool.Business.Services.Implementation
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _enrollRepo;
        private readonly IStudentRepository    _studentRepo;
        private readonly IGuardianRepository   _guardianRepo;
        private readonly IParentRepository     _parentRepo;
        private readonly IGradeRepository      _gradeRepo;
        private readonly IMapper               _mapper;

        public EnrollmentService(
            IEnrollmentRepository enrollRepo,
            IStudentRepository    studentRepo,
            IGuardianRepository   guardianRepo,
            IParentRepository     parentRepo,
            IGradeRepository      gradeRepo,
            IMapper               mapper)
        {
            _enrollRepo   = enrollRepo;
            _studentRepo  = studentRepo;
            _guardianRepo = guardianRepo;
            _parentRepo   = parentRepo;
            _gradeRepo    = gradeRepo;
            _mapper       = mapper;
        }

        public async Task<IEnumerable<EnrollmentDto>> GetAllAsync() =>
            _mapper.Map<IEnumerable<EnrollmentDto>>(await _enrollRepo.GetAllAsync());

        public async Task<IEnumerable<EnrollmentDto>> GetByStudentAsync(Guid studentId) =>
            _mapper.Map<IEnumerable<EnrollmentDto>>(await _enrollRepo.GetByStudentAsync(studentId));

        public async Task<EnrollmentDto> CreateAsync(CreateEnrollmentDto dto)
        {
            var entity = _mapper.Map<EnrollmentEntity>(dto);
            entity.Id             = Guid.NewGuid();
            entity.EnrollmentDate = DateTime.UtcNow;
            return _mapper.Map<EnrollmentDto>(await _enrollRepo.AddAsync(entity));
        }

        /// <summary>
        /// Procesa la ficha completa de matrícula:
        /// 1. Crea o reutiliza el estudiante
        /// 2. Registra padre y/o madre si se proporcionan
        /// 3. Crea el acudiente (reutilizando datos del padre/madre si aplica)
        /// 4. Genera la matrícula
        /// </summary>
        public async Task<FichaMatriculaResponseDto> CreateFichaAsync(CreateFichaMatriculaDto dto)
        {
            // ── 1. Validar grado ─────────────────────────────────────────────
            var grade = await _gradeRepo.GetByIdAsync(dto.GradeId)
                        ?? throw new NotFoundException($"Grado {dto.GradeId} no encontrado.");

            // ── 2. Crear o reutilizar estudiante ─────────────────────────────
            var student = await _studentRepo.GetByDocumentAsync(dto.Student.DocumentNumber);
            if (student == null)
            {
                student = new StudentEntity
                {
                    Id                 = Guid.NewGuid(),
                    DocumentType       = dto.Student.DocumentType,
                    DocumentNumber     = dto.Student.DocumentNumber,
                    FirstName          = dto.Student.FirstName,
                    LastName           = dto.Student.LastName,
                    Gender             = dto.Student.Gender,
                    BirthDate          = dto.Student.BirthDate,
                    BirthPlace         = dto.Student.BirthPlace,
                    Address            = dto.Student.Address,
                    City               = dto.Student.City,
                    Phone              = dto.Student.Phone,
                    Email              = dto.Student.Email,
                    Eps                = dto.Student.Eps,
                    BloodType          = dto.Student.BloodType,
                    PreviousInstitution = dto.Student.PreviousInstitution,
                    CreatedAt          = DateTime.UtcNow
                };
                await _studentRepo.AddAsync(student);
            }

            // Verificar que no tenga matrícula activa en el mismo año
            var activeEnrollment = await _enrollRepo.GetActiveByStudentAsync(student.Id);
            if (activeEnrollment != null && activeEnrollment.AcademicYear == dto.AcademicYear)
                throw new ConflictException($"El estudiante ya tiene una matrícula activa para el año {dto.AcademicYear}.");

            // ── 3. Registrar padre (opcional) ────────────────────────────────
            ParentEntity? father = null;
            if (dto.Father != null)
            {
                father = await _parentRepo.GetByDocumentAsync(dto.Father.DocumentNumber);
                if (father == null)
                {
                    father = MapParent(dto.Father);
                    await _parentRepo.AddAsync(father);
                }
                if (!father.Students.Any(s => s.Id == student.Id))
                    father.Students.Add(student);
            }

            // ── 4. Registrar madre (opcional) ────────────────────────────────
            ParentEntity? mother = null;
            if (dto.Mother != null)
            {
                mother = await _parentRepo.GetByDocumentAsync(dto.Mother.DocumentNumber);
                if (mother == null)
                {
                    mother = MapParent(dto.Mother);
                    await _parentRepo.AddAsync(mother);
                }
                if (!mother.Students.Any(s => s.Id == student.Id))
                    mother.Students.Add(student);
            }

            // ── 5. Crear acudiente ───────────────────────────────────────────
            GuardianEntity guardian;

            if (dto.Guardian.GuardianType == GuardianRelationship.Father)
            {
                // Reutiliza datos del padre — no duplica
                if (father == null)
                    throw new BusinessException("Se indicó que el acudiente es el padre, pero no se proporcionaron datos del padre.");

                guardian = await _guardianRepo.GetByDocumentAsync(father.DocumentNumber)
                           ?? new GuardianEntity
                           {
                               Id             = Guid.NewGuid(),
                               FullName       = father.FullName,
                               DocumentType   = father.DocumentType,
                               DocumentNumber = father.DocumentNumber,
                               Phone          = father.Phone ?? string.Empty,
                               Email          = father.Email,
                               Occupation     = father.Occupation,
                               Relationship   = GuardianRelationship.Father,
                               ParentId       = father.Id,
                               CreatedAt      = DateTime.UtcNow
                           };

                if (guardian.Id == Guid.Empty) guardian.Id = Guid.NewGuid();
                if (!guardian.Students.Any(s => s.Id == student.Id))
                    guardian.Students.Add(student);
                await _guardianRepo.AddOrUpdateAsync(guardian);
            }
            else if (dto.Guardian.GuardianType == GuardianRelationship.Mother)
            {
                // Reutiliza datos de la madre — no duplica
                if (mother == null)
                    throw new BusinessException("Se indicó que el acudiente es la madre, pero no se proporcionaron datos de la madre.");

                guardian = await _guardianRepo.GetByDocumentAsync(mother.DocumentNumber)
                           ?? new GuardianEntity
                           {
                               Id             = Guid.NewGuid(),
                               FullName       = mother.FullName,
                               DocumentType   = mother.DocumentType,
                               DocumentNumber = mother.DocumentNumber,
                               Phone          = mother.Phone ?? string.Empty,
                               Email          = mother.Email,
                               Occupation     = mother.Occupation,
                               Relationship   = GuardianRelationship.Mother,
                               ParentId       = mother.Id,
                               CreatedAt      = DateTime.UtcNow
                           };

                if (guardian.Id == Guid.Empty) guardian.Id = Guid.NewGuid();
                if (!guardian.Students.Any(s => s.Id == student.Id))
                    guardian.Students.Add(student);
                await _guardianRepo.AddOrUpdateAsync(guardian);
            }
            else
            {
                // Otra persona — datos propios obligatorios
                if (string.IsNullOrWhiteSpace(dto.Guardian.FullName)       ||
                    string.IsNullOrWhiteSpace(dto.Guardian.DocumentNumber)  ||
                    string.IsNullOrWhiteSpace(dto.Guardian.Phone))
                    throw new BusinessException("El acudiente (otra persona) requiere: nombre, documento y teléfono.");

                guardian = await _guardianRepo.GetByDocumentAsync(dto.Guardian.DocumentNumber!)
                           ?? new GuardianEntity
                           {
                               Id             = Guid.NewGuid(),
                               FullName       = dto.Guardian.FullName!,
                               DocumentType   = dto.Guardian.DocumentType ?? DocumentType.CC,
                               DocumentNumber = dto.Guardian.DocumentNumber!,
                               Phone          = dto.Guardian.Phone!,
                               Address        = dto.Guardian.Address,
                               Email          = dto.Guardian.Email,
                               Occupation     = dto.Guardian.Occupation,
                               Relationship   = GuardianRelationship.Other,
                               CreatedAt      = DateTime.UtcNow
                           };

                if (!guardian.Students.Any(s => s.Id == student.Id))
                    guardian.Students.Add(student);
                await _guardianRepo.AddOrUpdateAsync(guardian);
            }

            // ── 6. Generar matrícula ─────────────────────────────────────────
            var enrollment = new EnrollmentEntity
            {
                Id             = Guid.NewGuid(),
                StudentId      = student.Id,
                GradeId        = grade.Id,
                GuardianId     = guardian.Id,
                AcademicYear   = dto.AcademicYear,
                Status         = EnrollmentStatus.Active,
                EnrollmentDate = DateTime.UtcNow
            };
            await _enrollRepo.AddAsync(enrollment);

            // ── 7. Respuesta ─────────────────────────────────────────────────
            return new FichaMatriculaResponseDto
            {
                EnrollmentId   = enrollment.Id,
                StudentName    = $"{student.FirstName} {student.LastName}",
                DocumentNumber = student.DocumentNumber,
                GradeName      = grade.Name,
                AcademicYear   = enrollment.AcademicYear,
                GuardianName   = guardian.FullName,
                GuardianPhone  = guardian.Phone,
                Father         = father != null ? _mapper.Map<ParentDto>(father) : null,
                Mother         = mother != null ? _mapper.Map<ParentDto>(mother) : null,
                EnrollmentDate = enrollment.EnrollmentDate
            };
        }

        public async Task WithdrawAsync(Guid id)
        {
            var e = await _enrollRepo.GetByIdAsync(id)
                    ?? throw new NotFoundException($"Matrícula {id} no encontrada.");
            e.Status        = EnrollmentStatus.Withdrawn;
            e.WithdrawalDate = DateTime.UtcNow;
            await _enrollRepo.UpdateAsync(e);
        }

        private static ParentEntity MapParent(CreateParentDto dto) => new()
        {
            Id             = Guid.NewGuid(),
            FullName       = dto.FullName,
            DocumentType   = dto.DocumentType,
            DocumentNumber = dto.DocumentNumber,
            Phone          = dto.Phone,
            Occupation     = dto.Occupation,
            Email          = dto.Email,
            Role           = dto.Role,
            CreatedAt      = DateTime.UtcNow
        };
    }
}
