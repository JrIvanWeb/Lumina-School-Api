# 🎓 Lumini School — Backend API

Plataforma educativa integral desarrollada en **.NET 8** con arquitectura por capas.

---

## 🚀 Cómo abrir en Visual Studio Community

1. Descomprime el ZIP en tu carpeta de proyectos (ej: `C:\Proyectos\`)
2. Abre **Visual Studio Community 2022**
3. Clic en **"Abrir un proyecto o solución"**
4. Selecciona el archivo **`LuminiSchool.sln`** en la raíz
5. Visual Studio carga los 4 proyectos automáticamente ✅

---

## ⚙️ Primeros pasos

### 1. Configurar la cadena de conexión
Edita `Backend/Presentation/appsettings.Development.json`:
```json
"DefaultConnection": "Server=TU_SERVIDOR;Database=LuminiSchoolDb;Trusted_Connection=True;TrustServerCertificate=True;"
```

### 2. Crear la base de datos
Abre la **Consola del Administrador de paquetes** (Herramientas → NuGet → Consola):
```powershell
Add-Migration InitialCreate `
  -Project LuminiSchool.Infrastructure `
  -StartupProject LuminiSchool.Presentation

Update-Database `
  -Project LuminiSchool.Infrastructure `
  -StartupProject LuminiSchool.Presentation
```

### 3. Ejecutar
- Clic derecho en `LuminiSchool.Presentation` → **Establecer como proyecto de inicio**
- Presiona **F5**
- Swagger UI: `https://localhost:{puerto}/swagger`

---

## 🏗️ Estructura

```
LuminiSchool.sln
└── Backend/
    ├── Domain/              → Entidades + DTOs por módulo
    ├── Business/            → Servicios, interfaces, AutoMapper, excepciones
    ├── Infrastructure/      → Repositorios, DbContext, FileStorage, Migrations
    └── Presentation/        → Controllers, IoC, Serilog, Program.cs
```

---

## 📋 Módulos implementados (23)

| Módulo                  | Ruta API                        |
|-------------------------|---------------------------------|
| Autenticación           | `POST /api/auth/login`          |
| Estudiantes             | `/api/students`                 |
| Docentes                | `/api/teachers`                 |
| Acudientes              | `/api/guardians`                |
| Asignaturas             | `/api/subjects`                 |
| Grados                  | `/api/grades`                   |
| Períodos Académicos     | `/api/academic-periods`         |
| Matrículas              | `/api/enrollments`              |
| Planeador de Clase      | `/api/class-planners`           |
| Actividades             | `/api/activities`               |
| Asistencia              | `/api/attendance`               |
| Calificaciones          | `/api/grade-records`            |
| Boletín                 | `/api/bulletins`                |
| Horarios                | `/api/schedules`                |
| Observador              | `/api/observers`                |
| Banco de Logros         | `/api/achievements`             |
| Certificados            | `/api/certificates`             |
| Gobierno Escolar        | `/api/school-representatives`   |
| Notificaciones          | `/api/notifications`            |
| Mensajería              | `/api/messages`                 |
| Reportes                | `/api/reports`                  |
| Pruebas Diagnósticas    | `/api/diagnostic-tests`         |
| Simulador ICFES         | `/api/icfes-simulators`         |

---

## 🔐 Roles

| Rol        | Descripción                        |
|------------|------------------------------------|
| SuperAdmin | Acceso total                       |
| Admin      | Administración general             |
| Rector     | Gestión académica institucional    |
| Teacher    | Clases, notas y asistencia         |
| Student    | Su información académica           |
| Guardian   | Información de su acudido          |

---

## 📦 Paquetes NuGet incluidos en los `.csproj`

- `Microsoft.EntityFrameworkCore.SqlServer` 8.0.0
- `Microsoft.AspNetCore.Authentication.JwtBearer` 8.0.0
- `Microsoft.AspNetCore.Identity.EntityFrameworkCore` 8.0.0
- `AutoMapper` 13.0.1
- `Serilog.AspNetCore` 8.0.2
- `Swashbuckle.AspNetCore` 6.5.0
- `System.IdentityModel.Tokens.Jwt` 7.5.1
