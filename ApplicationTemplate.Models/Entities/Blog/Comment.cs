using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationTemplate.Models.Entities.Blog
{
    [Table("Blog.Comments")]
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string AuthorEmail { get; set; }

        [Required]
        public string DisplayName { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime TimeWritten { get; set; }

        [Required]
        public string AuthorIpAddress { get; set; }

        [Required]
        public int PostId { get; set; }
        
        [ForeignKey("PostId")]
        public Post Post { get; set; }
    }
}
