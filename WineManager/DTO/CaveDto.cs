using WineManager.Entities;

namespace WineManager.DTO
{
    public class CaveDto
    {
        public int CaveId { get; set; }
        public string CaveType { get; set; }
        public string Family { get; set; }
        public string Brand { get; set; }
        public int? Temperature { get; set; }
        public int NbMaxDrawer { get; set; }
        public int NbMaxBottlePerDrawer { get; set; }
        public int? UserId { get; set; }

        public CaveDto(Cave cave)
        {
            CaveId = cave.CaveId;
            CaveType = cave.CaveType;
            if (cave.Family != null)
            {
                Family = cave.Family;
            }
            if (cave.Brand != null)
            {
                Brand = cave.Brand;
            }
            Temperature = cave.Temperature;
            NbMaxDrawer = cave.NbMaxDrawer;
            NbMaxBottlePerDrawer = cave.NbMaxBottlePerDrawer;
            if (cave.UserId > 0)
            {
                UserId = cave.UserId;
            }

        }
        public CaveDto()
        {

        }
    }
}
