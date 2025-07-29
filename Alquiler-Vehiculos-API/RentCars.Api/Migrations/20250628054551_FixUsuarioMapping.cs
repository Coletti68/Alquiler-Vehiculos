using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentCars.Api.Migrations
{
    /// <inheritdoc />
    public partial class FixUsuarioMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aceptaciones_Alquileres_AlquilerId",
                table: "Aceptaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Aceptaciones_Usuarios_ClienteId",
                table: "Aceptaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Aceptaciones_Usuarios_UsuarioId",
                table: "Aceptaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Alquileres_Empleados_EmpleadoId",
                table: "Alquileres");

            migrationBuilder.DropForeignKey(
                name: "FK_Alquileres_Usuarios_ClienteId",
                table: "Alquileres");

            migrationBuilder.DropForeignKey(
                name: "FK_Alquileres_Vehiculos_VehiculoId",
                table: "Alquileres");

            migrationBuilder.DropForeignKey(
                name: "FK_Contactos_Usuarios_UsuarioId",
                table: "Contactos");

            migrationBuilder.DropForeignKey(
                name: "FK_Multas_Alquileres_AlquilerId",
                table: "Multas");

            migrationBuilder.DropForeignKey(
                name: "FK_Pagos_Alquileres_AlquilerId",
                table: "Pagos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vehiculos",
                table: "Vehiculos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pagos",
                table: "Pagos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Multas",
                table: "Multas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Empleados",
                table: "Empleados");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contactos",
                table: "Contactos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Alquileres",
                table: "Alquileres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Aceptaciones",
                table: "Aceptaciones");

            migrationBuilder.DropIndex(
                name: "IX_Aceptaciones_ClienteId",
                table: "Aceptaciones");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Aceptaciones");

            migrationBuilder.RenameTable(
                name: "Vehiculos",
                newName: "Vehiculo");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "Usuario");

            migrationBuilder.RenameTable(
                name: "Pagos",
                newName: "Pago");

            migrationBuilder.RenameTable(
                name: "Multas",
                newName: "Multa");

            migrationBuilder.RenameTable(
                name: "Empleados",
                newName: "Empleado");

            migrationBuilder.RenameTable(
                name: "Contactos",
                newName: "Contacto");

            migrationBuilder.RenameTable(
                name: "Alquileres",
                newName: "Alquiler");

            migrationBuilder.RenameTable(
                name: "Aceptaciones",
                newName: "AceptacionTerminos");

            migrationBuilder.RenameColumn(
                name: "FechaVencimiento",
                table: "Usuario",
                newName: "FechaNacimiento");

            migrationBuilder.RenameIndex(
                name: "IX_Pagos_AlquilerId",
                table: "Pago",
                newName: "IX_Pago_AlquilerId");

            migrationBuilder.RenameIndex(
                name: "IX_Multas_AlquilerId",
                table: "Multa",
                newName: "IX_Multa_AlquilerId");

            migrationBuilder.RenameIndex(
                name: "IX_Contactos_UsuarioId",
                table: "Contacto",
                newName: "IX_Contacto_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Alquileres_VehiculoId",
                table: "Alquiler",
                newName: "IX_Alquiler_VehiculoId");

            migrationBuilder.RenameIndex(
                name: "IX_Alquileres_EmpleadoId",
                table: "Alquiler",
                newName: "IX_Alquiler_EmpleadoId");

            migrationBuilder.RenameIndex(
                name: "IX_Alquileres_ClienteId",
                table: "Alquiler",
                newName: "IX_Alquiler_ClienteId");

            migrationBuilder.RenameIndex(
                name: "IX_Aceptaciones_UsuarioId",
                table: "AceptacionTerminos",
                newName: "IX_AceptacionTerminos_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Aceptaciones_AlquilerId",
                table: "AceptacionTerminos",
                newName: "IX_AceptacionTerminos_AlquilerId");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "AceptacionTerminos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId1",
                table: "AceptacionTerminos",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vehiculo",
                table: "Vehiculo",
                column: "VehiculoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario",
                column: "UsuarioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pago",
                table: "Pago",
                column: "PagoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Multa",
                table: "Multa",
                column: "MultaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Empleado",
                table: "Empleado",
                column: "EmpleadoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contacto",
                table: "Contacto",
                column: "ContactoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Alquiler",
                table: "Alquiler",
                column: "AlquilerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AceptacionTerminos",
                table: "AceptacionTerminos",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AceptacionTerminos_UsuarioId1",
                table: "AceptacionTerminos",
                column: "UsuarioId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AceptacionTerminos_Alquiler_AlquilerId",
                table: "AceptacionTerminos",
                column: "AlquilerId",
                principalTable: "Alquiler",
                principalColumn: "AlquilerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AceptacionTerminos_Usuario_UsuarioId",
                table: "AceptacionTerminos",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AceptacionTerminos_Usuario_UsuarioId1",
                table: "AceptacionTerminos",
                column: "UsuarioId1",
                principalTable: "Usuario",
                principalColumn: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alquiler_Empleado_EmpleadoId",
                table: "Alquiler",
                column: "EmpleadoId",
                principalTable: "Empleado",
                principalColumn: "EmpleadoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Alquiler_Usuario_ClienteId",
                table: "Alquiler",
                column: "ClienteId",
                principalTable: "Usuario",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Alquiler_Vehiculo_VehiculoId",
                table: "Alquiler",
                column: "VehiculoId",
                principalTable: "Vehiculo",
                principalColumn: "VehiculoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contacto_Usuario_UsuarioId",
                table: "Contacto",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Multa_Alquiler_AlquilerId",
                table: "Multa",
                column: "AlquilerId",
                principalTable: "Alquiler",
                principalColumn: "AlquilerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pago_Alquiler_AlquilerId",
                table: "Pago",
                column: "AlquilerId",
                principalTable: "Alquiler",
                principalColumn: "AlquilerId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AceptacionTerminos_Alquiler_AlquilerId",
                table: "AceptacionTerminos");

            migrationBuilder.DropForeignKey(
                name: "FK_AceptacionTerminos_Usuario_UsuarioId",
                table: "AceptacionTerminos");

            migrationBuilder.DropForeignKey(
                name: "FK_AceptacionTerminos_Usuario_UsuarioId1",
                table: "AceptacionTerminos");

            migrationBuilder.DropForeignKey(
                name: "FK_Alquiler_Empleado_EmpleadoId",
                table: "Alquiler");

            migrationBuilder.DropForeignKey(
                name: "FK_Alquiler_Usuario_ClienteId",
                table: "Alquiler");

            migrationBuilder.DropForeignKey(
                name: "FK_Alquiler_Vehiculo_VehiculoId",
                table: "Alquiler");

            migrationBuilder.DropForeignKey(
                name: "FK_Contacto_Usuario_UsuarioId",
                table: "Contacto");

            migrationBuilder.DropForeignKey(
                name: "FK_Multa_Alquiler_AlquilerId",
                table: "Multa");

            migrationBuilder.DropForeignKey(
                name: "FK_Pago_Alquiler_AlquilerId",
                table: "Pago");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vehiculo",
                table: "Vehiculo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pago",
                table: "Pago");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Multa",
                table: "Multa");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Empleado",
                table: "Empleado");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contacto",
                table: "Contacto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Alquiler",
                table: "Alquiler");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AceptacionTerminos",
                table: "AceptacionTerminos");

            migrationBuilder.DropIndex(
                name: "IX_AceptacionTerminos_UsuarioId1",
                table: "AceptacionTerminos");

            migrationBuilder.DropColumn(
                name: "UsuarioId1",
                table: "AceptacionTerminos");

            migrationBuilder.RenameTable(
                name: "Vehiculo",
                newName: "Vehiculos");

            migrationBuilder.RenameTable(
                name: "Usuario",
                newName: "Usuarios");

            migrationBuilder.RenameTable(
                name: "Pago",
                newName: "Pagos");

            migrationBuilder.RenameTable(
                name: "Multa",
                newName: "Multas");

            migrationBuilder.RenameTable(
                name: "Empleado",
                newName: "Empleados");

            migrationBuilder.RenameTable(
                name: "Contacto",
                newName: "Contactos");

            migrationBuilder.RenameTable(
                name: "Alquiler",
                newName: "Alquileres");

            migrationBuilder.RenameTable(
                name: "AceptacionTerminos",
                newName: "Aceptaciones");

            migrationBuilder.RenameColumn(
                name: "FechaNacimiento",
                table: "Usuarios",
                newName: "FechaVencimiento");

            migrationBuilder.RenameIndex(
                name: "IX_Pago_AlquilerId",
                table: "Pagos",
                newName: "IX_Pagos_AlquilerId");

            migrationBuilder.RenameIndex(
                name: "IX_Multa_AlquilerId",
                table: "Multas",
                newName: "IX_Multas_AlquilerId");

            migrationBuilder.RenameIndex(
                name: "IX_Contacto_UsuarioId",
                table: "Contactos",
                newName: "IX_Contactos_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Alquiler_VehiculoId",
                table: "Alquileres",
                newName: "IX_Alquileres_VehiculoId");

            migrationBuilder.RenameIndex(
                name: "IX_Alquiler_EmpleadoId",
                table: "Alquileres",
                newName: "IX_Alquileres_EmpleadoId");

            migrationBuilder.RenameIndex(
                name: "IX_Alquiler_ClienteId",
                table: "Alquileres",
                newName: "IX_Alquileres_ClienteId");

            migrationBuilder.RenameIndex(
                name: "IX_AceptacionTerminos_UsuarioId",
                table: "Aceptaciones",
                newName: "IX_Aceptaciones_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_AceptacionTerminos_AlquilerId",
                table: "Aceptaciones",
                newName: "IX_Aceptaciones_AlquilerId");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Aceptaciones",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "Aceptaciones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vehiculos",
                table: "Vehiculos",
                column: "VehiculoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "UsuarioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pagos",
                table: "Pagos",
                column: "PagoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Multas",
                table: "Multas",
                column: "MultaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Empleados",
                table: "Empleados",
                column: "EmpleadoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contactos",
                table: "Contactos",
                column: "ContactoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Alquileres",
                table: "Alquileres",
                column: "AlquilerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Aceptaciones",
                table: "Aceptaciones",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Aceptaciones_ClienteId",
                table: "Aceptaciones",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Aceptaciones_Alquileres_AlquilerId",
                table: "Aceptaciones",
                column: "AlquilerId",
                principalTable: "Alquileres",
                principalColumn: "AlquilerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Aceptaciones_Usuarios_ClienteId",
                table: "Aceptaciones",
                column: "ClienteId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Aceptaciones_Usuarios_UsuarioId",
                table: "Aceptaciones",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alquileres_Empleados_EmpleadoId",
                table: "Alquileres",
                column: "EmpleadoId",
                principalTable: "Empleados",
                principalColumn: "EmpleadoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Alquileres_Usuarios_ClienteId",
                table: "Alquileres",
                column: "ClienteId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Alquileres_Vehiculos_VehiculoId",
                table: "Alquileres",
                column: "VehiculoId",
                principalTable: "Vehiculos",
                principalColumn: "VehiculoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contactos_Usuarios_UsuarioId",
                table: "Contactos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Multas_Alquileres_AlquilerId",
                table: "Multas",
                column: "AlquilerId",
                principalTable: "Alquileres",
                principalColumn: "AlquilerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pagos_Alquileres_AlquilerId",
                table: "Pagos",
                column: "AlquilerId",
                principalTable: "Alquileres",
                principalColumn: "AlquilerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
