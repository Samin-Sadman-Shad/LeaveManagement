using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR.LeaveManagement.Persistance.Migrations
{
    public partial class AddEmployeeIdToLeaveRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "leaveTypes");

            migrationBuilder.DropColumn(
                name: "AxtionTakenById",
                table: "leaveRequests");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "leaveRequests");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "leaveAllocations");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "leaveTypes",
                newName: "LastModifiedById");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "leaveRequests",
                newName: "RequestingEmployeeId");

            migrationBuilder.RenameColumn(
                name: "ActionTakenBy",
                table: "leaveRequests",
                newName: "LastModifiedById");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "leaveAllocations",
                newName: "LastModifiedById");

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "leaveTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ActionTakenById",
                table: "leaveRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "leaveRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "leaveAllocations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "leaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2025, 4, 5, 3, 17, 23, 245, DateTimeKind.Local).AddTicks(7625));

            migrationBuilder.UpdateData(
                table: "leaveTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2025, 4, 5, 3, 17, 23, 245, DateTimeKind.Local).AddTicks(7641));

            migrationBuilder.UpdateData(
                table: "leaveTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2025, 4, 5, 3, 17, 23, 245, DateTimeKind.Local).AddTicks(7643));

            migrationBuilder.UpdateData(
                table: "leaveTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2025, 4, 5, 3, 17, 23, 245, DateTimeKind.Local).AddTicks(7644));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "leaveTypes");

            migrationBuilder.DropColumn(
                name: "ActionTakenById",
                table: "leaveRequests");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "leaveRequests");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "leaveAllocations");

            migrationBuilder.RenameColumn(
                name: "LastModifiedById",
                table: "leaveTypes",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "RequestingEmployeeId",
                table: "leaveRequests",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "LastModifiedById",
                table: "leaveRequests",
                newName: "ActionTakenBy");

            migrationBuilder.RenameColumn(
                name: "LastModifiedById",
                table: "leaveAllocations",
                newName: "LastModifiedBy");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "leaveTypes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "AxtionTakenById",
                table: "leaveRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "leaveRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "leaveAllocations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "leaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedBy", "DateCreated" },
                values: new object[] { "admin", new DateTime(2025, 4, 4, 11, 53, 45, 618, DateTimeKind.Local).AddTicks(7444) });

            migrationBuilder.UpdateData(
                table: "leaveTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedBy", "DateCreated" },
                values: new object[] { "admin", new DateTime(2025, 4, 4, 11, 53, 45, 618, DateTimeKind.Local).AddTicks(7460) });

            migrationBuilder.UpdateData(
                table: "leaveTypes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedBy", "DateCreated" },
                values: new object[] { "admin", new DateTime(2025, 4, 4, 11, 53, 45, 618, DateTimeKind.Local).AddTicks(7462) });

            migrationBuilder.UpdateData(
                table: "leaveTypes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedBy", "DateCreated" },
                values: new object[] { "admin", new DateTime(2025, 4, 4, 11, 53, 45, 618, DateTimeKind.Local).AddTicks(7463) });
        }
    }
}
