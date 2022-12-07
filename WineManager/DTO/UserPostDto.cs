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

        public static User ConvertUserPostDtoToUser(UserPostDto userPostDto)
        {
            var user = new User
            {
                Name = userPostDto.Name,
                Email = userPostDto.Email,
                Password = userPostDto.Password,
                BirthDate = userPostDto.BirthDate
            };

            return user;
        }
        public static UserDto ConvertUserPostDtoToUserDto(UserPostDto userDto)
        {
            var user = new UserDto
            {
                Name = userDto.Name,
                Email = userDto.Email,
                BirthDate = userDto.BirthDate
            };

            return user;
        }

    }


}
