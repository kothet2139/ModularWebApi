
# ModularWebApi

A modular, scalable, and maintainable ASP.NET Core Web API project structure built for modern applications.  
This project demonstrates how to organize a .NET Web API into distinct feature modules, promoting clean architecture, separation of concerns, and easier extensibility.

## Features

- **Modular Architecture** — organize features into self-contained modules.
- **Clean and Scalable Structure** — easy to maintain and extend.
- **ASP.NET Core 8.0** — leveraging the latest .NET technologies.
- **Controller-based APIs** — structured around MVC Controllers.
- **Dependency Injection** — follows best practices for service registration.
- **API Versioning Ready** — structured for future expansions.

## Project Structure

```
/ModularWebApi
│
├── /Core                 # Core abstractions (Interfaces, Base classes, Shared logic)
├── /Modules
│    ├── /Products        # Example feature module
│    ├── /Orders          # (Optional) Add more modules easily
│
├── /Infrastructure       # Infrastructure layer (Data access, External services)
├── /Shared                # Common/shared components across modules
├── /WebApi               # Entry point and API configuration
│    ├── Program.cs       # Application startup
│    ├── Extensions       # Application Service Extensions
│
├── ModularWebApi.sln      # Solution file
```

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Visual Studio 2022+ or Visual Studio Code

### Running the Application

1. Clone the repository:

   ```bash
   git clone https://github.com/kothet2139/ModularWebApi.git
   ```

2. Navigate into the project directory:

   ```bash
   cd ModularWebApi/WebApi
   ```

3. Restore dependencies:

   ```bash
   dotnet restore
   ```

4. Run the API:

   ```bash
   dotnet run
   ```

5. The API will start on `https://localhost:5001` or as configured in the `launchSettings.json`.

### Adding New Modules

To add a new module:
- Create a new folder under `/Modules`.
- Implement your Controllers, Services, and DTOs inside it.
- Register services inside the Dependency Injection container using extension methods if needed.
- Add routing and endpoints in your module setup.

## Contributing

Contributions are welcome!  
Feel free to open issues or submit pull requests to improve this project.

## License

This project is licensed under the [MIT License](LICENSE).
