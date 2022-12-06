using WineManager.Entities;

namespace WineManager.DTO
{
    public class DrawerDTOLight
    {
        public int? DrawerId { get; set; }

        public DrawerDTOLight(Drawer drawer)
        {
            if (drawer != null)
                DrawerId = drawer.DrawerId;
        }
    }
}