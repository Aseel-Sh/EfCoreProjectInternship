using EfCoreProjectInternship.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCoreProjectInternship.Data.Configurations
{
    internal class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasMany(q => q.Comments)
                .WithOne(q => q.Post)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(q => q.RowVersion)
                .IsRowVersion();

            builder.HasQueryFilter(x => x.isDeleted == false);
        }
    }
}
