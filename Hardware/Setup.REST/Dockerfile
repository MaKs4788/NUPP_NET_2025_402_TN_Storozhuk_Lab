# ===== BUILD STAGE =====
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# копіюємо тільки потрібні csproj
COPY Setup.Infrastructure/Setup.Infrastructure.csproj Setup.Infrastructure/
COPY Setup.REST/Setup.REST.csproj Setup.REST/

# restore тільки REST-проєкту
RUN dotnet restore Setup.REST/Setup.REST.csproj

# копіюємо код
COPY Setup.Infrastructure/ Setup.Infrastructure/
COPY Setup.REST/ Setup.REST/

# build
RUN dotnet publish Setup.REST/Setup.REST.csproj -c Release -o /app/publish

# ===== RUNTIME STAGE =====
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Setup.REST.dll"]
