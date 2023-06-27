using GrennyWebApplication.Database.Models.Common;

namespace GrennyWebApplication.Database.Models
{
    public class Comment : BaseEntity<int>, IAuditable
    {
        public string Name { get; set; }
        public string Context { get; set; }
        public string Email { get; set; }
        public int PlantId { get; set; }
        public Plant? Plant { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
