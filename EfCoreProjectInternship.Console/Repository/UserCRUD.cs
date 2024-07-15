

using EfCoreProjectInternship.Data;
using EfCoreProjectInternship.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class UserCRUD(BloggingDbContext context) {
    public async Task AddUser()
    {
        Console.WriteLine("Enter the name of the user:");
        string name = Console.ReadLine();

        SqlParameter[] parameters = new SqlParameter[] {
            new SqlParameter("@Name", name),
            new SqlParameter("@CreatedDate", DateTime.UtcNow),
            new SqlParameter("@ModifiedDate", DateTime.UtcNow),
            new SqlParameter("@isDeleted", false)
        };

        await context.Database.ExecuteSqlRawAsync("INSERT INTO Users (Name, CreatedDate, ModifiedDate, isDeleted) VALUES (@Name, @CreatedDate, @ModifiedDate, @isDeleted)", parameters);
    }

    public async Task SoftDeleteUser()
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

}
