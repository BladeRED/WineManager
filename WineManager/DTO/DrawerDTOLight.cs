using WineManager.Entities;

namespace WineManager.DTO
{
    public class DrawerDtoLight
    {
        public int DrawerId { get; set; }
        public int? Level { get; set; }
        public int MaxPosition { get; set; }

        public DrawerDtoLight(Drawer drawer)
        {
            if (drawer != null)
            {
                DrawerId = drawer.DrawerId;
                Level = drawer.Level;
                MaxPosition = drawer.MaxPosition;
            }
        }
    }
}