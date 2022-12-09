using System.ComponentModel.DataAnnotations;
using WineManager.Entities;

namespace WineManager.DTO
{
    public class UserDtoGet
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public List<BottleDtoLight>? Bottles { get; set; }
        public List<DrawerDtoLight>? Drawers { get; set; }
        public List<CaveDtoLight>? Caves { get; set; }
        public UserDtoGet(User user)
        {
            Name = user.Name;
            Email = user.Email;
            if (user.Caves != null)
            {
                Caves = new List<CaveDtoLight>();
                foreach (var item in user.Caves)
                {
                    Caves.Add(new CaveDtoLight(item));
                }
            }
            if (user.Bottles != null)
            {
                Bottles = new List<BottleDtoLight>();
                foreach (var item in user.Bottles)
                {
                    Bottles.Add(new BottleDtoLight(item));
                }
            }
            if (user.Drawers != null)
            {
                Drawers = new List<DrawerDtoLight>();
                foreach (var item in user.Drawers)
                {
                    Drawers.Add(new DrawerDtoLight(item));
                }
            }
        }
    }
}
