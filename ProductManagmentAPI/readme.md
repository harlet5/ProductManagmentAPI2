# ProductManagmentAPI

REST API for managing product, product groups and stores.

## Setup Instructions

1. Clone the repository
2. Install `dotnet tool install --global dotnet-ef`
3. Create migration `dotnet ef migrations add InitialCreate`
4. Update database `dotnet ef database update`
5. Use `ProductManagmentAPI.http` file to hit endpoints or Postman with given endpoints:
	POST http://localhost:5226/api/product/AddProduct
	GET http://localhost:5226/api/product/{id?}
	GET http://localhost:5226/api/product/GetProductGroupTree
