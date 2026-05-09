using Microsoft.EntityFrameworkCore;
using PZ3.Models;

namespace PZ3.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Course> Courses { get; set; } = null!;
        public DbSet<Enrollment> Enrollments { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            // Автоматическое создание таблиц БД
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // настраиваем связи между таблицами
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId);

            // добавляем тестовые данные для курсов
            modelBuilder.Entity<Course>().HasData(
                new Course
                {
                    CourseId = 1,
                    Title = "Математика ЕГЭ",
                    Subject = "Математика",
                    Price = 5000.00m,
                    StartDate = new DateOnly(2026, 9, 1),
                    TeacherName = "Иванова М.П."
                },
                new Course
                {
                    CourseId = 2,
                    Title = "Английский ",
                    Subject = "Английский",
                    Price = 7500.00m,
                    StartDate = new DateOnly(2026, 9, 15),
                    TeacherName = "Petrov A.S."
                },
                new Course
                {
                    CourseId = 3,
                    Title = "Информатика ЕГЭ",
                    Subject = "Информатика",
                    Price = 6000.00m,
                    StartDate = new DateOnly(2026, 10, 1),
                    TeacherName = "Сидоров К.В."
                }
            );

            // добавляем тестовые данные для студентов
            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    StudentId = 1,
                    FirstName = "Анна",
                    LastName = "Дробжева",
                    Email = "annadr@st.ru",
                    DateOfBirth = new DateOnly(2006, 11, 10)
                },
                new Student
                {
                    StudentId = 2,
                    FirstName = "Виктория",
                    LastName = "Косолапова",
                    Email = "vikakos@st.ru",
                    DateOfBirth = new DateOnly(2006, 5, 5)
                },
                new Student
                {
                    StudentId = 3,
                    FirstName = "Виктория",
                    LastName = "Верховых",
                    Email = "vikaverh@st.ru",
                    DateOfBirth = new DateOnly(2006, 4, 25)
                }
            );
        }
    }
}