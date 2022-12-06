namespace WineManager.DTO
{
    public class DrawerDtoGet
    {
        public int DrawerId { get; set; }
        public UserDTOLight UserDTOLight { get; set; }
        public BottleDtoLight BottleDtoLight { get; set; }
        public CaveDtoLight CaveDtoLight { get; set; }

        public DrawerDtoGet(int id, UserDTOLight userDTOLight)
        {
            DrawerId = id;
            UserDTOLight = userDTOLight;
        }
        public DrawerDtoGet(int id, BottleDtoLight bottleDtoLight)
        {
            DrawerId = id;
            BottleDtoLight = bottleDtoLight;
        }
        public DrawerDtoGet(int id, CaveDtoLight caveDtoLight)
        {
            DrawerId = id;
            CaveDtoLight = caveDtoLight;
        }
    }
}
