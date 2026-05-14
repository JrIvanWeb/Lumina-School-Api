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
            _mapper.Map<IEnumerable<EnrollmentDto>>(await _enrollRepo.GetAllWithDetailsAsync());

        public async Task<EnrollmentDto> GetByIdAsync(Guid enrollmentId)
        {
            var entity = await _enrollRepo.GetWithDetailsAsync(enrollmentId)
                         ?? throw new NotFoundException($"Matrícula {enrollmentId} no encontrada.");
            return MapEnrollmentDetail(entity);
        }

        public async Task<CreateFichaMatriculaDto> GetFichaAsync(Guid enrollmentId)
        {
            var entity = await _enrollRepo.GetWithDetailsAsync(enrollmentId)
                         ?? throw new NotFoundException($"Matricula {enrollmentId} no encontrada.");
            return MapFichaDetail(entity);
        }

        public async Task<IEnumerable<EnrollmentDto>> GetByStudentAsync(Guid studentId) =>
            _mapper.Map<IEnumerable<EnrollmentDto>>(await _enrollRepo.GetByStudentAsync(studentId));

        public async Task<EnrollmentDto> CreateAsync(CreateEnrollmentDto dto)
        {
            var entity = _mapper.Map<EnrollmentEntity>(dto);
            entity.Id             = Guid.NewGuid();
            entity.EnrollmentDate = DateTime.UtcNow;
            return _mapper.Map<EnrollmentDto>(await _enrollRepo.AddAsync(entity));
        }
        public async Task ActivateAsync(Guid id)
        {
            var e = await _enrollRepo.GetByIdAsync(id)
                    ?? throw new NotFoundException($"Matrícula {id} no encontrada.");
            e.Status = EnrollmentStatus.Active;
            e.WithdrawalDate = null;
            await _enrollRepo.UpdateAsync(e);
        }

        public async Task<EnrollmentDto> UpdateAsync(Guid enrollmentId, UpdateEnrollmentDto dto)
        {
            var enrollment = await _enrollRepo.GetWithDetailsAsync(enrollmentId)
                             ?? throw new NotFoundException($"Matricula {enrollmentId} no encontrada.");

            var grade = await _gradeRepo.GetByIdAsync(dto.GradeId)
                        ?? throw new NotFoundException($"Grado {dto.GradeId} no encontrado.");

            var student = enrollment.Student
                          ?? throw new NotFoundException("Estudiante de la matricula no encontrado.");

            UpdateStudent(student, dto.Student);
            await _studentRepo.UpdateAsync(student);

            var father = await UpsertParentAsync(student, dto.Father, ParentRole.Father);
            var mother = await UpsertParentAsync(student, dto.Mother, ParentRole.Mother);
            var guardian = await ResolveGuardianAsync(student, dto.Guardian, father, mother, enrollment.Guardian);

            enrollment.GradeId = grade.Id;
            enrollment.GuardianId = guardian.Id;
            enrollment.AcademicYear = dto.AcademicYear;

            await _enrollRepo.UpdateAsync(enrollment);

            var updated = await _enrollRepo.GetWithDetailsAsync(enrollmentId)
                          ?? throw new NotFoundException($"Matricula {enrollmentId} no encontrada.");

            return MapEnrollmentDetail(updated);
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
                {
                    father.Students.Add(student);
                    await _parentRepo.UpdateAsync(father);
                }
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
                {
                    mother.Students.Add(student);
                    await _parentRepo.UpdateAsync(mother);
                }
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

        public async Task DeleteAsync(Guid id)
        {
            _ = await _enrollRepo.GetByIdAsync(id)
                ?? throw new NotFoundException($"Matrícula {id} no encontrada.");
            await _enrollRepo.DeleteAsync(id);
        }

        private EnrollmentDto MapEnrollmentDetail(EnrollmentEntity entity)
        {
            var father = entity.Student?.Parents.FirstOrDefault(p => p.Role == ParentRole.Father);
            var mother = entity.Student?.Parents.FirstOrDefault(p => p.Role == ParentRole.Mother);
            if (father == null && entity.Guardian?.Relationship == GuardianRelationship.Father && entity.Guardian.Parent != null)
                father = entity.Guardian.Parent;
            if (mother == null && entity.Guardian?.Relationship == GuardianRelationship.Mother && entity.Guardian.Parent != null)
                mother = entity.Guardian.Parent;

            return new EnrollmentDto
            {
                Id = entity.Id,
                StudentId = entity.StudentId,
                StudentName = entity.Student != null ? $"{entity.Student.FirstName} {entity.Student.LastName}" : string.Empty,
                GradeId = entity.GradeId,
                GradeName = entity.Grade?.Name ?? string.Empty,
                GuardianName = entity.Guardian?.FullName ?? string.Empty,
                AcademicYear = entity.AcademicYear,
                Status = entity.Status,
                EnrollmentDate = entity.EnrollmentDate,
                Student = entity.Student == null ? null : new StudentFichaDto
                {
                    DocumentType = entity.Student.DocumentType,
                    DocumentNumber = entity.Student.DocumentNumber,
                    FirstName = entity.Student.FirstName,
                    LastName = entity.Student.LastName,
                    Gender = entity.Student.Gender,
                    BirthDate = entity.Student.BirthDate,
                    BirthPlace = entity.Student.BirthPlace,
                    Address = entity.Student.Address,
                    City = entity.Student.City,
                    Phone = entity.Student.Phone,
                    Email = entity.Student.Email,
                    Eps = entity.Student.Eps,
                    BloodType = entity.Student.BloodType,
                    PreviousInstitution = entity.Student.PreviousInstitution
                },
                Father = father != null ? _mapper.Map<ParentDto>(father) : null,
                Mother = mother != null ? _mapper.Map<ParentDto>(mother) : null,
                Guardian = entity.Guardian == null ? null : new CreateAcudienteDto
                {
                    GuardianType = entity.Guardian.Relationship,
                    FullName = entity.Guardian.FullName,
                    DocumentType = entity.Guardian.DocumentType,
                    DocumentNumber = entity.Guardian.DocumentNumber,
                    Phone = entity.Guardian.Phone,
                    Address = entity.Guardian.Address,
                    Email = entity.Guardian.Email,
                    Occupation = entity.Guardian.Occupation
                }
            };
        }

        private CreateFichaMatriculaDto MapFichaDetail(EnrollmentEntity entity)
        {
            var father = entity.Student?.Parents.FirstOrDefault(p => p.Role == ParentRole.Father);
            var mother = entity.Student?.Parents.FirstOrDefault(p => p.Role == ParentRole.Mother);

            if (father == null && entity.Guardian?.Relationship == GuardianRelationship.Father && entity.Guardian.Parent != null)
                father = entity.Guardian.Parent;
            if (mother == null && entity.Guardian?.Relationship == GuardianRelationship.Mother && entity.Guardian.Parent != null)
                mother = entity.Guardian.Parent;

            return new CreateFichaMatriculaDto
            {
                Student = entity.Student == null ? new StudentFichaDto() : new StudentFichaDto
                {
                    DocumentType = entity.Student.DocumentType,
                    DocumentNumber = entity.Student.DocumentNumber,
                    FirstName = entity.Student.FirstName,
                    LastName = entity.Student.LastName,
                    Gender = entity.Student.Gender,
                    BirthDate = entity.Student.BirthDate,
                    BirthPlace = entity.Student.BirthPlace,
                    Address = entity.Student.Address,
                    City = entity.Student.City,
                    Phone = entity.Student.Phone,
                    Email = entity.Student.Email,
                    Eps = entity.Student.Eps,
                    BloodType = entity.Student.BloodType,
                    PreviousInstitution = entity.Student.PreviousInstitution
                },
                Father = father == null ? null : new CreateParentDto
                {
                    FullName = father.FullName,
                    DocumentType = father.DocumentType,
                    DocumentNumber = father.DocumentNumber,
                    Phone = father.Phone,
                    Occupation = father.Occupation,
                    Email = father.Email,
                    Role = ParentRole.Father
                },
                Mother = mother == null ? null : new CreateParentDto
                {
                    FullName = mother.FullName,
                    DocumentType = mother.DocumentType,
                    DocumentNumber = mother.DocumentNumber,
                    Phone = mother.Phone,
                    Occupation = mother.Occupation,
                    Email = mother.Email,
                    Role = ParentRole.Mother
                },
                Guardian = entity.Guardian == null ? new CreateAcudienteDto() : new CreateAcudienteDto
                {
                    GuardianType = entity.Guardian.Relationship,
                    FullName = entity.Guardian.FullName,
                    DocumentType = entity.Guardian.DocumentType,
                    DocumentNumber = entity.Guardian.DocumentNumber,
                    Phone = entity.Guardian.Phone,
                    Address = entity.Guardian.Address,
                    Email = entity.Guardian.Email,
                    Occupation = entity.Guardian.Occupation
                },
                GradeId = entity.GradeId,
                AcademicYear = entity.AcademicYear
            };
        }

        private static void UpdateStudent(StudentEntity student, StudentFichaDto dto)
        {
            student.DocumentType = dto.DocumentType;
            student.DocumentNumber = dto.DocumentNumber;
            student.FirstName = dto.FirstName;
            student.LastName = dto.LastName;
            student.Gender = dto.Gender;
            student.BirthDate = dto.BirthDate;
            student.BirthPlace = dto.BirthPlace;
            student.Address = dto.Address;
            student.City = dto.City;
            student.Phone = dto.Phone;
            student.Email = dto.Email;
            student.Eps = dto.Eps;
            student.BloodType = dto.BloodType;
            student.PreviousInstitution = dto.PreviousInstitution;
            student.UpdatedAt = DateTime.UtcNow;
        }

        private async Task<ParentEntity?> UpsertParentAsync(StudentEntity student, CreateParentDto? dto, ParentRole role)
        {
            if (dto == null) return student.Parents.FirstOrDefault(p => p.Role == role);

            var parent = student.Parents.FirstOrDefault(p => p.Role == role)
                         ?? await _parentRepo.GetByDocumentAsync(dto.DocumentNumber);

            if (parent == null)
            {
                parent = MapParent(dto);
                parent.Role = role;
                parent.Students.Add(student);
                await _parentRepo.AddAsync(parent);
                return parent;
            }

            parent.FullName = dto.FullName;
            parent.DocumentType = dto.DocumentType;
            parent.DocumentNumber = dto.DocumentNumber;
            parent.Phone = dto.Phone;
            parent.Occupation = dto.Occupation;
            parent.Email = dto.Email;
            parent.Role = role;

            // No agregar si ya existe en la colección cargada (evita duplicate key en ParentStudents)
            if (!parent.Students.Any(s => s.Id == student.Id))
                parent.Students.Add(student);

            // UpdateWithoutSave evita SaveChanges intermedio que puede romper el tracking
            _parentRepo.UpdateWithoutSave(parent);
            return parent;
        }

        private async Task<GuardianEntity> ResolveGuardianAsync(
            StudentEntity student,
            CreateAcudienteDto dto,
            ParentEntity? father,
            ParentEntity? mother,
            GuardianEntity? currentGuardian)
        {
            if (dto.GuardianType == GuardianRelationship.Father)
            {
                if (father == null)
                    throw new BusinessException("El acudiente es el padre, pero no se proporcionaron datos del padre.");

                return await UpsertGuardianFromParentAsync(student, father, GuardianRelationship.Father, currentGuardian);
            }

            if (dto.GuardianType == GuardianRelationship.Mother)
            {
                if (mother == null)
                    throw new BusinessException("El acudiente es la madre, pero no se proporcionaron datos de la madre.");

                return await UpsertGuardianFromParentAsync(student, mother, GuardianRelationship.Mother, currentGuardian);
            }

            if (string.IsNullOrWhiteSpace(dto.FullName) ||
                string.IsNullOrWhiteSpace(dto.DocumentNumber) ||
                string.IsNullOrWhiteSpace(dto.Phone))
                throw new BusinessException("El acudiente requiere: nombre, documento y telefono.");

            var guardian = currentGuardian?.Relationship == GuardianRelationship.Other
                ? currentGuardian
                : await _guardianRepo.GetByDocumentAsync(dto.DocumentNumber!);

            guardian ??= new GuardianEntity { Id = Guid.NewGuid(), CreatedAt = DateTime.UtcNow };
            guardian.FullName = dto.FullName!;
            guardian.DocumentType = dto.DocumentType ?? DocumentType.CC;
            guardian.DocumentNumber = dto.DocumentNumber!;
            guardian.Phone = dto.Phone!;
            guardian.Address = dto.Address;
            guardian.Email = dto.Email;
            guardian.Occupation = dto.Occupation;
            guardian.Relationship = GuardianRelationship.Other;
            guardian.ParentId = null;

            if (!guardian.Students.Any(s => s.Id == student.Id))
                guardian.Students.Add(student);

            await _guardianRepo.AddOrUpdateAsync(guardian);
            return guardian;
        }

        private async Task<GuardianEntity> UpsertGuardianFromParentAsync(
            StudentEntity student,
            ParentEntity parent,
            GuardianRelationship relationship,
            GuardianEntity? currentGuardian)
        {
            var guardian = currentGuardian?.ParentId == parent.Id
                ? currentGuardian
                : await _guardianRepo.GetByDocumentAsync(parent.DocumentNumber);

            guardian ??= new GuardianEntity { Id = Guid.NewGuid(), CreatedAt = DateTime.UtcNow };
            guardian.FullName = parent.FullName;
            guardian.DocumentType = parent.DocumentType;
            guardian.DocumentNumber = parent.DocumentNumber;
            guardian.Phone = parent.Phone ?? string.Empty;
            guardian.Email = parent.Email;
            guardian.Occupation = parent.Occupation;
            guardian.Relationship = relationship;
            guardian.ParentId = parent.Id;

            if (!guardian.Students.Any(s => s.Id == student.Id))
                guardian.Students.Add(student);

            await _guardianRepo.AddOrUpdateAsync(guardian);
            return guardian;
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
