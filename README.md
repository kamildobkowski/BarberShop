# BARBERSHOP
### DESCRIPTION
Barbershop is an API built as a management tool for Barbershop owners and appointment booking tool for clients
### Technologies in use
- C#
- .NET Core
- ASP.NET Core Web  Api
- Entity Framework Core 7
- Microsoft SQL Server (locally on docker)
- Automapper, FluentValidation
- Swagger

### Design patterns in use
- Clean Architecture
- CQRS
- Repository Pattern

## ENDPOINTS
### Account
- [**POST**] Register Customer (*/api/register*)
- [**POST**] Register Shop Admin (*/api/register/shopadmin*)
- [**POST**] Register Admin (*/api/register/admin*)
- [**POST**] Login (*/api/login*)
- [**PUT**] Link ShopAdmin to a shop
### Review
- [**GET**] Get all reviews (*/api/shops/{shopId}/reviews*)
- [**GET**] Get review (*/api/shops/{shopId}/reviews/{reviewId}*)
- [**POST**] Add review (*/api/shops/{shopId}/reviews*)
- [**PATCH**] Change rating in review (*/api/shops/{shopId}/reviews/{reviewId}*)
- [**DELETE**] Delete review (*/api/shops/{shopId}/reviews/{reviewId}*)
### Service
- [**GET**] Get all services (*/api/shops/{shopId}/services*)
- [**GET**] Get service (*/api/shops/{shopId}/services/{serviceId}*)
- [**POST**] Add service (*/api/shops/{shopId}/services*)
- [**PATCH**] Change price in service (*/api/shops/{shopId}/services/{serviceId}*)
- [**DELETE**] Delete service (*/api/shops/{shopId}/services/{serviceId}*)
### Shop
- [**GET**] Get all shops (*/api/shops*)
- [**GET**] Get shop (*/api/shops/{shopId}*)
- [**POST**] Add shop (*/api/shops*)
- [**PUT**] Change shop info (*/api/shops/{shopId}*)
- [**PUT**] Change shop address (*/api/shops/{shopId}/address*)
- [**DELETE**] Delete shop (*/api/shops/{shopId}*)
### TimeTable
- [**GET**] Get schedule (*/api/shopAdmin/schedule)
- [**POST**] Add working slots schedule (*/api/shopAdmin/schedule)
- [**DELETE**] Delete working slot (*/api/shopAdmin/schedule)
### Appointment
- [**POST**] Add appointment (*/api/shops/{shopId}/appointments)
