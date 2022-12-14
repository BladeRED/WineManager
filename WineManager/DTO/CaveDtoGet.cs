using WineManager.Entities;

namespace WineManager.DTO
{
    public class CaveDtoGet
    {
        public int CaveId { get; set; }
        public UserDTOLight User { get; set; }
        public List<DrawerDtoLight> Drawers { get; set; }

        public CaveDtoGet(int id, UserDTOLight userDTOLight)
        {
            CaveId = id;
            User = userDTOLight;
        }
        public CaveDtoGet(int id, List<Drawer>? drawers)
        {
            CaveId = id;
            if (drawers != null)
            {
                Drawers = new List<DrawerDtoLight>();
                foreach (var item in drawers)
                {
                    Drawers.Add(new DrawerDtoLight(item));
                }
            }
        }
    }
}
