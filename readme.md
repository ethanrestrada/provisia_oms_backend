# OMS Lite — Sistema de Gestión de Pedidos y Envíos en Tiempo Real

> API backend en **ASP.NET Core** con **Clean Architecture**, mensajería asíncrona con **RabbitMQ**, caching con **Redis**, notificaciones en tiempo real con **SignalR** y resiliencia con **Polly**. Caso de estudio: una distribuidora de alimentos B2B/B2C.

![.NET](https://img.shields.io/badge/.NET-8%2F9-512BD4?logo=dotnet)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-Database-4169E1?logo=postgresql&logoColor=white)
![RabbitMQ](https://img.shields.io/badge/RabbitMQ-Messaging-FF6600?logo=rabbitmq&logoColor=white)
![Redis](https://img.shields.io/badge/Redis-Caching-DC382D?logo=redis&logoColor=white)
![SignalR](https://img.shields.io/badge/SignalR-Real--time-0078D4)
![License](https://img.shields.io/badge/license-MIT-green)

---

## 📋 Descripción

**OMS Lite** es un sistema de gestión de pedidos, facturación y envíos pensado para una distribuidora de alimentos de tamaño mediano (modelo híbrido B2B/B2C). El proyecto no busca solo integrar tecnologías por separado, sino resolver un problema de negocio real: procesar pedidos de forma confiable, mantener el sistema responsivo bajo carga, dar visibilidad inmediata al cliente sobre el estado de su pedido, y sostener un código mantenible a largo plazo.

Caso de negocio usado como ejemplo: una distribuidora con catálogo en **Lácteos y Quesos, Charcutería y Carnes Frías, Aceites y Vinagres, Harinas y Granos Premium y Conservas y Gourmet**, con clientes finales y clientes corporativos (restaurantes, hoteles, minimarkets) que operan con línea de crédito.

## ✨ Funcionalidades principales

- Catálogo de productos con caching (Cache-Aside en Redis, TTL 15 min) e invalidación automática.
- Gestión de pedidos con validación de stock en tiempo real.
- Cotizaciones B2B (estado `Draft`) con precios congelados por 7 días y descarga en PDF.
- Validación automática de línea de crédito (`CreditLimit`) para clientes corporativos.
- Facturación asíncrona vía RabbitMQ, con patrón **Outbox** para garantizar la publicación de eventos.
- Asignación de transportista y seguimiento de envío mediante checkpoints (sin GPS en tiempo real).
- Notificaciones de estado de pedido en vivo (SignalR) sin necesidad de recargar la página.
- Resiliencia ante fallos externos con **Circuit Breaker** y reintentos exponenciales (Polly).
- Autenticación con JWT (access + refresh token) y contraseñas protegidas con BCrypt.
- Documentación interactiva de la API con Swagger/OpenAPI.

## 🏗️ Arquitectura

Clean Architecture, con las dependencias apuntando siempre hacia el dominio:

```
src/
├── Domain/          # Entidades y reglas de negocio puras (sin dependencias externas)
├── Application/     # Casos de uso, interfaces de repositorios y servicios (ports)
├── Infrastructure/  # EF Core + PostgreSQL, RabbitMQ, Redis, SignalR Hubs (adapters)
└── Api/             # Controladores REST, middlewares, configuración de Swagger
```

## 🧰 Stack tecnológico

| Área | Tecnología |
|---|---|
| API | ASP.NET Core (.NET 8/9) |
| Base de datos | PostgreSQL + Entity Framework Core |
| Mensajería | RabbitMQ (Topic Exchange, patrón Outbox, DLX) |
| Caching | Redis (Cache-Aside, rate limiting distribuido) |
| Tiempo real | SignalR (WebSockets) |
| Resiliencia | Polly (Circuit Breaker, Retry con jitter) |
| Autenticación | JWT + BCrypt |
| Documentación | Swagger / OpenAPI |
| Contenedores | Docker + Docker Compose |
| Testing | xUnit, TestContainers |

## 🗄️ Modelo de datos

13 tablas organizadas en 6 dominios: Usuarios y Permisos, Catálogo e Inventario, Gestión de Pedidos, Pagos y Facturación, Despacho y Logística, y Sistema/Auditoría. Detalle completo en [`/docs`](./docs).

## 🔄 Flujo principal

```
Cliente → POST /api/orders → Orders (Pending) + OutboxMessages
                                    │
                                    ▼
                        RabbitMQ (evento OrderCreated)
                                    │
                    ┌───────────────┴───────────────┐
                    ▼                                ▼
             InvoiceWorker                    (tras InvoiceGenerated)
        (mock de pago + Polly)                  ShipmentWorker
                    │                                │
                    ▼                                ▼
              Invoices (Paid)                 Shipments + Tracking
                    │                                │
                    └──────────► SignalR ◄───────────┘
                        (notificación en tiempo real al cliente)
```

## 🚀 Cómo levantar el proyecto

### Requisitos previos
- .NET SDK 8 o 9
- Docker y Docker Compose

### Pasos

```bash
# Clonar el repositorio
git clone https://github.com/<tu-usuario>/oms-lite.git
cd oms-lite

# Levantar PostgreSQL, RabbitMQ y Redis
docker compose up -d

# Aplicar migraciones
dotnet ef database update --project src/Infrastructure --startup-project src/Api

# Correr la API
dotnet run --project src/Api
```

La API queda disponible en `https://localhost:5001`, con Swagger UI en `/swagger`.

## 🧪 Testing

```bash
dotnet test
```

Incluye pruebas unitarias de los casos de uso (con mocks de repositorios) y pruebas de integración con TestContainers levantando PostgreSQL y RabbitMQ reales.

## 🗺️ Roadmap de desarrollo

El desarrollo sigue un orden incremental por fases (fundamentos → autenticación → catálogo/pedidos → caching → mensajería → resiliencia → tiempo real → reglas de negocio → testing → contenedores/CI-CD → observabilidad). Detalle completo del checklist en [`/docs`](./docs).

## 📄 Licencia

Este proyecto se distribuye bajo la licencia MIT. Ver [`LICENSE`](./LICENSE) para más detalles.
