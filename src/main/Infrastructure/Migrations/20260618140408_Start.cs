using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JacksonVeroneze.NET.GRPCServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Start : Migration
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
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    full_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    birth_date = table.Column<DateOnly>(type: "date", nullable: false),
                    gender = table.Column<int>(type: "integer", nullable: false),
                    cpf = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    phone_number = table.Column<long>(type: "bigint", nullable: false),
                    mother_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    father_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    nationality = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    street = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    address_number = table.Column<int>(type: "integer", nullable: false),
                    neighborhood = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    city = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    state = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    zip_code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    country = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    dependents_count = table.Column<int>(type: "integer", nullable: false),
                    is_politically_exposed = table.Column<bool>(type: "boolean", nullable: false),
                    is_email_verified = table.Column<bool>(type: "boolean", nullable: false),
                    risk_level = table.Column<int>(type: "integer", nullable: false),
                    latitude = table.Column<double>(type: "double precision", nullable: false),
                    longitude = table.Column<double>(type: "double precision", nullable: false),
                    activated_on_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    inactivated_on_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    version = table.Column<int>(type: "integer", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_profile", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ux_profile_cpf",
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
