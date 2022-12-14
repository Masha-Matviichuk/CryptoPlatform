using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Article : BaseEntity
    {
        public string Title { get; set; }
        [ForeignKey(nameof(Category))]
        public string CategoryId { get; set; }
        [ForeignKey(nameof(Source))]
        public string SourceId { get; set; }
        public string URL { get; set; }
        public string AuthorName { get; set; }
        public DateTime DateCreated { get; set; } // time of creation on source
        public DateTime DateUploaded { get; set; } // time of uploading into db
        public string Text { get; set; }
        [ForeignKey(nameof(Picture))]
        public string PictureId { get; set; }
        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }
        
        public virtual Picture Picture { get; set; }
        public virtual Category Category { get; set; }
        public virtual Source Source { get; set; }
    }
}