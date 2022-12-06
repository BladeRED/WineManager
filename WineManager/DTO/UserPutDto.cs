using WineManager.Entities;

namespace WineManager.DTO
{
    public class UserPutDto
    {
        public string? NewName { get; set; }
        public string CurrentEmail { get; set; }
        public string? NewEmail { get; set; }
        public string CurrentPassword { get; set; }
        public string? NewPassword { get; set; }
        public DateTime? NewBirthDate { get; set; }
    }
}
