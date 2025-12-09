FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["RRHH.WEB/RRHH.WEB.csproj", "RRHH.WEB/"]
RUN dotnet restore "RRHH.WEB/RRHH.WEB.csproj"
COPY . .
WORKDIR "/src/RRHH.WEB"
RUN dotnet build "./RRHH.WEB.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./RRHH.WEB.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RRHH.WEB.dll"]
