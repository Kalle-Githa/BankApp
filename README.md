# Bank App API

Ett backend-API för en bank, byggt som en skoluppgift. Målet var att bygga
ett fungerande ASP.NET Web API med autentisering, auktorisering och
databasintegration. Administratörer och kunder har olika åtkomst och
funktionalitet.

## Stack

- C# / ASP.NET Core Web API
- Entity Framework Core + SQL Server
- JWT-autentisering
- Swagger för testning

## Funktioner

**Admin**
- Skapa kunder och bankkonton
- Registrera lån för kunder

**Kund**
- Se konton, saldon och transaktionshistorik
- Skapa ytterligare konton
- Föra över pengar mellan egna konton
- Föra över pengar till andra kunder

**Auth**
- Inloggning med JWT
- Rollbaserad auktorisering (Admin / Kund)

## Köra lokalt

1. Klona repot och öppna solution i Visual Studio
2. Konfigurera connection string till databasen
3. Kör EF Core-migrationer
4. Starta API:et
5. Testa endpoints i Swagger eller Postman
