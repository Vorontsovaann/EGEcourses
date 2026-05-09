using PZ3.Models;

namespace PZ3.Services
{
    public interface IStudentService
    {
        IEnumerable<Student> FormatStudents(IEnumerable<Student> students);
        string GetAppInfo();
    }
}