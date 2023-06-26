using GrennyWebApplication.Database.Models.Common;

namespace GrennyWebApplication.Database.Models
{
    public class City : BaseEntity<int>, IAuditable
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string BgImageName { get; set; }
        public string BgImageNameInFileSystem { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
