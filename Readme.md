# RabbitMQ Practice – .NET 9, Blazor, RabbitMQ & PostgreSQL

A practical, didactic project demonstrating asynchronous messaging and microservices architecture with .NET 9 WebAPI, Blazor WebAssembly, RabbitMQ, and PostgreSQL — all containerized via Docker Compose.

---

## 🖥️ Stack

- **Backend:** .NET 9 WebAPI
- **Frontend:** Blazor WebAssembly (.NET 9)
- **Messaging:** RabbitMQ
- **Database:** PostgreSQL
- **Infra:** Docker / Docker Compose
- **ORM:** Entity Framework Core

---

## ⚡ Features

- Async CRUD for Users and Enterprises
- API publishes events to RabbitMQ (producer/consumer)
- Data persistence in PostgreSQL
- Automatic database migration on startup
- Ready-to-run infra for dev, test, and learning

---

## 🚀 Getting Started

### **Prerequisites**

- Docker and Docker Compose installed

### **Run the project**

```bash
git clone https://github.com/your-username/rabbitmq-practice.git
cd rabbitmq-practice
docker compose up --build
```

- The first build may take a while as Docker pulls all required images.

### **Access the services**

- **API (Swagger):** [http://localhost:8080/swagger](http://localhost:8080/swagger)

- **RabbitMQ Dashboard:** [http://localhost:15672](http://localhost:15672) (user: `guest`, password: `guest`)

- **PostgreSQL:** `localhost:5432`

  - DB: `RabbitMQ`
  - User: `postgres`
  - Password: `e3b0c4...` (see `docker-compose.yml`)

- **Database Files:** Persisted locally at `./postgres-rabbitmq` (auto-created by Docker)

---

## 📝 How It Works

- The **Blazor frontend** triggers CRUD operations by sending HTTP requests to the API.
- The **API** publishes event messages (like create, update, delete) to RabbitMQ queues asynchronously ("fire-and-forget").
- **Consumer services** subscribe to queues, receive messages, and handle business logic (including writing data to PostgreSQL).
- The project is fully asynchronous and decoupled, following SOLID, DRY, KISS, and YAGNI principles.

---

## 🛠️ Initial Database Migration

> **Automatic Migration:**
> On API startup, all pending Entity Framework Core migrations are applied automatically (see `Program.cs`).
> If the tables do not exist, they are created.
> If everything is up to date, nothing happens.

```csharp
// Startup code in Program.cs
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<RabbitMQContext>();
    db.Database.Migrate();
}
```

> **Manual Migration:**
> If you need to generate or apply migrations manually:

```bash
# Create a new migration (only needed once, if you change models)
dotnet ef migrations add InitialCreate --project RabbitMQ.API.Infra --startup-project RabbitMQ.API

# Apply migrations to the DB
dotnet ef database update --project RabbitMQ.API.Infra --startup-project RabbitMQ.API
```

> **Tip:**
> You can also connect to the running Postgres instance using [Beekeeper Studio](https://www.beekeeperstudio.io/), [DBeaver](https://dbeaver.io/), or `psql` CLI:

- Host: `localhost`
- Port: `5432`
- User: `postgres`
- DB: `RabbitMQ`
- Password: see above

---

## 🔗 Useful Endpoints & Dashboards

- **RabbitMQ Management UI:**
  [http://localhost:15672](http://localhost:15672)
  User: `guest` | Password: `guest`

  > Here you can inspect queues, see message traffic, create/delete queues, and monitor consumers.

- **API Endpoints:**
  The API provides CRUD for both Users and Enterprises, e.g.:

  - `GET http://localhost:8080/api/user`

  - `POST http://localhost:8080/api/user`

  - `GET http://localhost:8080/api/enterprise`

  - `POST http://localhost:8080/api/enterprise`

  > Use the Swagger UI to explore, test and document all endpoints:
  > [http://localhost:8080/swagger](http://localhost:8080/swagger)

- **PostgreSQL Data:**
  Connect using your favorite SQL client (see above) to browse, query, and edit tables.

---

## 📁 Project Structure

```
├── RabbitMQ.API                 # .NET 9 WebAPI (Backend)
├── RabbitMQ.MessageSenderBus    # RabbitMQ Producer Service
├── RabbitMQ.MessageReceiverBus  # RabbitMQ Consumer Service
├── RabbitMQ.API.Infra           # Infrastructure and Migrations (EF Core)
├── front/                       # Blazor WebAssembly (Frontend)
├── docker-compose.yml
└── postgres-rabbitmq/           # Persistent Postgres data
```

---

## 🧑‍💻 Development & Contribution

- All infrastructure runs in Docker — zero setup needed on host!
- Open for issues, suggestions, and pull
