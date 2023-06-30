namespace GrennyWebApplication.Areas.Admin.ViewModels.Plantİmage
{
    public class PlantİmageViewModel
    {
        public int PlantId { get; set; }
        public List<ListItem>? Images { get; set; }

        public class ListItem
        {
            public int Id { get; set; }
            public string? ImageUrl { get; set; }
            public DateTime CreatedAt { get; set; }
        }
    }
}
