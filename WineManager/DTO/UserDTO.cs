using System.ComponentModel.DataAnnotations;
using WineManager.Entities;

namespace WineManager.DTO
{
    public class UserDto
    {
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        public List<Bottle>? Bottels { get; set; }
        public List<Drawer>? Drawers { get; set; }
        public List<Cave>? Caves { get; set; }

        public UserDto() { }
        public UserDto(User user)
        {
            Name = user.Name;
            BirthDate = user.BirthDate;
            Email = user.Email;
            Drawers = user.Drawers;
        }
        public UserDto(UserPutDto userPutDto)
        {
            Name = userPutDto.NewName;
            Email = userPutDto.NewEmail;
            BirthDate = (DateTime)userPutDto.NewBirthDate;
        }

    }
}

