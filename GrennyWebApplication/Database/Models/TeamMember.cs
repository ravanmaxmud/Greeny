using GrennyWebApplication.Database.Models.Common;

namespace GrennyWebApplication.Database.Models
{
    public class TeamMember : BaseEntity<int>, IAuditable
    {
        public string BgImageName { get; set; }
        public string BgImageNameInFileSystem { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
