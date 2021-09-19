using Microsoft.EntityFrameworkCore.Migrations;

namespace TenantApplication.Migrations
{
    public partial class CascadeDeleteBehaviourAddedToTenant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tenants_Tenants_ParentTenantId",
                table: "Tenants");

            migrationBuilder.AddForeignKey(
                name: "FK_Tenants_Tenants_ParentTenantId",
                table: "Tenants",
                column: "ParentTenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tenants_Tenants_ParentTenantId",
                table: "Tenants");

            migrationBuilder.AddForeignKey(
                name: "FK_Tenants_Tenants_ParentTenantId",
                table: "Tenants",
                column: "ParentTenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
