using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChristmasOnline.Models
{
    public class Material
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(40,MinimumLength =2,ErrorMessage ="{0} name must be between {2} and {1} characters long")]
        [Display(Name = "Material")]
        public string Name { get; set; }
    }
}
