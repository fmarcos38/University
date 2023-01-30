using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Models
{
    public class Student: BaseEntity
    {
        [Required, StringLength(50)]
        public string FirsName { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public DateTime Dob { get; set; }

        //relacion con cursos
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
