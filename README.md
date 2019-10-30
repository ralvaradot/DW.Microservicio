# DWMicroservicios
Web API como Microservicio NET Core en Digital Ware

# Microservicios .NET Core

Microservicios es una Arquitectura, que nos permite desacoplar las actividades al momento de crear nuestra aplicación, nos permite hacer un testing muy mas sencillo, los equipos pueden realizar más tareas y más sencillas. El deploy de los servicios se hace de manera independiente. Se pueden hacer pequeños equipos de trabajo. Se organiza mejor las habilidades del equipo de desarrollo. Todo se puede hacer de manera gradual, para poder entender como evolucionar las aplicaciones monolíticas.

Todo esta desacoplado, si, pero necesitamos la comunicación entre humanos, pues es muy importante conocer lo que pasa entre los equipos de desarrollo.

Los microservicios no son la solución a todos, me sirve para escalar.

La coordinación del equipo de trabajo es importante la comunicación en torno a los componentes a utilizar, las mecánicas de reintentos, las tecnologías de mensajería entre los microservicios.

Crear un componente que haga una sola cosa, hay que colocar mucha instrumentación en el microservicio, traces, logs, que pasa si una llamada fallo, por ejemplo, Netflix, Uber.


## Patrones para Microservicios

•	Comunicación Async Publicador y subscriptor, utilizando Event Bus o RabbitMQ para comunicar servicios.

•	Helpcheck, usamos Docker, el contenedor se puede monitorear

•	Resilencia de aplicaciones Cloud, HttpClientFactory, permite colocar políticas dentro del HttpClient, o usar tambien Polly. El Factory permite inyectarle políticas. Polly permite la creación de políticas. Aplicar cuantos reintentos si falla la llamada.

•	API Gateway, es lo mejor, da encapsulamiento del servicio, no hacer el llamado directo. Los servicios no quedan expuestos, da balanceo, puede generar sesión entre los servicios. Se recomienda Ocelot como framework para .NET permite hacer un API Gateway para .NET.

•	Orquestadores, permite Docker Composer, Azure Dev Spaces


## Referencias

https://microservices.io/index.html

