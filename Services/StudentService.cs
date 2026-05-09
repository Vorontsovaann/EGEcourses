using PZ3.Models;
using Microsoft.Extensions.Configuration;

namespace PZ3.Services
{
    public class StudentService : IStudentService
    {
        private readonly IConfiguration _config;

        public StudentService(IConfiguration config)
        {
            _config = config;
        }

        // сортируем студентов по фамилии
        public IEnumerable<Student> FormatStudents(IEnumerable<Student> students)
        {
            return students
                .Where(s => !string.IsNullOrEmpty(s.LastName))
                .OrderBy(s => s.LastName)
                .ThenBy(s => s.FirstName);
        }

        // получаем информацию о приложении из конфигурации
        public string GetAppInfo()
        {
            var appName = _config["AppName"];
            var version = _config["Version"];
            return $"{appName} v{version}";
        }
    }
}