using UniversityApiBackend.Models;

namespace UniversityApiBackend.Services
{
    public interface IStudentsService
    {
        IEnumerable<Student> GetStudentsWhithCourses();
        IEnumerable<Student> GetStudentsWhithNoCourses();
    }
}
