using WineManager.DTO;

namespace WineManager.Entities
{
    public class Cave
    {
        public int CaveId { get; set; }
        public string CaveType { get; set; }
        public string Family { get; set; }
        public string Brand { get; set; }
        public int? Temperature { get; set; }
        public int NbMaxDrawer { get; set; }
        public int NbMaxBottlePerDrawer { get; set; }


        // Navigation Properties //
        public List<Drawer>? Drawers { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }

        public Cave()
        {
        }

        public Cave(int caveId, string caveType, string family, string brand, int temperature, int nbMaxDrawer, int nbMaxBottlePerDrawer, List<Drawer> drawers, User user)
        {
            CaveId = caveId;
            CaveType = caveType;
            Family = family;
            Brand = brand;
            Temperature = temperature;
            Drawers = drawers;
            NbMaxDrawer = nbMaxDrawer;
            NbMaxBottlePerDrawer = nbMaxBottlePerDrawer;
            User = user;
        }


        public Cave(CaveDto caveDto, int userId)
        {
            CaveType = caveDto.CaveType;
            Family = caveDto.Family;
            Brand = caveDto.Brand;
            Temperature = caveDto.Temperature;
            NbMaxDrawer = caveDto.NbMaxDrawer;
            NbMaxBottlePerDrawer = caveDto.NbMaxBottlePerDrawer;
            UserId = userId;
        }
    }
}
