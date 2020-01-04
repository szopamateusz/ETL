using Microsoft.EntityFrameworkCore.Migrations;

namespace ETL.API.Migrations
{
    public partial class CreateInitialModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReviewDate = table.Column<string>(nullable: true),
                    ReviewerName = table.Column<string>(nullable: true),
                    ProductRating = table.Column<string>(nullable: true),
                    ReviewTitle = table.Column<string>(nullable: true),
                    ReviewText = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");
        }
    }
}
