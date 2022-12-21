# CatalogService

## Functionalities:

-  [x] Integrating to 2 rate providers : [CurrencyLayer]( https://currencylayer.com/ ) and [ApiLayer-Currency-data]( https://apilayer.com/marketplace/currency_data-api)

- [x] Retaining information abount currency exchange trades carried out by its clients in database
- [x] When an exchange rate is used, it cached and if not older than 30 minutes it used again in other exchanges (30 minutes is configurable in appSettings.json)
- [x] Limiting each client to 10 currency exchange trades per hour ( number 10, is configurable in appSettings.json)

---

## Technics and Technologies

- [x] C# (.NET 6 Web Api)
- [x] Docker/ Docker Compose
- [x] Entity Framework Core
- [x] MsSQL Server 2019
- [x] RESTful APIs
- [x] xUnit Tests
- [x] Fluent Assertion
- [x] Distributed Cache By Redis
- [x] Logging
- [x] Fluent Validation
- [x] MediatR

---

## How To Run

- First go to the FH.CatalogService folder and run this command in terminal : `docker compose up -d`
- When docker compose has finished building.
- Now in your browser try http://localhost/swagger and see the APIs list and test them.
