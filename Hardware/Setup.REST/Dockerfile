# =========================
# 1. Build stage
# =========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Копіюємо csproj і відновлюємо залежності
COPY *.sln .
COPY Setup.Infrastructure/*.csproj ./Setup.Infrastructure/
COPY Setup.REST/*.csproj ./Setup.REST/

RUN dotnet restore

# Копіюємо увесь код
COPY Setup.Infrastructure/. ./Setup.Infrastructure/
COPY Setup.REST/. ./Setup.REST/

# Build проекту
WORKDIR /app/Setup.REST
RUN dotnet publish -c Release -o /app/publish

# =========================
# 2. Runtime stage
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Копіюємо файли з попередньої стадії
COPY --from=build /app/publish .

# Виставляємо порт (за потреби)
EXPOSE 5000

# Вказуємо команду запуску
ENTRYPOINT ["dotnet", "Setup.REST.dll"]
