using Microsoft.Build.Framework;

namespace UniversityApiBackend.Models
{
    public class UserLogins
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
