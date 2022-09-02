using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    public partial class AddedPrimaryForMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Messages",
                table: "Messages");

            migrationBuilder.AddColumn<int>(
                name: "MessageId",
                table: "Messages",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Messages",
                table: "Messages",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_FromId",
                table: "Messages",
                column: "FromId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Messages",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_FromId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "MessageId",
                table: "Messages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Messages",
                table: "Messages",
                columns: new[] { "FromId", "ForId" });
        }
    }
}
