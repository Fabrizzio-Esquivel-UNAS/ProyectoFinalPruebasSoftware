using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CleanArchitecture.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Entidades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "Users",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "GivenName",
                table: "Users",
                newName: "FirstName");

            migrationBuilder.AddColumn<int>(
                name: "AsesoriaEstado",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                table: "Users",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "EscuelaId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastLoggedinDate",
                table: "Users",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LineaInvestigacionId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<byte>(
                name: "Notificaciones",
                table: "Users",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Preferencias",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Telefono",
                table: "Users",
                type: "nvarchar(9)",
                maxLength: 9,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Calendarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserUri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccessToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccessTokenExpiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RefreshTokenExpiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EventType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventsPageToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SchedulingUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Calendarios_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Citas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventoId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    DesarrolloAsesor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DesarrolloAsesorado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AsesorUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AsesoradoUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Citas_Users_AsesorUserId",
                        column: x => x.AsesorUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Citas_Users_AsesoradoUserId",
                        column: x => x.AsesoradoUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Facultades",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facultades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GruposInvestigacion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GruposInvestigacion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Solicitudes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AsesoradoUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AsesorUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumeroTesis = table.Column<int>(type: "int", nullable: false),
                    Mensaje = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    FechaRespuesta = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solicitudes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Solicitudes_Users_AsesorUserId",
                        column: x => x.AsesorUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Solicitudes_Users_AsesoradoUserId",
                        column: x => x.AsesoradoUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Escuelas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FacultadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escuelas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Escuelas_Facultades_FacultadId",
                        column: x => x.FacultadId,
                        principalTable: "Facultades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HistorialCoordinadores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GrupoInvestigacionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorialCoordinadores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistorialCoordinadores_GruposInvestigacion_GrupoInvestigacionId",
                        column: x => x.GrupoInvestigacionId,
                        principalTable: "GruposInvestigacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistorialCoordinadores_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LineasInvestigacion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FacultadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GrupoInvestigacionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineasInvestigacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LineasInvestigacion_Facultades_FacultadId",
                        column: x => x.FacultadId,
                        principalTable: "Facultades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LineasInvestigacion_GruposInvestigacion_GrupoInvestigacionId",
                        column: x => x.GrupoInvestigacionId,
                        principalTable: "GruposInvestigacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contratos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SolicitudId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaInicio = table.Column<DateOnly>(type: "date", nullable: false),
                    FechaFinal = table.Column<DateOnly>(type: "date", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contratos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contratos_Solicitudes_SolicitudId",
                        column: x => x.SolicitudId,
                        principalTable: "Solicitudes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Facultades",
                columns: new[] { "Id", "Deleted", "Nombre" },
                values: new object[] { new Guid("6bc517b3-d8b3-47b2-b81d-e30f5a615129"), false, "Admin Facultad" });

            migrationBuilder.InsertData(
                table: "GruposInvestigacion",
                columns: new[] { "Id", "Deleted", "Nombre" },
                values: new object[] { new Guid("8167563c-5ecb-4778-bab9-f603f35df52d"), false, "Admin Grupo de Investigacion" });

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "Id", "Deleted", "Name" },
                values: new object[] { new Guid("b542bf25-134c-47a2-a0df-84ed14d03c4a"), false, "Admin Tenant" });

            migrationBuilder.InsertData(
                table: "Escuelas",
                columns: new[] { "Id", "Deleted", "FacultadId", "Nombre" },
                values: new object[] { new Guid("ef858bce-40b4-4445-aa8a-87c6edf80e65"), false, new Guid("6bc517b3-d8b3-47b2-b81d-e30f5a615129"), "Admin Escuela" });

            migrationBuilder.InsertData(
                table: "LineasInvestigacion",
                columns: new[] { "Id", "Deleted", "FacultadId", "GrupoInvestigacionId", "Nombre" },
                values: new object[] { new Guid("8167563c-5ecb-4778-bac8-f603f35df55f"), false, new Guid("6bc517b3-d8b3-47b2-b81d-e30f5a615129"), new Guid("8167563c-5ecb-4778-bab9-f603f35df52d"), "Admin Linea de Investigacion" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AsesoriaEstado", "Codigo", "Deleted", "Email", "EscuelaId", "FirstName", "LastLoggedinDate", "LastName", "LineaInvestigacionId", "Notificaciones", "Password", "Preferencias", "Role", "Status", "Telefono", "TenantId" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), 0, "0123456789", false, "coordinador@email.com", new Guid("ef858bce-40b4-4445-aa8a-87c6edf80e65"), "Coordinador", null, "User", new Guid("8167563c-5ecb-4778-bac8-f603f35df55f"), (byte)1, "$2a$12$Blal/uiFIJdYsCLTMUik/egLbfg3XhbnxBC6Sb5IKz2ZYhiU/MzL2", "", 1, 0, "123456789", new Guid("b542bf25-134c-47a2-a0df-84ed14d03c4a") },
                    { new Guid("00000000-0000-0000-0000-000000000002"), 0, "0123456789", false, "asesor@email.com", new Guid("ef858bce-40b4-4445-aa8a-87c6edf80e65"), "Asesor", null, "User", new Guid("8167563c-5ecb-4778-bac8-f603f35df55f"), (byte)1, "$2a$12$Blal/uiFIJdYsCLTMUik/egLbfg3XhbnxBC6Sb5IKz2ZYhiU/MzL2", "", 2, 0, "123456789", new Guid("b542bf25-134c-47a2-a0df-84ed14d03c4a") },
                    { new Guid("00000000-0000-0000-0000-000000000003"), 0, "0123456789", false, "asesorado@email.com", new Guid("ef858bce-40b4-4445-aa8a-87c6edf80e65"), "Asesorado", null, "User", new Guid("8167563c-5ecb-4778-bac8-f603f35df55f"), (byte)1, "$2a$12$Blal/uiFIJdYsCLTMUik/egLbfg3XhbnxBC6Sb5IKz2ZYhiU/MzL2", "", 3, 0, "123456789", new Guid("b542bf25-134c-47a2-a0df-84ed14d03c4a") },
                    { new Guid("00000000-0000-0000-0000-000000000004"), 0, "0123456789", false, "user@email.com", new Guid("ef858bce-40b4-4445-aa8a-87c6edf80e65"), "User", null, "User", new Guid("8167563c-5ecb-4778-bac8-f603f35df55f"), (byte)1, "$2a$12$Blal/uiFIJdYsCLTMUik/egLbfg3XhbnxBC6Sb5IKz2ZYhiU/MzL2", "", 4, 0, "123456789", new Guid("b542bf25-134c-47a2-a0df-84ed14d03c4a") },
                    { new Guid("00000000-0000-0000-0000-000000000010"), 0, "0123456789", false, "admin@email.com", new Guid("ef858bce-40b4-4445-aa8a-87c6edf80e65"), "Admin", null, "User", new Guid("8167563c-5ecb-4778-bac8-f603f35df55f"), (byte)1, "$2a$12$Blal/uiFIJdYsCLTMUik/egLbfg3XhbnxBC6Sb5IKz2ZYhiU/MzL2", "", 0, 0, "123456789", new Guid("b542bf25-134c-47a2-a0df-84ed14d03c4a") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_EscuelaId",
                table: "Users",
                column: "EscuelaId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_LineaInvestigacionId",
                table: "Users",
                column: "LineaInvestigacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TenantId",
                table: "Users",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Calendarios_UserId",
                table: "Calendarios",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Citas_AsesoradoUserId",
                table: "Citas",
                column: "AsesoradoUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Citas_AsesorUserId",
                table: "Citas",
                column: "AsesorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Citas_EventoId",
                table: "Citas",
                column: "EventoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contratos_SolicitudId",
                table: "Contratos",
                column: "SolicitudId");

            migrationBuilder.CreateIndex(
                name: "IX_Escuelas_FacultadId",
                table: "Escuelas",
                column: "FacultadId");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialCoordinadores_GrupoInvestigacionId",
                table: "HistorialCoordinadores",
                column: "GrupoInvestigacionId");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialCoordinadores_UserId",
                table: "HistorialCoordinadores",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LineasInvestigacion_FacultadId",
                table: "LineasInvestigacion",
                column: "FacultadId");

            migrationBuilder.CreateIndex(
                name: "IX_LineasInvestigacion_GrupoInvestigacionId",
                table: "LineasInvestigacion",
                column: "GrupoInvestigacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitudes_AsesoradoUserId",
                table: "Solicitudes",
                column: "AsesoradoUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitudes_AsesorUserId",
                table: "Solicitudes",
                column: "AsesorUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Escuelas_EscuelaId",
                table: "Users",
                column: "EscuelaId",
                principalTable: "Escuelas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_LineasInvestigacion_LineaInvestigacionId",
                table: "Users",
                column: "LineaInvestigacionId",
                principalTable: "LineasInvestigacion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Tenants_TenantId",
                table: "Users",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Escuelas_EscuelaId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_LineasInvestigacion_LineaInvestigacionId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Tenants_TenantId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Calendarios");

            migrationBuilder.DropTable(
                name: "Citas");

            migrationBuilder.DropTable(
                name: "Contratos");

            migrationBuilder.DropTable(
                name: "Escuelas");

            migrationBuilder.DropTable(
                name: "HistorialCoordinadores");

            migrationBuilder.DropTable(
                name: "LineasInvestigacion");

            migrationBuilder.DropTable(
                name: "Tenants");

            migrationBuilder.DropTable(
                name: "Solicitudes");

            migrationBuilder.DropTable(
                name: "Facultades");

            migrationBuilder.DropTable(
                name: "GruposInvestigacion");

            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_EscuelaId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_LineaInvestigacionId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_TenantId",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"));

            migrationBuilder.DropColumn(
                name: "AsesoriaEstado",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EscuelaId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastLoggedinDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LineaInvestigacionId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Notificaciones",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Preferencias",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Telefono",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Users",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Users",
                newName: "GivenName");
        }
    }
}
