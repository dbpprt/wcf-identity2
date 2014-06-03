using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationTemplate.Models.Entities.Blog
{
    [Table("Blog.Posts")]
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Teaser { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime TimeWritten { get; set; }

        // TODO: String?
        public string Author { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public PostStatus PostStatus { get; set; }

        //[Index(IsUnique = true)]
        [MaxLength(100)]
        public string UrlSegment { get; set; }
    }
}
