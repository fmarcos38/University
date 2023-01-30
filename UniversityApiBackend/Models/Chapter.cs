using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Models
{
    public class Chapter: BaseEntity
    {
        [Required]
        public string Chapters = string.Empty;

        //creo col para ID de curso
        public int CourseId { get; set; }
        //relacion con un curso
        public virtual Course Course { get; set; } = new Course();
    }
}
