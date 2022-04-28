using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Practica3.Web.Migrations
{
    public partial class AdicionMunicipioBarrio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Alumnos_Nombres",
                table: "Alumnos");

            migrationBuilder.AlterColumn<string>(
                name: "Documento",
                table: "Alumnos",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Municipios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AlumnoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Municipios_Alumnos_AlumnoId",
                        column: x => x.AlumnoId,
                        principalTable: "Alumnos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Barrios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MunicipioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Barrios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Barrios_Municipios_MunicipioId",
                        column: x => x.MunicipioId,
                        principalTable: "Municipios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alumnos_Documento",
                table: "Alumnos",
                column: "Documento",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Barrios_MunicipioId",
                table: "Barrios",
                column: "MunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_Barrios_Nombre",
                table: "Barrios",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Municipios_AlumnoId",
                table: "Municipios",
                column: "AlumnoId");

            migrationBuilder.CreateIndex(
                name: "IX_Municipios_Nombre",
                table: "Municipios",
                column: "Nombre",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Barrios");

            migrationBuilder.DropTable(
                name: "Municipios");

            migrationBuilder.DropIndex(
                name: "IX_Alumnos_Documento",
                table: "Alumnos");

            migrationBuilder.AlterColumn<string>(
                name: "Documento",
                table: "Alumnos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Alumnos_Nombres",
                table: "Alumnos",
                column: "Nombres",
                unique: true);
        }
    }
}
