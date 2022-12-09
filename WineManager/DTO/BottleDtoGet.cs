using WineManager.Entities;

namespace WineManager.DTO
{
    public class BottleDtoGet
    {
        public int BottleId { get; set; }
        public string Name { get; set; }
        public UserDTOLight? User { get; set; }
        public DrawerDtoLight? Drawer { get; set; }

        public BottleDtoGet(int bottleId, string name, UserDTOLight userDTOLight)
        {
            BottleId = bottleId;
            Name = name;
            User = userDTOLight;
        }

        public BottleDtoGet(int bottleId, string name, DrawerDtoLight drawerDTOLight)
        {
            BottleId = bottleId;
            Name = name;
            Drawer = drawerDTOLight;
        }
    }
}
