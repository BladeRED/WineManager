using WineManager.Entities;

namespace WineManager.DTO
{
    public class UserDTOLight
    {
        public int? UserId { get; set; }
        public string? Name { get; set; }

        public UserDTOLight(User user)
        {
            if (user != null)
            {
                UserId = user.UserId;
                Name = user.Name;
            }
        }
    }
}
