using System;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Entities;

namespace BLL.Dtos
{
    public class ArticleDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string CategoryName { get; set; }
        public string SourceName { get; set; }
        public string URL { get; set; }
        public string AuthorName { get; set; }
        public DateTime DateCreated { get; set; } // time of creation on source
        public DateTime DateUploaded { get; set; } // time of uploading into db
        public string Text { get; set; }
        public byte[] Picture { get; set; }
        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }
    }
}