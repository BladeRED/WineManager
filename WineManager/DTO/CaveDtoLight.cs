using WineManager.Entities;

namespace WineManager.DTO
{
    public class CaveDtoLight
    {
        public int CaveId { get; set; }
        public string Brand { get; set; }

        public CaveDtoLight(Cave? cave)
        {
            if (cave != null)
            {
                CaveId = cave.CaveId;
                if (cave.Brand != null)
                    Brand = cave.Brand;
            }
        }
    }
}
