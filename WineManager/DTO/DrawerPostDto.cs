using WineManager.Entities;

namespace WineManager.DTO
{
    public class DrawerPostDto
    {
        public int Level { get; set; }
        public int MaxPosition { get; set; }
        public int? CaveId { get; set; }
    }
}
