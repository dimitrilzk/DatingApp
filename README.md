# Applicazione Web Full-Stack con .NET 8, Angular 18 ed Entity Framework Core

Progetto dimostrativo che implementa un'applicazione web moderna con funzionalità avanzate, utilizzando un stack tecnologico aggiornato e best practice di sviluppo.

## 🛠️ Tecnologie Principali

**Backend (.NET 8)**
- ASP.NET Core Web API per servizi RESTful
- Entity Framework Core per ORM e gestione database
- JWT Authentication per sicurezza endpoint
- SignalR per notifiche real-time e presenza utente
- AutoMapper per object mapping
- Integrazione cloud per upload file (Cloudinary)

**Frontend (Angular 18)**
- Architettura modulare con standalone components
- Gestione stato reattiva
- Drag & Drop per upload immagini
- Sistema di notifiche integrate
- Paginazione e filtri avanzati
- Interfaccia messaggistica privata

## 🌟 Funzionalità Chiave
- **Autenticazione JWT** con refresh token
- **CRUD avanzato** con sorting/filtering/paging
- **Upload immagini** drag & drop su piattaforma cloud
- **Sistema di messaggistica** in tempo reale via SignalR
- **Gestione errori** centralizzata (API e SPA)
- **Notifiche push** e indicatori di presenza utente
- **Integrazione continua** tra frontend e backend

## 🚀 Deployment
- Configurazione CORS per sicurezza
- Hosting su Kestrel con reverse proxy (Nginx/Apache)
- Possibilità di containerizzazione (Docker)
- Supporto a database relazionali (SQL Server/PostgreSQL)

## 📦 Come Iniziare
1. Clona il repository
2. Configura connection string in `appsettings.json`
3. Esegui migration EF Core: dotnet ef database update
4. Avvia backend: dotnet run
5. Installa dipendenze Angular: npm install
6. Lancia frontend: ng serve

## 🔧 Strumenti di Sviluppo
- Visual Studio Code con estensioni .NET/Angular
- Postman per test API
- Swagger per documentazione endpoint
- Git per version control

## 📚 Risorse Utili
- [Documentazione .NET 8](https://learn.microsoft.com/dotnet/core/)
- [Guida Angular 18](https://angular.dev/)
- [Esempi EF Core](https://learn.microsoft.com/ef/core)


