using WineManager.Entities;

namespace WineManager.DTO
{
    public class CaveDtoGet
    {
        public int CaveId { get; set; }
        public UserDTOLight UserDTOLight { get; set; }
        public List<DrawerDtoLight> DrawersDtoLight { get; set; }

        public CaveDtoGet(int id, UserDTOLight userDTOLight)
        {
            CaveId = id;
            UserDTOLight = userDTOLight;
        }
        public CaveDtoGet(int id,  List<Drawer> drawers)
        {
            CaveId = id;
            DrawersDtoLight = new List<DrawerDtoLight>();
            foreach (var item in drawers)
            {
                DrawersDtoLight.Add(new DrawerDtoLight(item));
            }
        }
    }
}
