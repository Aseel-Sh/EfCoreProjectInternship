using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCoreProjectInternship.Domain
{
    public class Blog : BaseDomainModel
    {
        public string Title { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
        public virtual List<Post> Posts { get; set; } = new List<Post>();
    }
}
