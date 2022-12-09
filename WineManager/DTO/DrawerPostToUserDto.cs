using WineManager.Entities;

namespace WineManager.DTO
{
    public class DrawerPostToUserDto
    {
        public int MaxPosition { get; set; }
        public int? Level { get; set; }
        public int? CaveId { get; set; }
    }
}
