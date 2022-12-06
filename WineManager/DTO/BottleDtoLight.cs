using WineManager.Entities;

namespace WineManager.DTO
{
    public class BottleDtoLight
    {
        public int BottleId { get; set; }
        public string Name { get; set; }

        public BottleDtoLight(Bottle bottle)
        {
            if(bottle != null)
            {
                BottleId = bottle.BottleId;
                Name = bottle.Name;
            }
        }
    }
}
