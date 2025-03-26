using System.ComponentModel.DataAnnotations;

namespace MedicalSearchingPlatform.Data.Entities
{
    public class ArticleCategory
    {
        [Key]
        public string CategoryId { get; set; } = Guid.NewGuid().ToString();
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
    }
}
