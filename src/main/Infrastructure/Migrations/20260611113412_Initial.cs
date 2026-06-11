using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JacksonVeroneze.NET.GRPCServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "profile");

            migrationBuilder.CreateTable(
                name: "profile",
                schema: "profile",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    full_name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    birth_date = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    gender = table.Column<int>(type: "INTEGER", nullable: false),
                    cpf = table.Column<string>(type: "TEXT", maxLength: 11, nullable: false),
                    status = table.Column<int>(type: "INTEGER", nullable: false),
                    email = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true),
                    phone_number = table.Column<string>(type: "TEXT", maxLength: 30, nullable: true),
                    mother_name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    father_name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    nationality = table.Column<string>(type: "TEXT", maxLength: 60, nullable: true),
                    birth_city = table.Column<string>(type: "TEXT", maxLength: 80, nullable: true),
                    birth_state = table.Column<string>(type: "TEXT", maxLength: 40, nullable: true),
                    street = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true),
                    address_number = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    complement = table.Column<string>(type: "TEXT", maxLength: 80, nullable: true),
                    neighborhood = table.Column<string>(type: "TEXT", maxLength: 80, nullable: true),
                    city = table.Column<string>(type: "TEXT", maxLength: 80, nullable: true),
                    state = table.Column<string>(type: "TEXT", maxLength: 40, nullable: true),
                    zip_code = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    country = table.Column<string>(type: "TEXT", maxLength: 60, nullable: true),
                    activated_on_utc = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    inactivated_on_utc = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "NOW()"),
                    updated_at = table.Column<DateTime>(type: "TEXT", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "TEXT", nullable: true),
                    version = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_profile", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_profile_cpf",
                schema: "profile",
                table: "profile",
                column: "cpf",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "profile",
                schema: "profile");
        }
    }
}
