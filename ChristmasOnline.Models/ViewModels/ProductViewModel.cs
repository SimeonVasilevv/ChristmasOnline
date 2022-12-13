using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChristmasOnline.Models.ViewModels
{
	public class ProductViewModel
	{
        public Product Product { get; set; }

        public IEnumerable<SelectListItem> CategoryList { get; set; }

        public IEnumerable<SelectListItem> MaterialList { get; set; }
    }
}
