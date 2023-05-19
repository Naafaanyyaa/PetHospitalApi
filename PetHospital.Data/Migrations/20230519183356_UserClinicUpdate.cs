using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetHospital.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserClinicUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCreator",
                table: "UserClinic",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCreator",
                table: "UserClinic");
        }
    }
}
