using WineManager.Entities;

namespace WineManager.DTO
{
    public class CavePutDto
    {
        public int CaveId { get; set; }
        public string CaveType { get; set; }
        public string Family { get; set; }
        public string Brand { get; set; }
        public int Temperature { get; set; }
        public CavePutDto()
        {

        }
    }
}
