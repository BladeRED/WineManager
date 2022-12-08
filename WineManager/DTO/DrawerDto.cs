using WineManager.Entities;

namespace WineManager.DTO
{
    public class DrawerDto
    {
        public int DrawerId { get; set; }
        public int Level { get; set; }
        public int MaxPosition { get; set; }
        public int? CaveId { get; set; }

        public DrawerDto(Drawer drawer)
        {
            DrawerId = drawer.DrawerId;
            Level = drawer.Level;
            MaxPosition = drawer.MaxPosition;

            if (drawer.CaveId != null)
            {
                CaveId = drawer.CaveId;
            }

        }
    }
}
