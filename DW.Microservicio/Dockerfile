FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["DW.Microservicio/DW.Microservicio.csproj", "DW.Microservicio/"]
RUN dotnet restore "DW.Microservicio/DW.Microservicio.csproj"
COPY . .
WORKDIR "/src/DW.Microservicio"
RUN dotnet build "DW.Microservicio.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DW.Microservicio.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DW.Microservicio.dll"]