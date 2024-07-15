namespace EfCoreProjectInternship.Domain
{
    public class User : BaseDomainModel    {
        public string Name { get; set; }

        public virtual List<Blog> Blogs { get; set; } = new List<Blog>();
    }
}
