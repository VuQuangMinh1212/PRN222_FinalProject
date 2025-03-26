using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalSearchingPlatform.Data.Entities
{
    public class Article
    {
        [Key]
        public string ArticleId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        [ForeignKey("User")]
        public string AuthorId { get; set; }


        [Required, ForeignKey("ArticleCategory")]
        public string ArticleCategoryId { get; set; }

        [Required]
        public DateTime PublishedDate { get; set; } = DateTime.UtcNow;

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "Draft"; // Draft, Published, Archived

        public virtual User Author { get; set; }

        public virtual ArticleCategory Category { get; set; }

        public virtual ICollection<ArticleLike> Likes { get; set; } = new List<ArticleLike>();
    }
}
