using UniversityApiBackend.Models;

namespace UniversityApiBackend.Services
{
    public interface ICategoriesService
    {
        IEnumerable<Category> GetCourseByCategory(string name);
    }
}
