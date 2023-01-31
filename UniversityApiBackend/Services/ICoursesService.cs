using UniversityApiBackend.Models;

namespace UniversityApiBackend.Services
{
    public interface ICoursesService
    {
        IEnumerable<Course> GetCourseByCategoty(string categotyId);
        IEnumerable<Course> GetCourseNoChapter();
    }
}
