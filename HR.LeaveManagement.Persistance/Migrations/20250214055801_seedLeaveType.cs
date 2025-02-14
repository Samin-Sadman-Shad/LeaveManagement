using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR.LeaveManagement.Persistance.Migrations
{
    public partial class seedLeaveType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "leaveTypes",
                columns: new[] { "Id", "AllocatedDays", "CreatedBy", "DateCreated", "LastModifiedBy", "LastModifiedDate", "LeaveTypeName" },
                values: new object[,]
                {
                    { 1, 10, "admin", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Vacation" },
                    { 2, 14, "admin", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Sick Leaves" },
                    { 3, 15, "admin", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Compassionate Leaves" },
                    { 4, 10, "admin", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Pilgrimage" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "leaveTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "leaveTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "leaveTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "leaveTypes",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
