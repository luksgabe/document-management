using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocManagements.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class createbase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Document",
                columns: table => new
                {
                    DocumentId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Description = table.Column<string>(type: "varchar(360)", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)0),
                    Url = table.Column<string>(type: "varchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(name: "Created_At", type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(name: "Updated_At", type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.DocumentId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Document");
        }
    }
}
