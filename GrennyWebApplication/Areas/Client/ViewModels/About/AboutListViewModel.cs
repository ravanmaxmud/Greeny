using GrennyWebApplication.Areas.Client.ViewModels.Home.Index;

namespace GrennyWebApplication.Areas.Client.ViewModels.About
{
    public class AboutListViewModel
    {
        public List<AboutListItemViewModel> Abouts { get; set; }
        public List<TeamMemberListItemViewModel> TeamMembers { get; set; }
        public List<FeedBackListItemViewModel> FeedBacks { get; set; }
    }
}
