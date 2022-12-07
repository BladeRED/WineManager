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
        public List<Bottle>? Bottles { get; set; }
        public List<Drawer>? Drawers { get; set; }
        public List<Cave>? Caves { get; set; }
        public List<BottleDtoLight>? BottlesList { get; set; }
        public List<DrawerDtoLight>? DrawersList { get; set; }
        public List<CaveDtoLight>? CavesList { get; set; }

        public UserDto() { }

        public UserDto(UserPostDto userPostDto)
        {
            Name = userPostDto.Name;
            Email = userPostDto.Email;
            BirthDate = userPostDto.BirthDate;
        }

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

        public UserDto(User user, List<Bottle> bottles)
        {
            Name = user.Name;
            BirthDate = user.BirthDate;
            Email = user.Email;
            if (bottles != null)
            {
                BottlesList = new List<BottleDtoLight>();
                foreach (var item in bottles)
                {
                    BottlesList.Add(new BottleDtoLight(item));
                }
            }
        }

        public UserDto(User user, List<Cave> caves)
        {
            Name = user.Name;
            BirthDate = user.BirthDate;
            Email = user.Email;
            if (caves != null)
            {
                CavesList = new List<CaveDtoLight>();
                foreach (var item in caves)
                {
                    CavesList.Add(new CaveDtoLight(item));
                }
            }
        }

        public UserDto(User user, List<Drawer> drawers)
        {
            Name = user.Name;
            BirthDate = user.BirthDate;
            Email = user.Email;
            if (drawers != null)
            {
                DrawersList = new List<DrawerDtoLight>();
                foreach (var item in drawers)
                {
                    DrawersList.Add(new DrawerDtoLight(item));
                }
            }
        }

    }
}

