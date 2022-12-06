using WineManager.Entities;

namespace WineManager.DTO
{
    public class DrawerDtoGet
    {
        public int DrawerId { get; set; }
        public UserDTOLight UserDTOLight { get; set; }
        public List<BottleDtoLight> BottlesDtoLight { get; set; }
        public CaveDtoLight CaveDtoLight { get; set; }

        public DrawerDtoGet(int id, UserDTOLight userDTOLight)
        {
            DrawerId = id;
            UserDTOLight = userDTOLight;
        }
        public DrawerDtoGet(int id, List<Bottle> bottles)
        {
            DrawerId = id;
            BottlesDtoLight = new List<BottleDtoLight>();
            foreach (var item in bottles)
            {
                BottlesDtoLight.Add(new BottleDtoLight(item));
            }
        }
        public DrawerDtoGet(int id, CaveDtoLight caveDtoLight)
        {
            DrawerId = id;
            CaveDtoLight = caveDtoLight;
        }
    }
}
