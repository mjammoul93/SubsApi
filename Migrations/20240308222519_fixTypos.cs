using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SubsApi.Migrations
{
    public partial class fixTypos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subsciptions_Subsciptions_SubsciptionId",
                table: "Subsciptions");

            migrationBuilder.RenameColumn(
                name: "SubsciptionId",
                table: "Subsciptions",
                newName: "SubscriptionId");

            migrationBuilder.RenameIndex(
                name: "IX_Subsciptions_SubsciptionId",
                table: "Subsciptions",
                newName: "IX_Subsciptions_SubscriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subsciptions_SubscriptionTypes_SubscriptionId",
                table: "Subsciptions",
                column: "SubscriptionId",
                principalTable: "SubscriptionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subsciptions_SubscriptionTypes_SubscriptionId",
                table: "Subsciptions");

            migrationBuilder.RenameColumn(
                name: "SubscriptionId",
                table: "Subsciptions",
                newName: "SubsciptionId");

            migrationBuilder.RenameIndex(
                name: "IX_Subsciptions_SubscriptionId",
                table: "Subsciptions",
                newName: "IX_Subsciptions_SubsciptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subsciptions_Subsciptions_SubsciptionId",
                table: "Subsciptions",
                column: "SubsciptionId",
                principalTable: "Subsciptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
