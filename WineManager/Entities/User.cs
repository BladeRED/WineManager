using System.ComponentModel.DataAnnotations;
using WineManager.DTO;

namespace WineManager.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public string Password { get; set; }

        // Navigation properties //

        public List<Bottle>? Bottles { get; set; }
        public List<Cave>? Caves { get; set; }
        public List<Drawer>? Drawers { get; set; }

        public User()
        {
        }

        public User(UserPutDto userPutDto)
        {
            Name = userPutDto.NewName;
            Email = userPutDto.NewEmail;
            BirthDate = (DateTime)userPutDto.NewBirthDate;
            Password = userPutDto.NewPassword;
        }
        public User(UserPostDto userPostDto)
        {
            Name = userPostDto.Name;
            Email = userPostDto.Email;
            BirthDate = (DateTime)userPostDto.BirthDate;
            Password = userPostDto.Password;
        }

    }
}

