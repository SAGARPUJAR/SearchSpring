# Intelligent Reach API Test  

# Steps Implemented to Perform the Endpoints
 # Solution Creation
 - Created the ApiTest Web API Project
 - Created the ApiTest.Contracts Project
 - Created the ApiTest.Entity project 

 # Implemented ApiTest.Entity
 - Created the Entity and Repository Folders
 - Entity : Create a Product Class with Properties and Create a PaginationFilter.cs
 - Create the AppDbContext.cs and configure the InMemoryDatabase (Install Microsoft.EntityFrameworkCore.InMemory) and Seed the DataTable Product.
 - Repository : Create a IProductRepository.cs with Services.
 - ProductRepository : Implement all the services in the ProductRespository.

 # Implemented ApiTest.Contracts
 - Created Mapper and Model Folders to Implement Mapping and POCO Classes.
 - Install the AutoMapper to Map the POCO Classes and Entities which are main object.
 - Create a MappingProfile to Map the POCO and Entity Class for both Pagination and Product.
 - Create a Service i.e.. IProductContracts and define the services.
 - Implement Servies in ProductRepository.cs file.

 # Implemented ApiTest
 - Install the Dependecy Packages for the performing CRUD Operation 
    * Microsoft.AspNetCore.JsonPath -> To Work with the [HTTPPATCH]
    * Microsftt.AspNetCore.Mvc.NewtonsoftJson
 - Create the ProductController.
 - Configure the Depedency Injection to implement the services in program.cs file.
    * Registred the AppDbContext
    * Registered the both IProductRepository and IProductContracts using AddScoped.
    * Register the AutoMapper.
    * In Order to work with JsonPatch we need to attach the AddNewtonsoftJson as below.
        - builder.Services.AddControllers().AddNewtonsoftJson();
