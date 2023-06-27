namespace GrennyWebApplication.Areas.Client.ViewModels.About
{
    public class TeamMemberListItemViewModel
    {
        public string FullName { get; set; }
        public string Position { get; set; }
        public string ImgUrl { get; set; }
        public TeamMemberListItemViewModel(string fullName, string position, string ımgUrl)
        {
            FullName = fullName;
            Position = position;
            ImgUrl = ımgUrl;
        }
    }
}
