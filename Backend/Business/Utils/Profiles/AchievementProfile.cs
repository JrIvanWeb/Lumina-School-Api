using AutoMapper;
using LuminiSchool.Domain.Entities.Achievement;
using LuminiSchool.Domain.Model.Achievement.DTOs;

namespace LuminiSchool.Business.Utils.Profiles
{
    public class AchievementProfile : Profile
    {
        public AchievementProfile()
        {
            // ── Entity → DTO de respuesta ────────────────────────────────────
            CreateMap<AchievementEntity, AchievementDto>()
                .ForMember(d => d.PeriodName,  o => o.MapFrom(s =>
                    s.Period  != null
                        ? (s.Period.Name ?? $"Periodo {s.Period.PeriodNumber} - {s.Period.AcademicYear}")
                        : string.Empty))
                .ForMember(d => d.GradeName,   o => o.MapFrom(s =>
                    s.Grade   != null ? s.Grade.Name   : string.Empty))
                .ForMember(d => d.SubjectName, o => o.MapFrom(s =>
                    s.Subject != null ? s.Subject.Name : string.Empty));

            // ── CreateDto → Entity ───────────────────────────────────────────
            CreateMap<CreateAchievementDto, AchievementEntity>()
                .ForMember(d => d.Id,        o => o.Ignore())
                .ForMember(d => d.IsActive,  o => o.Ignore())
                .ForMember(d => d.CreatedAt, o => o.Ignore())
                .ForMember(d => d.UpdatedAt, o => o.Ignore())
                .ForMember(d => d.Period,    o => o.Ignore())
                .ForMember(d => d.Grade,     o => o.Ignore())
                .ForMember(d => d.Subject,   o => o.Ignore());

            // ── UpdateDto → Entity  (sólo para referencias futuras) ──────────
            CreateMap<UpdateAchievementDto, AchievementEntity>()
                .ForMember(d => d.Id,        o => o.Ignore())
                .ForMember(d => d.IsActive,  o => o.Ignore())
                .ForMember(d => d.CreatedAt, o => o.Ignore())
                .ForMember(d => d.UpdatedAt, o => o.Ignore())
                .ForMember(d => d.Period,    o => o.Ignore())
                .ForMember(d => d.Grade,     o => o.Ignore())
                .ForMember(d => d.Subject,   o => o.Ignore());
        }
    }
}
