using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCoreProjectInternship.Domain
{
    public class Post : BaseDomainModel
    {
        public string Content { get; set; }
        public virtual Blog Blog { get; set; }
        public int BlogId { get; set; }
        public virtual List<Comment> Comments { get; set; } = new List<Comment>();

        [Timestamp]
        public byte[] RowVersion { get; set; }

        //in fluent api
        //modelBuilder.Entity<YourEntity>()
        //.Property(e => e.RowVersion)
        //.IsRowVersion();
    }
}
