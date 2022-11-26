using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChristmasOnlineWeb.Models
{
    public class Category
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString(); //string заради базата ли е? Ще приеме ли GUID?
        [Required]
        [StringLength(40,MinimumLength = 4,ErrorMessage ="{0} must be between {2} and {1} symbols")]
        public string Name { get; set; }
        //[DisplayName("Display Order")]
        //public int DisplayOrder { get; set; }
       
        [DisplayName("Created On")]
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
