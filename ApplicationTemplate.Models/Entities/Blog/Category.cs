using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationTemplate.Models.Entities.Blog
{
    [Table("Blog.Categories")]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        //[Index(IsUnique = true)]
        [MaxLength(100)]
        public string UrlSegment { get; set; }
    }
}
