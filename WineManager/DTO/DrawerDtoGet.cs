using WineManager.Entities;

namespace WineManager.DTO
{
    public class DrawerDtoGet
    {
        public int DrawerId { get; set; }
        public UserDTOLight User { get; set; }
        public List<BottleDtoLight> Bottles { get; set; }
        public CaveDtoLight Cave { get; set; }

        public DrawerDtoGet(int id, UserDTOLight userDTOLight)
        {
            DrawerId = id;
            User = userDTOLight;
        }

        public DrawerDtoGet(int id, List<Bottle>? bottles)
        {
            DrawerId = id;
            if (bottles != null)
            {
                Bottles = new List<BottleDtoLight>();
                foreach (var item in bottles)
                {
                    Bottles.Add(new BottleDtoLight(item));
                }
            }
        }

        public DrawerDtoGet(int id, CaveDtoLight caveDtoLight)
        {
            DrawerId = id;
            Cave = caveDtoLight;
        }
    }
}
