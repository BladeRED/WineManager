using System.ComponentModel.DataAnnotations;
using WineManager.Entities;

namespace WineManager.DTO
{
    public class UserPostDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }

        public UserPostDto()
        {
        }

        public UserPostDto(User user)
        {
            Name = user.Name;
            Email = user.Email;
            Password = user.Password;
            BirthDate = user.BirthDate;
        }
    }


}
