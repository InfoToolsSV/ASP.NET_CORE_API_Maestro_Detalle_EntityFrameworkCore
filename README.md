
# Project Title

Este proyecto es un sistema de gestión de pedidos desarrollado con ASP.NET Core, Entity Framework Core y SQL Server. Permite a los usuarios realizar operaciones CRUD en pedidos y detalles de pedidos a través de una API RESTful.


## Tech Stack

- ASP.NET Core
- Entity Framework Core
- SQL Server
- Visual Studio Code
- Postman
## Archivos del Proyecto

- /Controllers: Contiene los controladores de la API para gestionar pedidos y detalles de pedidos.
- /Models: Contiene las clases de modelo para representar pedidos y detalles de pedidos.
- /Data: Contiene el contexto de la base de datos y las migraciones de Entity Framework.

## Installation

1. Clona este repositorio en tu máquina local.
2. Abre el proyecto en Visual Studio Code.
3. Configura la cadena de conexión a tu base de datos SQL Server en el archivo `appsettings.json`.
4. Abre una terminal en Visual Studio Code y ejecuta el siguiente comando para aplicar las migraciones y crear la base de datos:
    ```
    dotnet ef database update
    ```
5. Ejecuta la aplicación utilizando el siguiente comando:
    ```
    dotnet run
    ```
6. Abre Postman o cualquier herramienta similar para probar la API utilizando las rutas proporcionadas en los controladores.
