using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ImageCRUD.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Full Name")]
        public string? Name { get; set; }
        [DisplayName("Age")]
        public string? Age { get; set; }
        [DisplayName("Education")]
        public string? Education { get; set; }
        [DisplayName("Image")]
        public string? Image { get; set; }
        [NotMapped]
        [DisplayName("Choose image")]
        public IFormFile Imagepath { get; set; }
    }
}
