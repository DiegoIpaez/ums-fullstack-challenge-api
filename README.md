# UMS Fullstack Challenge API

API RESTful para gestión de usuarios, direcciones y estudios, con autenticación JWT y paginación.

## 🚀 Instalación y Ejecución

1. **Clonar el repositorio:**
   ```bash
   git clone https://github.com/DiegoIpaez/ums-fullstack-challenge-api
   cd ums-fullstack-challenge-api
   ```
2. **Configurar la base de datos:**
   - Edita `appsettings.json` y `appsettings.Development.json` con tu cadena de conexión en `DefaultConnection`.

3. **Ejecutar migraciones:**
   ```bash
   dotnet ef database update
   ```

4. **Ejecutar la API:**
   ```bash
   dotnet run --project UmsApi/UmsApi.csproj
   ```

5. **Swagger UI:**
   - Accede a la documentación interactiva en: `https://localhost:5098/swagger/index.html`

## 🔑 Autenticación

- Usa `/api/Auth/login` para obtener un token JWT.
- Agrega el header `Authorization: Bearer <token>` en los endpoints protegidos.

## 📚 Endpoints principales

### Auth
- `POST /api/Auth/login` — Login
- `POST /api/Auth/logout` — Logout
- `POST /api/Auth/register` — Registro

### Users
- `GET /api/Users` — Listado paginado (admin)
- `GET /api/Users/{id}` — Detalle
- `PATCH /api/Users/{id}` — Actualizar
- `DELETE /api/Users/{id}` — Eliminar

### Address
- `GET /api/Address` — Listado paginado
- `GET /api/Address/{id}` — Detalle
- `POST /api/Address` — Crear
- `PUT /api/Address/{id}` — Actualizar
- `DELETE /api/Address/{id}` — Eliminar

### Studies
- `GET /api/Studies` — Listado paginado
- `GET /api/Studies/{id}` — Detalle
- `POST /api/Studies` — Crear
- `PUT /api/Studies/{id}` — Actualizar
- `DELETE /api/Studies/{id}` — Eliminar

## ⚙️ Configuración

- Edita `appsettings.json` para JWT y cadena de conexión.
- Variables importantes:
  - `JWT:Key`
  - `JWT:Issuer`
  - `JWT:Audience`
  - `ConnectionStrings:DefaultConnection`

## 🛡️ Validaciones y Errores
- Los DTOs principales usan validaciones nativas de .NET (DataAnnotations).

## 🧩 Stack
- .NET 8
- Entity Framework Core
- SQL Server
- JWT Auth
- Swagger/OpenAPI

## 📄 Licencia
MIT
