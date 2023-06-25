using GrennyWebApplication.Database.Models.Common;

namespace GrennyWebApplication.Database.Models
{
    public class PlantDiscont : BaseEntity<int>, IAuditable
    {
        public int PlantId { get; set; }
        public Plant? Plant { get; set; }
        public int DiscontId { get; set; }
        public Discont? Discont { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
