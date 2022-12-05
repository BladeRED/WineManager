using System.ComponentModel.DataAnnotations;

namespace WineManager.DTO
{
    public class UserDto
    {
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
    }
}
