using System;
using System.Collections.Generic;

namespace Swastika.Domain_Entities._tmp
{
    public partial class BlogPost
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? CreateDateTime { get; set; }
    }
}
