# Sistema de Gestión de Usuarios - Autenticación y Seguridad

Este proyecto es una aplicación web robusta desarrollada con **ASP.NET Core 8 MVC**. Implementa un flujo completo de autenticación de usuarios, gestión de sesiones y medidas de seguridad contra ataques de fuerza bruta, siguiendo los lineamientos de diseño de **Figma**.

##  Tecnologías:

* **Backend:** .NET 8 (C#) con ASP.NET Core MVC.
* **Base de Datos:** SQL Server.
* **ORM:** Entity Framework Core.
* **Frontend:** Razor Views, Bootstrap 5, JavaScript (Vanilla).
* **Seguridad:** Cookie Authentication Middleware.

## Características de Seguridad:

El proyecto destaca por implementar reglas de negocio críticas para entornos de producción:

1. **Bloqueo de Cuenta:** Tras **5 intentos fallidos** de inicio de sesión, la cuenta se bloquea automáticamente por **1 minuto**. Esta lógica se gestiona de forma persistente consultando la base de datos.
2. **Gestión de Sesión (60s):**
   * **Alerta Preventiva:** A los 45 segundos de inactividad, se dispara un **Modal de Advertencia** permitiendo al usuario extender su sesión (Sliding Expiration).
   * **Expulsión Automática:** Al cumplirse el minuto, el sistema redirige automáticamente al usuario al Login e invalida la cookie de autenticación.
3. **Protección SQL Injection:** Uso de consultas parametrizadas a través de EF Core.
