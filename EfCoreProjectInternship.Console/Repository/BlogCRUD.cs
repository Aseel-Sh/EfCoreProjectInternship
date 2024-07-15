
using EfCoreProjectInternship.Data;
using EfCoreProjectInternship.Domain;

public class BlogCRUD(BloggingDbContext context) {
    public async Task AddBlog()
    {
        Console.WriteLine("Enter the Id of the user:");
        int userId = Convert.ToInt32(Console.ReadLine());

        var user = await context.Users.FindAsync(userId);
        if (user == null)
        {
            Console.WriteLine("User not found.");
        }
        else
        {
            Console.WriteLine("Title of Blog:");
            string blogTitle = Console.ReadLine();

            var newBlog = new Blog
            {
                Title = blogTitle,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                isDeleted = false,
                UserId = userId
            };

            await context.Blogs.AddAsync(newBlog);
            await context.SaveChangesAsync();
        }
    } 
}
