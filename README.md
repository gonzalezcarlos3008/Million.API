
# Pasos para Configurar y Ejecutar el Proyecto

Este documento describe los pasos necesarios para restaurar, configurar y ejecutar el proyecto API de Real Estate, una herramienta para gestionar propiedades inmobiliarias, utilizando .NET 8.

---

## 1. Restaurar la Base de Datos

1. Ubica el archivo de respaldo de la base de datos proporcionado (`testdb.bak` o `testdb.sql`).
2. Utiliza un gestor de bases de datos como SQL Server Management Studio (SSMS) para restaurar el respaldo:
   - Abre SSMS y conéctate a tu servidor SQL Server.
   - Haz clic derecho en **Bases de datos** > **Restaurar base de datos**.
   - Selecciona el archivo de respaldo y sigue las instrucciones para completar la restauración.
3. Asegúrate de que la base de datos esté restaurada correctamente y que contenga las tablas necesarias, como `Properties`, `PropertyImages`, `PropertyTraces` y `Owners`, ya que estas son críticas para el correcto funcionamiento del proyecto.

---

## 2. Abrir la Solución en Visual Studio y Restaurar Paquetes NuGet

1. Abre la solución del proyecto (`Million.API.sln`) en Visual Studio.
2. Restaura los paquetes NuGet:
   - En Visual Studio, ve al menú **Herramientas** > **Administrador de paquetes NuGet** > **Consola del Administrador de Paquetes**.
   - Ejecuta el comando:
     ```bash
     dotnet restore
     ```
   - Esto descargará e instalará los paquetes necesarios.

---

## 3. Verificar la Versión de .NET

Este proyecto utiliza **.NET 8**. Asegúrate de que tienes instalada la versión adecuada:

- Puedes verificar tu versión de .NET instalada ejecutando:
  ```bash
  dotnet --version
  ```

Si no tienes .NET 8 instalado, descárgalo desde [Microsoft .NET Download](https://dotnet.microsoft.com/download).

---

## 4. Configurar la Cadena de Conexión

1. Localiza el archivo `appsettings.json` en el proyecto principal.
2. Actualiza la cadena de conexión dentro de la sección `ConnectionStrings` para apuntar a tu servidor SQL Server donde restauraste la base de datos:
   ```json
   {
       "ConnectionStrings": {
           "DefaultConnection": "Server=YOUR_SERVER;Database=YOUR_DATABASE;Trusted_Connection=True;"
       }
   }
   ```
   - Reemplaza `YOUR_SERVER` por el nombre de tu servidor SQL.
   - Reemplaza `YOUR_DATABASE` por el nombre de tu base de datos restaurada.

---

## 5. Correr el Proyecto

1. En Visual Studio, selecciona el proyecto principal como el proyecto de inicio.
2. Presiona `F5` o haz clic en **Iniciar depuración** para ejecutar el proyecto.
3. La API estará disponible en una URL similar a:
   - http://localhost:5000 (HTTP)
   - https://localhost:5001 (HTTPS)

---

## 6. Generar Token de Autenticación

1. Abre Swagger en tu navegador (normalmente disponible en http://localhost:5000/swagger o similar).
2. Busca el endpoint **AuthController > POST /auth/login**.
3. Ejecuta este endpoint. Se generará un token JWT como parte de la respuesta.

---

## 7. Autenticarse en Swagger con el Token Generado

1. En Swagger, haz clic en el botón **Authorize** en la parte superior derecha.
2. Introduce el token en el siguiente formato:
   ```plaintext
   Bearer YOUR_TOKEN
   ```
   - Reemplaza `YOUR_TOKEN` con el token JWT generado.
3. Haz clic en **Authorize** para autenticarte.

---

## 8. Consumir Endpoints del API

1. Una vez autenticado, selecciona cualquier endpoint en Swagger.
2. Proporciona los parámetros requeridos (si aplica) y ejecuta el endpoint.
3. Visualiza los resultados de las operaciones realizadas en la API.

---

## Notas Adicionales

- Asegúrate de que tu servidor SQL y la base de datos estén en ejecución antes de iniciar el proyecto.
