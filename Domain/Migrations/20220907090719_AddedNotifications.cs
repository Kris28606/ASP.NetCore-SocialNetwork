using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    public partial class AddedNotifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    FromWhoId = table.Column<int>(type: "int", nullable: false),
                    ForWhoId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => new { x.ForWhoId, x.FromWhoId, x.Date });
                    table.ForeignKey(
                        name: "FK_Notifications_AspNetUsers_ForWhoId",
                        column: x => x.ForWhoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Notifications_AspNetUsers_FromWhoId",
                        column: x => x.FromWhoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "CommentNotifications",
                columns: table => new
                {
                    FromWhoId = table.Column<int>(type: "int", nullable: false),
                    ForWhoId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentNotifications", x => new { x.ForWhoId, x.FromWhoId, x.Date });
                    table.ForeignKey(
                        name: "FK_CommentNotifications_AspNetUsers_ForWhoId",
                        column: x => x.ForWhoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CommentNotifications_AspNetUsers_FromWhoId",
                        column: x => x.FromWhoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CommentNotifications_Notifications_ForWhoId_FromWhoId_Date",
                        columns: x => new { x.ForWhoId, x.FromWhoId, x.Date },
                        principalTable: "Notifications",
                        principalColumns: new[] { "ForWhoId", "FromWhoId", "Date" });
                    table.ForeignKey(
                        name: "FK_CommentNotifications_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "FollowNotifications",
                columns: table => new
                {
                    FromWhoId = table.Column<int>(type: "int", nullable: false),
                    ForWhoId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowNotifications", x => new { x.ForWhoId, x.FromWhoId, x.Date });
                    table.ForeignKey(
                        name: "FK_FollowNotifications_AspNetUsers_ForWhoId",
                        column: x => x.ForWhoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_FollowNotifications_AspNetUsers_FromWhoId",
                        column: x => x.FromWhoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_FollowNotifications_Notifications_ForWhoId_FromWhoId_Date",
                        columns: x => new { x.ForWhoId, x.FromWhoId, x.Date },
                        principalTable: "Notifications",
                        principalColumns: new[] { "ForWhoId", "FromWhoId", "Date" });
                });

            migrationBuilder.CreateTable(
                name: "LikeNotifications",
                columns: table => new
                {
                    FromWhoId = table.Column<int>(type: "int", nullable: false),
                    ForWhoId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikeNotifications", x => new { x.ForWhoId, x.FromWhoId, x.Date });
                    table.ForeignKey(
                        name: "FK_LikeNotifications_AspNetUsers_ForWhoId",
                        column: x => x.ForWhoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LikeNotifications_AspNetUsers_FromWhoId",
                        column: x => x.FromWhoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LikeNotifications_Notifications_ForWhoId_FromWhoId_Date",
                        columns: x => new { x.ForWhoId, x.FromWhoId, x.Date },
                        principalTable: "Notifications",
                        principalColumns: new[] { "ForWhoId", "FromWhoId", "Date" });
                    table.ForeignKey(
                        name: "FK_LikeNotifications_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentNotifications_FromWhoId",
                table: "CommentNotifications",
                column: "FromWhoId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentNotifications_PostId",
                table: "CommentNotifications",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowNotifications_FromWhoId",
                table: "FollowNotifications",
                column: "FromWhoId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeNotifications_FromWhoId",
                table: "LikeNotifications",
                column: "FromWhoId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeNotifications_PostId",
                table: "LikeNotifications",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_FromWhoId",
                table: "Notifications",
                column: "FromWhoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentNotifications");

            migrationBuilder.DropTable(
                name: "FollowNotifications");

            migrationBuilder.DropTable(
                name: "LikeNotifications");

            migrationBuilder.DropTable(
                name: "Notifications");
        }
    }
}
