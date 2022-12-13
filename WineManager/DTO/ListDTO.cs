using WineManager.Entities;

namespace WineManager.DTO
{
    public class ListDTO
    {

        public List<BottleDto> Bottles { get; set; }
        public List<DrawerDto> Drawers { get; set; }
        public List<CaveDto> Caves { get; set; }

        public ListDTO()
        {

        }
        public ListDTO(List<Bottle>? bottles, List<Drawer>? drawers, List<Cave>? caves)
        {
            if (bottles != null)
            {
                Bottles = new List<BottleDto>();
                foreach (var item in bottles)
                {
                    Bottles.Add(new BottleDto(item));
                }
            }

            if (drawers != null)
            {
                Drawers = new List<DrawerDto>();
                foreach (var item in drawers)
                {
                    Drawers.Add(new DrawerDto(item));
                }
            }
            if (caves != null)
            {
                Caves = new List<CaveDto>();
                foreach (var item in caves)
                {
                    Caves.Add(new CaveDto(item));
                }
            }
        }
    }
}
