using EfCoreProjectInternship.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCoreProjectInternship.Data.Configurations
{
    internal class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasMany(q => q.Comments)
                .WithOne(q => q.ParentComment)
                .HasForeignKey(q => q.ParentCommentId)
                .OnDelete(DeleteBehavior.Restrict); 


            builder.HasQueryFilter(x => x.isDeleted == false);
        }
    }
}
