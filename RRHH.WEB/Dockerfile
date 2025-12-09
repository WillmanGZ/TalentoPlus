# Etapa de build con .NET 10
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY RRHH.WEB/*.csproj RRHH.WEB/
RUN dotnet restore RRHH.WEB/RRHH.WEB.csproj

COPY . .
WORKDIR /src/RRHH.WEB
RUN dotnet publish RRHH.WEB.csproj -c Release -o /publish /p:UseAppHost=false

# Etapa final con ASP.NET 10
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

COPY --from=build /publish .

ENV ASPNETCORE_URLS=http://0.0.0.0:5041
EXPOSE 5041

ENTRYPOINT ["dotnet", "RRHH.WEB.dll"]
