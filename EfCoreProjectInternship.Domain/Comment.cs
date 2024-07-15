using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCoreProjectInternship.Domain
{
    public class Comment : BaseDomainModel
    {
        public string Text { get; set; }

        //modified in config to be self refrencing fk and one comment can have multiple comments
        public int? ParentCommentId { get; set; }
        public Comment ParentComment { get; set; }
        public virtual List<Comment> Comments  { get; set; }

        public int? PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}
