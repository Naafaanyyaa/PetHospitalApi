using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetHospital.Data.Migrations
{
    /// <inheritdoc />
    public partial class DiseaseFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Recommendations",
                table: "Diseases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Recommendations",
                table: "Diseases");
        }
    }
}
