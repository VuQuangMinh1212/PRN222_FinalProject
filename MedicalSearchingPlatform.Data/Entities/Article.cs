using System;
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
        public User Author { get; set; }

        [Required]
        public DateTime PublishedDate { get; set; } = DateTime.UtcNow;

        [MaxLength(100)]
        public string Category { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "Draft"; // Draft, Published, Archived
    }
}
