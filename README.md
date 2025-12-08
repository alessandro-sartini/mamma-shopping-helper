# Mamma Shopping Helper

**Mamma Shopping Helper** è un'applicazione full-stack di gestione della spesa composta da un backend API REST in ASP.NET Core e un frontend web in Angular. Permette di organizzare e gestire liste della spesa, prodotti, categorie e ricette in modo semplice ed efficiente.

## Prerequisiti

Prima di iniziare, assicurati di avere installato:

- Visual Studio 2022 (Community o superiore)
- .NET 8.0 SDK
- SQL Server Express (o versione completa)
- Node.js (v18 o superiore) e npm
- Angular CLI (`npm install -g @angular/cli`)
- Git

## Installazione

### 1. Clone del Repository

```
git clone https://github.com/alessandro-sartini/mamma-shopping-helper.git
cd mamma-shopping-helper
```

### 2. Configurazione Database

Modifica il connection string in `mamma-shopping-helper/appsettings.json` se usi un'istanza di SQL Server diversa da Express:

```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=.\\sqlexpress;Initial Catalog=MammaShoppingDb;Integrated Security=True;TrustServerCertificate=True;"
}
```

### 3. Creazione Database

Apri **Package Manager Console** in Visual Studio (seleziona il progetto `mamma-shopping-helper` come progetto predefinito) ed esegui:

```
Update-Database
```

Questo comando applicherà automaticamente le migration e creerà il database con le tabelle necessarie.

### 4. Installazione Dipendenze Frontend

Naviga nella cartella del frontend e installa le dipendenze:

```
cd frontend
npm install
```

## Avvio dell'Applicazione

### Backend (API)

1. Apri la solution `mamma-shopping-helper.sln` in Visual Studio
2. Imposta il progetto `mamma-shopping-helper` come progetto di avvio
3. Premi F5 o clicca su "Start" per avviare l'API
4. L'API sarà disponibile su `https://localhost:[porta]`

### Frontend (Angular)

Da terminale, nella cartella `frontend`:

```
ng serve
```

L'applicazione sarà disponibile su `http://localhost:4200`

## Architettura

### Backend: mamma-shopping-helper (Web API)

Web API REST costruita con ASP.NET Core 8.0.

#### Endpoint Principali

**Prodotti**
- `GET /api/Products` - Lista tutti i prodotti
- `GET /api/Products/{id}` - Dettaglio prodotto specifico
- `POST /api/Products` - Crea nuovo prodotto
- `PUT /api/Products/{id}` - Aggiorna prodotto
- `DELETE /api/Products/{id}` - Elimina prodotto

**Categorie**
- `GET /api/Categories` - Lista tutte le categorie
- `POST /api/Categories` - Crea nuova categoria
- `PUT /api/Categories/{id}` - Aggiorna categoria
- `DELETE /api/Categories/{id}` - Elimina categoria

**Liste della Spesa**
- `GET /api/ShoppingLists` - Lista tutte le liste
- `GET /api/ShoppingLists/{id}` - Dettaglio lista specifica
- `POST /api/ShoppingLists` - Crea nuova lista
- `PUT /api/ShoppingLists/{id}` - Aggiorna lista
- `DELETE /api/ShoppingLists/{id}` - Elimina lista

**Ricette**
- `GET /api/Recipes` - Lista tutte le ricette
- `GET /api/Recipes/{id}` - Dettaglio ricetta specifica
- `POST /api/Recipes` - Crea nuova ricetta
- `PUT /api/Recipes/{id}` - Aggiorna ricetta
- `DELETE /api/Recipes/{id}` - Elimina ricetta

#### Modello Dati

- **Product**: Id, Name, CategoryId, Unit, Notes, CreatedAt, UpdatedAt
- **Category**: Id, Name, Description, Products
- **ShoppingList**: Id, Name, CreatedAt, Items, IsCompleted
- **ShoppingListItem**: Id, ProductId, Quantity, IsPurchased
- **Recipe**: Id, Name, Description, Instructions, Ingredients

### Frontend: Angular Application

Applicazione web single-page costruita con Angular 19.

#### Funzionalità

- **Dashboard**: Panoramica generale con statistiche e liste attive
- **Gestione Prodotti**: CRUD completo per prodotti con filtri per categoria
- **Gestione Categorie**: Creazione e organizzazione categorie
- **Liste della Spesa**: Creazione e gestione liste con tracking degli acquisti
- **Ricettario**: Gestione ricette con ingredienti e istruzioni

#### Struttura Componenti

```
src/
├── app/
│   ├── components/        # Componenti UI
│   ├── services/          # Servizi per chiamate API
│   ├── models/            # Interfacce TypeScript
│   ├── guards/            # Route guards
│   └── interceptors/      # HTTP interceptors
```

## Dipendenze

### Backend

- Microsoft.EntityFrameworkCore.SqlServer (8.0.x)
- Microsoft.EntityFrameworkCore.Tools (8.0.x)
- Microsoft.AspNetCore.OpenApi
- Swashbuckle.AspNetCore

### Frontend

- Angular 19.x
- TypeScript 5.6
- RxJS 7.8
- Bootstrap (opzionale, se presente)

## Testing API

Durante lo sviluppo puoi testare le API usando:

1. **Swagger UI** - Naviga a `https://localhost:[porta]/swagger` quando l'API è in esecuzione
2. **Postman** - Importa gli endpoint dalla documentazione Swagger
3. **Visual Studio HTTP Client** - Usa il file `mamma-shopping-helper.http` incluso nel progetto

## Configurazione CORS

Il backend è configurato per accettare richieste dal frontend Angular su `http://localhost:4200`. Per modificare questa configurazione, edita il file `Program.cs`:

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy => policy.WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});
```

## Build per Produzione

### Backend

```
dotnet publish -c Release -o ./publish
```

### Frontend

```
cd frontend
ng build --configuration production
```

I file compilati saranno disponibili in `frontend/dist/`

## Link Utili

- Repository GitHub: [https://github.com/alessandro-sartini/mamma-shopping-helper](https://github.com/alessandro-sartini/mamma-shopping-helper)
- Documentazione Angular: [https://angular.dev](https://angular.dev)
- Documentazione ASP.NET Core: [https://learn.microsoft.com/aspnet/core](https://learn.microsoft.com/aspnet/core)

## Autore

Alessandro Sartini - [@alessandro-sartini](https://github.com/alessandro-sartini)
