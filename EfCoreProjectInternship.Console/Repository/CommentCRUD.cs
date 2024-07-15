// See https://aka.ms/new-console-template for more information


using EfCoreProjectInternship.Data;
using EfCoreProjectInternship.Domain;

public class CommentCRUD(BloggingDbContext context) {
    public async Task AddComment()
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

    public async Task AddCommentToAPost()
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

}
