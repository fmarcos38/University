using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Models
{
    public class Category: BaseEntity
    {
        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;

        //establesco relacion con curso
        //una categoría puede tener varios cursos
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
