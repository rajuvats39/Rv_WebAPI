using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rv_WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class BlogApp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlogAppCatagoryModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogAppCatagoryModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlogAppModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogAppModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogAppModel_BlogAppCatagoryModel_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "BlogAppCatagoryModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogAppModel_CategoryId",
                table: "BlogAppModel",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogAppModel");

            migrationBuilder.DropTable(
                name: "BlogAppCatagoryModel");
        }
    }
}
