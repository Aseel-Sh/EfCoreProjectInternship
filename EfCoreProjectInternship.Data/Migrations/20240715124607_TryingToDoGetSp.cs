using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfCoreProjectInternship.Data.Migrations
{
    /// <inheritdoc />
    public partial class TryingToDoGetSp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE GetPosts
               @BlogId int
               AS
               BEGIN
                   SELECT * FROM Posts WHERE BlogId = @BlogId
               END";

            migrationBuilder.Sql(sp);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sp = @"DROP PROCEDURE IF EXISTS GetPosts";
            migrationBuilder.Sql(sp);
        }
    }
}
