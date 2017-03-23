using System;
using System.Collections.Generic;

namespace Swastika.Domain.Entities.Blog
{
    public class BlogPost : IEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? CreateDateTime { get; set; }
    }
}
