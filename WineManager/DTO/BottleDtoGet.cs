using WineManager.Entities;

namespace WineManager.DTO
{
    public class BottleDtoGet
    {
        public int BottleId { get; set; }
        public string Name { get; set; }
        public UserDTOLight UserDTOLight { get; set; }
        public DrawerDtoLight DrawerDTOLight { get; set; }

        public BottleDtoGet(int bottleId, string name, UserDTOLight userDTOLight)
        {
            BottleId = bottleId;
            Name = name;
            UserDTOLight = userDTOLight;
        }

        public BottleDtoGet(int bottleId, string name, DrawerDtoLight drawerDTOLight)
        {
            BottleId = bottleId;
            Name = name;
            DrawerDTOLight = drawerDTOLight;
        }
    }
}