![](https://raw.githubusercontent.com/ralvaradot/DW.Microservicio/master/Assets/Microservicio001.png)


# Creando proyecto Microservicio con NET Core
![](https://raw.githubusercontent.com/ralvaradot/DW.Microservicio/master/Assets/Microservicio002.png)

![](https://raw.githubusercontent.com/ralvaradot/DW.Microservicio/master/Assets/Microservicio002.png)

![](https://raw.githubusercontent.com/ralvaradot/DW.Microservicio/master/Assets/Microservicio003.png)

![](https://raw.githubusercontent.com/ralvaradot/DW.Microservicio/master/Assets/Microservicio004.png)

![](https://raw.githubusercontent.com/ralvaradot/DW.Microservicio/master/Assets/Microservicio005.png)

YA creado el proyecto base, creamos una nueva carpeta llamada Models, para crear el modelo al cual vamos a hacer el CRUD

![](https://raw.githubusercontent.com/ralvaradot/DW.Microservicio/master/Assets/Microservicio006.png)

Agregamos la clase que representa el modelo de nuestra base de datos.

![](https://raw.githubusercontent.com/ralvaradot/DW.Microservicio/master/Assets/Microservicio007.png)

![](https://raw.githubusercontent.com/ralvaradot/DW.Microservicio/master/Assets/Microservicio008.png)

Llamamos Product al modelo, le adicionamos las siguientes propiedades

~~~
public class Product
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public decimal Precio { get; set; }
    public string NombreImagen { get; set; }
    public string UriImagen { get; set; }
    public int TipoProductoId { get; set; }
    public TipoProducto TipoProducto { get; set; }
    public int MarcaId { get; set; }
    public Marca Marca { get; set; }
    public int Stock { get; set; }
    public int PuntoReorden { get; set; }
    public int StockMaximo { get; set; }

    public bool OnReorder { get; set; }
    public CatalogItem() { }
}

~~~

Creamos ahora el modelo de TipoProducto

~~~
public class TipoProducto
{
    public int Id { get; set; }

    public string NombreTipoProducto { get; set; }
}

~~~


Ahora creamos el modelo de Marca.

~~~
public class Marca
{
    public int Id { get; set; }
    public string NombreMarca { get; set; }
}

~~~

Creamos la carpeta de Data, para colocar alli el Context de EntityFramework que nos servirá para conectarnos a la base de datos.

![](https://raw.githubusercontent.com/ralvaradot/DW.Microservicio/master/Assets/Microservicio009.png)


Creamos la clase ProductContext y que herede de DbContext, y adicionamos los DbSet de las tablas correspondientes.
![](https://raw.githubusercontent.com/ralvaradot/DW.Microservicio/master/Assets/Microservicio010.png)

![](https://raw.githubusercontent.com/ralvaradot/DW.Microservicio/master/Assets/Microservicio011.png)

![](https://raw.githubusercontent.com/ralvaradot/DW.Microservicio/master/Assets/Microservicio012.png)

El codigo completo del contexto.

~~~
using Catalog.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext()
        {
        }

        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\mssqllocaldb;Database=DWMicroservice00;" +
               "Trusted_Connection=True;");
        }

        public DbSet<Product> Productos { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<TipoProducto> TiposProducto { get; set; }

    }
}

~~~

Ahora debemos configurar e indicar donde se encuentra nuestra base de datos, vamos al appsettings.json, lo abrimos y creamos una nueva 

~~~
{
  "ConnectionStrings": {
    "DWMicroServiceProduct": "Server=(localdb)\\mssqllocaldb;Database=DWMicroservice00;Trusted_Connection=True;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Warning"
    }
  },
  "AllowedHosts": "*"
}

~~~

Ahora crearemos el Controlador de nuestro API con el CRUD de productos.
![](https://raw.githubusercontent.com/ralvaradot/DW.Microservicio/master/Assets/Microservicio013.png)

![](https://raw.githubusercontent.com/ralvaradot/DW.Microservicio/master/Assets/Microservicio014.png)

![](https://raw.githubusercontent.com/ralvaradot/DW.Microservicio/master/Assets/Microservicio015.png)

![](https://raw.githubusercontent.com/ralvaradot/DW.Microservicio/master/Assets/Microservicio016.png)

![](https://raw.githubusercontent.com/ralvaradot/DW.Microservicio/master/Assets/Microservicio017.png)

![](https://raw.githubusercontent.com/ralvaradot/DW.Microservicio/master/Assets/Microservicio018.png)

![](https://raw.githubusercontent.com/ralvaradot/DW.Microservicio/master/Assets/Microservicio019.png)

Una vez creado el controlador, debemos configurar la Inyeccion de dependencias en Startup.cs.
~~~
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ProductContext>(options =>
            {
                options.UseSqlServer(Configuration
                    .GetConnectionString("DWMicroServiceProduct"),
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(
                            typeof(Startup).GetTypeInfo()
                            .Assembly.GetName().Name);

                        // Configurando Connection resilence
                        sqlOptions
                        .EnableRetryOnFailure(maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                    });

                options.ConfigureWarnings(warnings =>
                    warnings.Throw(
                        RelationalEventId.QueryClientEvaluationWarning));
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

~~~

Ahora vamos al PAckage Manger Console y Adicionamos la migración y el update database.

![](https://raw.githubusercontent.com/ralvaradot/DW.Microservicio/master/Assets/Microservicio020.png)

![](https://raw.githubusercontent.com/ralvaradot/DW.Microservicio/master/Assets/Microservicio021.png)

Ahora actualizamos la base de datos

![](https://raw.githubusercontent.com/ralvaradot/DW.Microservicio/master/Assets/Microservicio022.png)

![](https://raw.githubusercontent.com/ralvaradot/DW.Microservicio/master/Assets/Microservicio023.png)

Revisamos el SQL Server Object Explorer

![](https://raw.githubusercontent.com/ralvaradot/DW.Microservicio/master/Assets/Microservicio024.png)

![](https://raw.githubusercontent.com/ralvaradot/DW.Microservicio/master/Assets/Microservicio025.png)


Ejecutamos
![](https://raw.githubusercontent.com/ralvaradot/DW.Microservicio/master/Assets/Microservicio026.png)


## El Archivo DockerFile

Revisemos el archivo Dockfile

~~~
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Catalog.API/Catalog.API.csproj", "Catalog.API/"]
RUN dotnet restore "Catalog.API/Catalog.API.csproj"
COPY . .
WORKDIR "/src/Catalog.API"
RUN dotnet build "Catalog.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Catalog.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.API.dll"]

~~~

Como se aprecia es un archivo de instrucciones secuenciales para realizar tareas en el contenedor y que nuestro Microservicio quede fucnionando como lo tenemos en nuestra maquina de desarrollo.

Les queda como tarea instalar el Docker Desktop y seguir utilizando esta tecnología.


