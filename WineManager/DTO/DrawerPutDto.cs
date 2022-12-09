using WineManager.Entities;

namespace WineManager.DTO
{
    public class DrawerPutDto
    {
        public int DrawerId { get; set; }
        public int? Level { get; set; }

        public DrawerPutDto()
        {

        }
    }
}
