using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeduEcommance.Migrations
{
    /// <inheritdoc />
    public partial class updateProduct2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
               name: "Slug",
               table: "Products",
                maxLength: 250,
               type: "varchar(250)",
               nullable: false
               );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
               name: "Slug",
               table: "Products");
        }
    }
}
