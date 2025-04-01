using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalSearchingPlatform.Data.Entities
{
    public class ArticleLike
    {
        [Key, Column(Order = 0)]
        [Required(ErrorMessage = "ArticleId is required"), ForeignKey("Article")]
        public string ArticleId { get; set; }
        public virtual Article Article { get; set; }

        [Key, Column(Order = 1)]
        [Required(ErrorMessage = "UserId is required"), ForeignKey("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        [Required(ErrorMessage = "Like At is required")]
        public DateTime LikeAt { get; set; }
    }
}
