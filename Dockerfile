# Build context is the repository root (see docker-compose.yml).

# --- Build stage ---
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy only the project files first so 'restore' is cached until they change.
COPY src/AgroShop.API/*.csproj ./src/AgroShop.API/
COPY src/AgroShop.Application/*.csproj ./src/AgroShop.Application/
COPY src/AgroShop.Core/*.csproj ./src/AgroShop.Core/
COPY src/AgroShop.Persistence/*.csproj ./src/AgroShop.Persistence/
RUN dotnet restore src/AgroShop.API/AgroShop.API.csproj

# Copy the rest of the source and publish.
COPY . .
RUN dotnet publish src/AgroShop.API/AgroShop.API.csproj -c Release -o /app/publish /p:UseAppHost=false

# --- Runtime stage ---
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Kestrel listens on 8080 inside the container by default (.NET 8+).
EXPOSE 8080

ENTRYPOINT ["dotnet", "AgroShop.API.dll"]
