using EfCoreProjectInternship.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace EfCoreProjectInternship.Data.Configurations
{
    internal class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder) 
        {
            builder.HasQueryFilter(x => x.isDeleted == false);
        }
    }
}
