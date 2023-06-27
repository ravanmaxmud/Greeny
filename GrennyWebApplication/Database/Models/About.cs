using GrennyWebApplication.Database.Models.Common;

namespace GrennyWebApplication.Database.Models
{
    public class About : BaseEntity<int>, IAuditable
    {
         
        public string Context { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
