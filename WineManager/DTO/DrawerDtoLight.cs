using WineManager.Entities;

namespace WineManager.DTO
{
    public class DrawerDtoLight
    {
        public int? DrawerId { get; set; }

        public DrawerDtoLight(Drawer drawer)
        {
            if (drawer != null)
                DrawerId = drawer.DrawerId;
        }
    }
}