using Microsoft.EntityFrameworkCore.Migrations;

namespace TenantApplication.Migrations
{
    public partial class CascadeDeleteBehaviorAddedToTenantWokrflow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TenantWorkflows_Tenants_TenantId",
                table: "TenantWorkflows");

            migrationBuilder.AddForeignKey(
                name: "FK_TenantWorkflows_Tenants_TenantId",
                table: "TenantWorkflows",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TenantWorkflows_Tenants_TenantId",
                table: "TenantWorkflows");

            migrationBuilder.AddForeignKey(
                name: "FK_TenantWorkflows_Tenants_TenantId",
                table: "TenantWorkflows",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
