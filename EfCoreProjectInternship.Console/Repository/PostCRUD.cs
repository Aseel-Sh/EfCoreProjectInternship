
using EfCoreProjectInternship.Data;
using EfCoreProjectInternship.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

public class PostCRUD(BloggingDbContext context) {
    public async Task AddPost()
    {
        Console.WriteLine("Enter the Id of the Blog:");
        int blogId = Convert.ToInt32(Console.ReadLine());

        var blog = await context.Blogs.FindAsync(blogId);
        if (blog == null)
        {
            Console.WriteLine("Blog not found.");
        }
        else
        {
            Console.WriteLine("Content of Post:");
            string PostContent = Console.ReadLine();

            var newPost = new Post
            {
                Content = PostContent,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                isDeleted = false,
                BlogId = blogId
            };

            try
            {
                await context.Posts.AddAsync(newPost);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
    //get sp way
    public async Task ViewAllPostsForABlog()
    {

            Console.WriteLine("Enter Blog Id: ");
            int blogId = Convert.ToInt32(Console.ReadLine());

            var idParam = new SqlParameter("@BlogId", blogId);

            var posts = await context.Posts
                .FromSqlRaw("EXEC GetPosts @BlogId", idParam)
                .IgnoreQueryFilters()
                .ToListAsync();

            if (posts.Count == 0)
            {
                Console.WriteLine("No posts found for the given blog.");
            }
            else
            {
                foreach (var post in posts)
                {
                    Console.WriteLine($"Post ID: {post.Id}, Content: {post.Content} | Row Version: {BitConverter.ToString(post.RowVersion).Replace("-", "")}");
                }
            }
    }
    //old way
    /*public async Task ViewAllPostsForABlog()
    {
        Console.WriteLine("Enter Blog Id: ");
        int blogId = Convert.ToInt32(Console.ReadLine());

        var posts = await context.Posts
            .Where(p => p.BlogId == blogId)
            .Include(p => p.Blog)
            .AsNoTracking()
            .ToListAsync();

        if (posts.Count == 0)
        {
            Console.WriteLine("No posts found for the given blog.");
        }
        else
        {
            foreach (var post in posts)
            {
                Console.WriteLine($"Post ID: {post.Id}, Blog Title: {post.Blog.Title}, Content: {post.Content} | Row Version: {BitConverter.ToString(post.RowVersion).Replace("-", "")}");
            }
        }
    }*/

}
