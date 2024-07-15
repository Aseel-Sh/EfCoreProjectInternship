// See https://aka.ms/new-console-template for more information


using EfCoreProjectInternship.Data;
using EfCoreProjectInternship.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

using var context = new BloggingDbContext();

int x = 0;
printMenu(x);

do
{
    x = Convert.ToInt32(Console.ReadLine());
    printMenu(x);

    switch (x)
    {   
        case 1:
            await AddUser();
            printMenu(x);
            break;
        case 2:
            await AddBlog();
            printMenu(x);
            break;
        case 3:
            await AddPost();
            printMenu(x);
            break;
        case 4:
            await AddComment();
            printMenu(x);
            break;
        case 5:
            await ViewAllPostsForABlog();
            printMenu(x);
            break;
        case 6:
            await AddCommentToAPost();
            printMenu(x);
            break;
        case 7:
            await SoftDeleteUser();
            printMenu(x);
            break;
        case 8:
            break;
        default:
            Console.WriteLine("Invalid Option");
            printMenu(x);
            break;
    }
} while (x != 8);

#region Implement CRUD Functions
async Task SoftDeleteUser()
{
    Console.WriteLine("Enter the Id of the user to delete:");
    int userIdToDelete = Convert.ToInt32(Console.ReadLine());

    var user = await context.Users.FindAsync(userIdToDelete);
    if (user != null)
    {
        user.isDeleted = true;
        user.ModifiedDate = DateTime.Now; 
        await context.SaveChangesAsync();
    }
}

async Task AddCommentToAPost()
{
    Console.WriteLine("Enter the Id of the Post:");
    int postId = Convert.ToInt32(Console.ReadLine());

    var post = await context.Posts.FindAsync(postId);
    if (post == null)
    {
        Console.WriteLine("Post not found.");
    }
    else
    {

        Console.WriteLine("Enter the Text of the Comment:");
        string commentText = Console.ReadLine();

        var newComment = new Comment
        {
            Text = commentText,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now,
            isDeleted = false,
            PostId = postId
        };

        await context.Comments.AddAsync(newComment);
        await context.SaveChangesAsync();
    }
}

async Task ViewAllPostsForABlog()
{
    Console.WriteLine("Enter Blog Id: ");
    int blogId = Convert.ToInt32(Console.ReadLine());

    var blog = await context.Blogs
        .Where(q => q.Id == blogId)
        .Include(q => q.Posts)
        .FirstOrDefaultAsync();

    if (blog == null)
    {
        Console.WriteLine("Blog not found.");
    }
    else
    {
        if (blog.Posts.Count == 0)
        {
            Console.WriteLine("No posts in this blog.");
        }
        else
        {
            foreach (var post in blog.Posts)
            {
                Console.WriteLine($"Post ID: {post.Id}, Title: {blog.Title}, Content: {post.Content}");
            }
        }

    }
}

async Task AddComment()
{
    Console.WriteLine("Is this a reply to another comment? (y/n):");
    string isReply = Console.ReadLine();

    int? parentCommentId = null;
    if (isReply.ToLower() == "y")
    {
        Console.WriteLine("Enter the Id of the Parent Comment:");
        parentCommentId = Convert.ToInt32(Console.ReadLine());

        var parentComment = await context.Comments.FindAsync(parentCommentId);
        if (parentComment == null)
        {
            Console.WriteLine("Parent Comment Id not found will be set to Null.");
            parentCommentId = null;
        }
    }

    Console.WriteLine("Enter the Text of the Comment:");
    string commentText = Console.ReadLine();

    var newComment = new Comment
    {
        Text = commentText,
        CreatedDate = DateTime.Now,
        ModifiedDate = DateTime.Now,
        isDeleted = false,
        ParentCommentId = parentCommentId
    };

    await context.Comments.AddAsync(newComment);
    await context.SaveChangesAsync();

}

async Task AddPost()
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


async Task AddBlog()
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


async Task AddUser()
{
    Console.WriteLine("Enter the name of the user:");
    string name = Console.ReadLine();

    var newUser = new User
    {
        Name = name,
        CreatedDate = DateTime.Now,
        ModifiedDate = DateTime.Now,
        isDeleted = false
    };

    await context.Users.AddAsync(newUser);
    await context.SaveChangesAsync();
}

#endregion
void printMenu(int x)
{
    if (x != 8)
    {
        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine("1. Add User\n2. Add Blog\n3. Add Post\n4. Add Comment\n5. View All Posts for a Blog\n6. Add Comment to Post\n7. Soft Delete User\n8. Exit");
        Console.WriteLine("-------------------------------------------------");
    }
}
