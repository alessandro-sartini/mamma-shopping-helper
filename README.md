# Mamma Shopping Helper

**Mamma Shopping Helper** è un'applicazione full-stack di gestione della spesa composta da un backend API REST in ASP.NET Core e un frontend web in Angular. Permette di organizzare e gestire liste della spesa, prodotti, categorie e ricette in modo semplice ed efficiente.

## Prerequisiti

Prima di iniziare, assicurati di avere installato:

- Visual Studio 2022
- .NET 8.0 SDK
- SQL Server Express 
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


### Liste della Spesa

- `GET /api/ListeDellaSpesa` - Recupera tutte le liste della spesa con filtri opzionali
  - Query params: `orderBy` (default: "DataCreazione"), `ascending` (default: false), `creataDa`, `conclusa`
- `GET /api/ListeDellaSpesa/{id}` - Recupera dettaglio lista specifica per ID
- `POST /api/ListeDellaSpesa` - Crea una nuova lista della spesa
- `PUT /api/ListeDellaSpesa/{id}` - Aggiorna una lista esistente
- `DELETE /api/ListeDellaSpesa/{id}` - Elimina una lista della spesa
- `PUT /api/ListeDellaSpesa/{id}/conclusa` - Alterna lo stato "conclusa" di una lista

### Prodotti

- `GET /api/Prodotti` - Recupera tutti i prodotti
- `GET /api/Prodotti/{id}` - Recupera dettaglio prodotto specifico per ID
- `GET /api/Prodotti/lista/{listId}` - Recupera tutti i prodotti di una lista specifica
- `GET /api/Prodotti/utente/{userName}` - Recupera tutti i prodotti aggiunti da un utente specifico
- `POST /api/Prodotti` - Crea un nuovo prodotto
- `PUT /api/Prodotti/{id}` - Aggiorna un prodotto esistente
- `DELETE /api/Prodotti/{id}` - Elimina un prodotto
- `PUT /api/Prodotti/{id}/acquistato` - Alterna lo stato "acquistato" di un prodotto
- `PUT /api/Prodotti/{id}/incrementa-quantita` - Incrementa la quantità di un prodotto
  - Query param: `quantita` (default: 1, max: 100)
- `PUT /api/Prodotti/{id}/decrementa-quantita` - Decrementa la quantità di un prodotto
  - Query param: `quantita` (default: 1, max: 100)



### Frontend: Angular Application

Applicazione web single-page costruita con Angular 19.

#### Funzionalità

- **Dashboard**: Panoramica generale con statistiche e liste attive
- **Gestione Prodotti**: CRUD completo per prodotti con filtri per categoria
- **Liste della Spesa**: Creazione e gestione liste.



### Backend

- Microsoft.EntityFrameworkCore.SqlServer (8.0.x)
- Microsoft.EntityFrameworkCore.Tools (8.0.x)

### Frontend

- Angular 19.x
- TypeScript 5.6
- RxJS 7.8

## Testing API

Durante lo sviluppo puoi testare le API usando:

1. **Swagger UI** - Naviga a `https://localhost:[porta]/swagger` quando l'API è in esecuzione
2. **Postman** - Importa gli endpoint dalla documentazione Swagger


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



## Dati di Test

Per facilitare i test dell'applicazione, il database viene automaticamente popolato con dati

### Cosa troverai

Nel database di test sono presenti:

- **5 liste della spesa** con scenari realistici:
  - Lista settimanale della famiglia
  - Preparazione per una cena speciale
  - Colazione e merenda dei bambini
  - Una lista completata (esempio di stato finale)
  - Ingredienti per pizza fatta in casa

- **42 prodotti** distribuiti nelle liste:
  - Prodotti in vari stati (acquistati e non)
  - Diversi utenti che aggiungono elementi
  - Date di aggiunta variabili per simulare un utilizzo reale

### Se vuoi ricominciare da zero

Per resettare il database e ricaricarlo con i dati di test, esegui semplicemente questo script SQL:

```sql
DELETE FROM Prodotti;
DELETE FROM ListeDellaSpesa;
DBCC CHECKIDENT ('Prodotti', RESEED, 0);
DBCC CHECKIDENT ('ListeDellaSpesa', RESEED, 0);
```

Poi riavvia l'applicazione e il seeding automatico ricaricherà i dati di test.


## Link Utili

- Repository GitHub: [https://github.com/alessandro-sartini/mamma-shopping-helper](https://github.com/alessandro-sartini/mamma-shopping-helper)

## Autore

Alessandro Sartini - [@alessandro-sartini](https://github.com/alessandro-sartini)
