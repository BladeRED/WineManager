namespace WineManager.DTO
{
    public class CaveDtoGet
    {
        public int CaveId { get; set; }
        public UserDTOLight UserDTOLight { get; set; }
        public CaveDtoLight CaveDtoLight { get; set; }

        public CaveDtoGet(int id, UserDTOLight userDTOLight)
        {
            CaveId = id;
            UserDTOLight = userDTOLight;
        }
        public CaveDtoGet(int id, CaveDtoLight caveDtoLight)
        {
            CaveId = id;
            CaveDtoLight = caveDtoLight;
        }
    }
}
