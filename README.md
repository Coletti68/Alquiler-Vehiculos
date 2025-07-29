RentCars - Sistema de Gestion de Alquiler de Vehiculos.

Esta aplicacion permite gestionar usuarios, alquileres de vehiculos y multas. Se divide en cuatro capas principales: API REST, interfaz de administracion(MVC), una interfaz cliente(SPA)  ademas de la aplicacion mobile tambien enfocada para los clientes.

Pasos para poder probar la APP:

1. Configurar base de datos: (Archivo appsettings.json): "ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=AlquilerBDD;Trusted_Connection=True;"
}

2. Ejecutar los Proyectos:
API(Rentcars.Api): al ejecutar se abrira el swagger con todos nuestros endpoints definidos (si solo abre esta direccion https://localhost:5001, agregar /swagger)
MVCAlquilerAutos: Al ejecutar nos lleva al panel de administrador en https://localhost:5002.
SPAReact: RentCars.SPA: este programa se abre y se ejecuta en visual studio code.Antes de ejecutar este programa se deben instalar las dependencias con el comando npm install, luego se ejecuta con el comando npm run dev.

3. Probar Funcionalidades:
   Probar en swagger cada endpoints ingresando sus respectivos campos y respetando sus restricciones.
- POST /api/Auth/login → Login de usuario
- GET /api/Usuario → Listar usuarios
- PUT /api/Usuario/{id} → Editar usuario (incluye campo Activo)
- GET /api/Usuario/detalles/{id} → Obtener usuario con alquileres y multas
- POST /api/Multas → Registrar multa.
  Desde el MVC se puede:
- Ver lista de usuarios
- Editar datos y cambiar el estado Activo
- Ver historial de alquileres y multas
Desde el SPA: 
- Iniciar sesión con un usuario activo
- Alquilar vehiculos
- Ver sus datos, alquileres y multas
- Si el usuario está inactivo, el login será rechazado.