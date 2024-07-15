// See https://aka.ms/new-console-template for more information


using EfCoreProjectInternship.Data;
using EfCoreProjectInternship.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Reflection.Metadata;

//do one for get using stored procedure

using var context = new BloggingDbContext();
var User = new UserCRUD(context);
var Blog = new BlogCRUD(context);
var Post = new PostCRUD(context);
var Comment = new CommentCRUD(context);

int x = 0;
printMenu(x);

do
{
    x = Convert.ToInt32(Console.ReadLine());
    printMenu(x);

    switch (x)
    {   
        case 1:
            await User.AddUser();
            printMenu(x);
            break;
        case 2:
            await Blog.AddBlog();
            printMenu(x);
            break;
        case 3:
            await Post.AddPost();
            printMenu(x);
            break;
        case 4:
            await Comment.AddComment();
            printMenu(x);
            break;
        case 5:
            await Post.ViewAllPostsForABlog();
            printMenu(x);
            break;
        case 6:
            await Comment.AddCommentToAPost();
            printMenu(x);
            break;
        case 7:
            await User.SoftDeleteUser();
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

void printMenu(int x)
{
    if (x != 8)
    {
        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine("1. Add User\n2. Add Blog\n3. Add Post\n4. Add Comment\n5. View All Posts for a Blog\n6. Add Comment to Post\n7. Soft Delete User\n8. Exit");
        Console.WriteLine("-------------------------------------------------");
    }
}
