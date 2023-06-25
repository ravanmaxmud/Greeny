using GrennyWebApplication.Database.Models.Enums;

namespace GrennyWebApplication.Areas.Admin.ViewModels.Order
{
    public class UpdateOrderViewModel
    {

        public string Id { get; set; }
        public OrderStatus Status { get; set; }
       

    }
}
