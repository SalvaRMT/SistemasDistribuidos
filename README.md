# Sistemas Distribuidos

Repositorio académico que reúne prácticas y proyectos desarrollados para estudiar fundamentos y tecnologías utilizadas en sistemas distribuidos.

Los ejercicios abarcan comunicación por sockets, REST, SOAP, gRPC, bases de datos, contenedores, autenticación e interoperabilidad entre servicios implementados en distintos lenguajes.

## Objetivo

Comprender mediante implementaciones prácticas cómo se comunican y coordinan procesos y servicios dentro de un sistema distribuido.

Durante el curso se trabajaron:

- TCP;
- peer-to-peer;
- REST;
- SOAP;
- gRPC y Protocol Buffers;
- Docker y Docker Compose;
- SQL y NoSQL;
- autenticación y autorización;
- separación por capas;
- interoperabilidad entre lenguajes.

## Proyectos principales

### Introducción a Docker
Práctica inicial para trabajar con Dockerfile, imágenes, contenedores y entornos de desarrollo.

### Hello World — Go
Microservicio HTTP básico en Go.

```http
GET /hello-world
```

Responde JSON y sirve como introducción a `net/http` y contenedorización.

### Chat P2P — Go
Comunicación directa mediante sockets TCP.

- modo escucha;
- conexión entre peers;
- envío y recepción de mensajes;
- goroutines;
- concurrencia básica.

### Transferencia de archivos P2P — Go
Envío de archivos mediante TCP, trabajando lectura por bloques y escritura en disco.

> Es una práctica académica de transferencia de datos, no un protocolo seguro de archivos.

### PokemonApi — SOAP + MySQL
Servicio ASP.NET Core con SoapCore y Entity Framework Core.

```text
Service
   │
Repository
   │
Entity Framework Core
   │
MySQL
```

Incluye servicios de Pokémon, hobbies y libros.

### PokedexApi — REST + SOAP + OAuth/JWT
API REST que funciona como fachada frente a `PokemonApi`.

```text
Cliente
   │ REST
   ▼
PokedexApi
   │ SOAP
   ▼
PokemonApi
   │
   ▼
Entity Framework Core
   │
   ▼
MySQL
```

Incluye:
- Controllers / Services / Repositories;
- DTOs y mappers;
- validaciones;
- códigos HTTP;
- JWT Bearer;
- scopes `read` y `write`;
- ORY Hydra;
- PostgreSQL para Hydra;
- Docker Compose.

### TrainerApi — C# / gRPC / MongoDB
Servicio gRPC con ASP.NET Core, Protocol Buffers y MongoDB.

Incluye llamadas unary, streaming de solicitudes, repository pattern y mappers.

### Trainer Service — Python / gRPC / MongoDB
Implementación en Python de un servicio gRPC para entrenadores.

Operaciones:
- obtener;
- buscar por nombre;
- listar;
- crear;
- actualizar;
- eliminar.

Tecnologías:
- Python;
- gRPC;
- Protobuf;
- PyMongo;
- MongoDB;
- Docker.

### Pokedex API — Go
Prototipo para practicar Gorilla Mux, routing, CORS, JSON y manejo de errores.

El punto de entrada y el servicio de consulta permanecen incompletos, por lo que se conserva como evidencia de la etapa de aprendizaje y exploración con Go.

## Tecnologías utilizadas

### Lenguajes
- C#
- Go
- Python
- PHP

### Frameworks y herramientas
- ASP.NET Core
- Entity Framework Core
- SoapCore
- gRPC
- Protocol Buffers
- Gorilla Mux
- PyMongo
- Docker
- Docker Compose
- ORY Hydra

### Bases de datos
- MySQL
- MongoDB
- PostgreSQL

### Comunicación
- HTTP / REST
- SOAP
- gRPC
- TCP sockets
- Peer-to-peer

## Estructura conceptual

```text
SistemasDistribuidos/
├── Crear y ejecutar un contenedor con Docker/
├── hello word/
├── p2p_chat2/
├── p2p_file_share/
├── pokedex_api (go)/
├── PokemonApi/
├── PokedexApi/
├── TrainerApi/
└── trainer-service-python/
```

Cada carpeta corresponde a una práctica o servicio independiente.

## Ejecución

### Go

```bash
cd <proyecto-go>
go run .
```

### .NET

```bash
cd <proyecto-dotnet>
dotnet restore
dotnet run
```

### Python

```bash
cd trainer-service-python
pip install -r requirements.txt
python server.py
```

Los proyectos que utilizan bases de datos, autenticación u otros servicios requieren levantar también sus dependencias, en algunos casos mediante Docker Compose.

## Resultado del curso

El repositorio muestra la evolución desde comunicación directa entre procesos hasta arquitecturas compuestas por múltiples servicios.

Los proyectos permiten demostrar:
- comunicación entre procesos;
- integración de protocolos;
- persistencia SQL y NoSQL;
- contenedorización;
- autenticación;
- diseño por capas;
- interoperabilidad entre lenguajes.

## Autor

**Alan Salvador Rojas Piña**  
Estudiante de Ingeniería de Software — Universidad Autónoma de Querétaro

GitHub: https://github.com/SalvaRMT
