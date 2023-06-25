﻿using System.ComponentModel.DataAnnotations;

namespace GrennyWebApplication.Areas.Admin.ViewModels.Slider
{
    public class AddSliderViewModel
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string OfferContext { get; set; }
        public string Content { get; set; }
        public string ButtonName { get; set; }
        public string ButtonRedirectUrl { get; set; }
        public int Order { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
    }
}
