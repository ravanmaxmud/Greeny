using GrennyWebApplication.Database.Models.Common;

namespace GrennyWebApplication.Database.Models
{
    public class Slider : BaseEntity<int>, IAuditable
    {
        public string OfferContext { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string BgImageName { get; set; }
        public string BgImageNameInFileSystem { get; set; }
        public string ButtonName { get; set; }
        public string BtnRedirectUrl { get; set; }
        public int Order { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


    }
}
