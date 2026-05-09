using Microsoft.EntityFrameworkCore;
using PZ3.Data;
using PZ3.Services;

var builder = WebApplication.CreateBuilder(args);

// получаем строку подключения из appsettings.json
var connection = builder.Configuration.GetConnectionString("DefaultConnection");

// регистрируем контекст базы данных
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlite(connection));

// регистрируем наш сервис
builder.Services.AddTransient<IStudentService, StudentService>();

var app = builder.Build();

// главная страница
app.MapGet("/", () => "API работает! Доступные эндпоинты: /api/data, /api/courses, /api/config");

// получаем всех студентов
app.MapGet("/api/data", async (ApplicationContext db, IStudentService studentService) =>
{
    var students = await db.Students.ToListAsync();
    var formattedStudents = studentService.FormatStudents(students);

    return Results.Json(new
    {
        appInfo = studentService.GetAppInfo(),
        count = formattedStudents.Count(),
        students = formattedStudents.Select(s => new
        {
            s.StudentId,
            s.FirstName,
            s.LastName,
            s.FullName,
            s.Email,
            s.DateOfBirth
        })
    });
});

// получаем все курсы
app.MapGet("/api/courses", async (ApplicationContext db) =>
{
    var courses = await db.Courses.ToListAsync();
    return Results.Json(new
    {
        totalCourses = courses.Count(),
        courses
    });
});

// получаем конфигурацию
app.MapGet("/api/config", (IConfiguration config) =>
{
    return Results.Json(new
    {
        appName = config["AppName"],
        version = config["Version"],
        maxItems = config["MaxItems"]
    });
});

Console.WriteLine("=== Практическая работа 3 ===");
Console.WriteLine("Приложение запущено на http://localhost:5000");
Console.WriteLine("Доступные endpoint'ы:");
Console.WriteLine("GET  /");
Console.WriteLine("GET  /api/data");
Console.WriteLine("GET  /api/courses");
Console.WriteLine("GET  /api/config");

app.Run();