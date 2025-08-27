# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o csproj e restaura dependências
COPY *.csproj ./
RUN dotnet restore

# Copia todo o código e publica
COPY . ./
RUN dotnet publish -c Release -o /app/out

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

EXPOSE 7140
ENTRYPOINT ["dotnet", "InventoryManagement.dll"]
