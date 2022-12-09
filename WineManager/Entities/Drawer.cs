using WineManager.DTO;

namespace WineManager.Entities
{
    public class Drawer
    {
        public int DrawerId { get; set; }
        public int? Level { get; set; }
        public int MaxPosition { get; set; }

        // Navigation properties //
        public List<Bottle>? Bottles { get; set; }
        public int? CaveId { get; set; }
        public Cave? Cave { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }

        public Drawer(DrawerDto drawerDto, int userId)
        {
            Level = drawerDto.Level;
            MaxPosition = drawerDto.MaxPosition;
            CaveId = drawerDto.CaveId;
            UserId = userId;
        }

        public Drawer()
        {

        }
    }
}