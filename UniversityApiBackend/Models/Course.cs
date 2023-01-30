using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Models
{
    //declaro la variable de tipo ENUM
    public enum Level
    {
        Basic,
        Medium,
        Advanced,
        Expert
    }
    public class Course: BaseEntity
    {
        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(280)]
        public string ShortDescription { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public Level Level { get; set; } = Level.Basic;

        //genero relaciones
        //un curso puede estar en varias categorias -> por eso el tipo de variable es una Coleccion
        [Required]
        public ICollection<Category> Categories { get; set; } = new List<Category>();

        [Required]
        public Chapter Chapters { get; set; } = new Chapter();

        //relacion con student
        [Required]
        public ICollection<Student> Levels { get;set; } = new List<Student>();
    }
}
