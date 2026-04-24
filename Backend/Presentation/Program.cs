using LuminiSchool.Presentation.IoCContainers;
using LuminiSchool.Presentation.Serilog;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// ── Serilog ───────────────────────────────────────────────────────────────────
builder.ConfigureSerilog();

// ── Services ──────────────────────────────────────────────────────────────────
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddBusiness();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddSwaggerDocs();

builder.Services.AddCors(options =>
{
    options.AddPolicy("LuminiPolicy", policy =>
    {
        policy
            .WithOrigins(
                builder.Configuration
                    .GetSection("AllowedOrigins")
                    .Get<string[]>() ?? new[] { "*" })
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// ── Pipeline ──────────────────────────────────────────────────────────────────
var app = builder.Build();

app.UseMiddleware<LuminiSchool.Presentation.IoCContainers.GlobalExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lumini School API v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseCors("LuminiPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

try
{
    Log.Information("🚀 Iniciando Lumini School API...");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "❌ Error fatal al iniciar la API.");
}
finally
{
    Log.CloseAndFlush();
}
