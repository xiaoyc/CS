
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonSpider.DBHelper.Models
{
    public class Post
    {

        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string VideoUrl { get; set; }
        public byte[] Image { get; set; }
        public string OriginalPageUrl { get; set; }
        public int OriginalPageId { get; set; }
        public long CategoryId { get; set; }

        public bool IsDraft { get; set; }
        public string CategoryName { get; set; }


        public string Tags { get; set; }

        public string Actors { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
