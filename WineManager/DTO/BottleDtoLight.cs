using WineManager.Entities;

namespace WineManager.DTO
{
    public class BottleDtoLight
    {
        public int BottleId { get; set; }
        public string Name { get; set; }
        public UserDTOLight UserDTOLight { get; set; }
        public DrawerDTOLight DrawerDTOLight { get; set; }

        public BottleDtoLight(int bottleId, string name, UserDTOLight userDTOLight)
        {
            BottleId = bottleId;
            Name = name;
            UserDTOLight = userDTOLight;
        }

        public BottleDtoLight(int bottleId, string name, DrawerDTOLight drawerDTOLight)
        {
            BottleId = bottleId;
            Name = name;
            DrawerDTOLight = drawerDTOLight;
        }
    }
}
