
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonSpider.DBHelper.Models
{

    public class PostTagMapping
    {
        public long Id { get; set; }
        public long PostId { get; set; }
        public long TagId { get; set; }
    }
}
