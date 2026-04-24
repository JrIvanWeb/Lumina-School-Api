using LuminiSchool.Business.Exceptions;
using LuminiSchool.Business.Services.Contract;
using LuminiSchool.Business.Services.Implementation;
using LuminiSchool.Business.Utils.Profiles;
using LuminiSchool.Domain.Entities.User;
using LuminiSchool.Infrastructure.FileStorage.Contract;
using LuminiSchool.Infrastructure.FileStorage.Implementation;
using LuminiSchool.Infrastructure.Repositories;
using LuminiSchool.Infrastructure.Repositories.Contract;
using LuminiSchool.Infrastructure.Repositories.Implementation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Text;
using System.Text.Json;

namespace LuminiSchool.Presentation.IoCContainers
{
    public static class IoCContainer
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            // ── Database ──────────────────────────────────────────────────────────
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    config.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("LuminiSchool.Infrastructure")));

            // ── Identity ──────────────────────────────────────────────────────────
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequireDigit           = true;
                options.Password.RequiredLength         = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase       = true;
                options.User.RequireUniqueEmail         = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            // ── Repositories ──────────────────────────────────────────────────────
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IStudentRepository,              StudentRepository>();
            services.AddScoped<ITeacherRepository,              TeacherRepository>();
            services.AddScoped<IGuardianRepository,             GuardianRepository>();
            services.AddScoped<ISubjectRepository,              SubjectRepository>();
            services.AddScoped<IGradeRepository,                GradeRepository>();
            services.AddScoped<IAcademicPeriodRepository,       AcademicPeriodRepository>();
            services.AddScoped<IEnrollmentRepository,           EnrollmentRepository>();
            services.AddScoped<IClassPlannerRepository,         ClassPlannerRepository>();
            services.AddScoped<IActivityRepository,             ActivityRepository>();
            services.AddScoped<IAttendanceRepository,           AttendanceRepository>();
            services.AddScoped<IGradeRecordRepository,          GradeRecordRepository>();
            services.AddScoped<IBulletinRepository,             BulletinRepository>();
            services.AddScoped<IScheduleRepository,             ScheduleRepository>();
            services.AddScoped<IObserverRepository,             ObserverRepository>();
            services.AddScoped<IAchievementRepository,          AchievementRepository>();
            services.AddScoped<ICertificateRepository,          CertificateRepository>();
            services.AddScoped<ISchoolRepresentativeRepository, SchoolRepresentativeRepository>();
            services.AddScoped<INotificationRepository,         NotificationRepository>();
            services.AddScoped<IMessageRepository,              MessageRepository>();
            services.AddScoped<IReportRepository,               ReportRepository>();
            services.AddScoped<IDiagnosticTestRepository,       DiagnosticTestRepository>();
            services.AddScoped<IIcfesSimulatorRepository,       IcfesSimulatorRepository>();

            // ── File Storage ──────────────────────────────────────────────────────
            services.AddScoped<IFileStorageService, LocalFileStorageService>();

            return services;
        }

        public static IServiceCollection AddBusiness(this IServiceCollection services)
        {
            // ── AutoMapper ────────────────────────────────────────────────────────
            services.AddAutoMapper(typeof(IcfesSimulatorProfile));

            // ── Services ──────────────────────────────────────────────────────────
            services.AddScoped<IAuthService,                AuthService>();
            services.AddScoped<IStudentService,             StudentService>();
            services.AddScoped<ITeacherService,             TeacherService>();
            services.AddScoped<IGuardianService,            GuardianService>();
            services.AddScoped<ISubjectService,             SubjectService>();
            services.AddScoped<IGradeService,               GradeService>();
            services.AddScoped<IAcademicPeriodService,      AcademicPeriodService>();
            services.AddScoped<IEnrollmentService,          EnrollmentService>();
            services.AddScoped<IClassPlannerService,        ClassPlannerService>();
            services.AddScoped<IActivityService,            ActivityService>();
            services.AddScoped<IAttendanceService,          AttendanceService>();
            services.AddScoped<IGradeRecordService,         GradeRecordService>();
            services.AddScoped<IBulletinService,            BulletinService>();
            services.AddScoped<IScheduleService,            ScheduleService>();
            services.AddScoped<IObserverService,            ObserverService>();
            services.AddScoped<IAchievementService,         AchievementService>();
            services.AddScoped<ICertificateService,         CertificateService>();
            services.AddScoped<ISchoolRepresentativeService,SchoolRepresentativeService>();
            services.AddScoped<INotificationService,        NotificationService>();
            services.AddScoped<IMessageService,             MessageService>();
            services.AddScoped<IReportService,              ReportService>();
            services.AddScoped<IDiagnosticTestService,      DiagnosticTestService>();
            services.AddScoped<IIcfesSimulatorService,      IcfesSimulatorService>();

            return services;
        }

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme    = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer           = true,
                    ValidateAudience         = true,
                    ValidateLifetime         = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer              = config["Jwt:Issuer"],
                    ValidAudience            = config["Jwt:Audience"],
                    IssuerSigningKey         = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(config["Jwt:Key"]!))
                };
            });

            services.AddAuthorization();
            return services;
        }

        public static IServiceCollection AddSwaggerDocs(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title       = "Lumini School API",
                    Version     = "v1",
                    Description = "Plataforma Educativa Integral — Backend API"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name         = "Authorization",
                    Type         = SecuritySchemeType.ApiKey,
                    Scheme       = "Bearer",
                    BearerFormat = "JWT",
                    In           = ParameterLocation.Header,
                    Description  = "Ingrese: Bearer {token}"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id   = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            return services;
        }
    }

    // ── Global Exception Middleware ────────────────────────────────────────────────
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate                    _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next   = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error no controlado: {Message}", ex.Message);
                await HandleAsync(context, ex);
            }
        }

        private static Task HandleAsync(HttpContext ctx, Exception ex)
        {
            var (code, msg) = ex switch
            {
                NotFoundException    e => (HttpStatusCode.NotFound,            e.Message),
                UnauthorizedException e => (HttpStatusCode.Unauthorized,        e.Message),
                ForbiddenException   e => (HttpStatusCode.Forbidden,           e.Message),
                ConflictException    e => (HttpStatusCode.Conflict,            e.Message),
                ValidationException  e => (HttpStatusCode.BadRequest,          string.Join("; ", e.Errors)),
                BusinessException    e => (HttpStatusCode.BadRequest,          e.Message),
                _                     => (HttpStatusCode.InternalServerError,  "Error inesperado en el servidor.")
            };

            ctx.Response.ContentType = "application/json";
            ctx.Response.StatusCode  = (int)code;

            return ctx.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                status    = (int)code,
                error     = code.ToString(),
                message   = msg,
                timestamp = DateTime.UtcNow
            }));
        }
    }
}
