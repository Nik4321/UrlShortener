# Build stage
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

COPY ["UrlShortener.sln", "./"]
COPY ["UrlShortener.Api/UrlShortener.Api.csproj", "UrlShortener.Api/"]
COPY ["UrlShortener.Data/UrlShortener.Data.csproj", "UrlShortener.Data/"]
COPY ["UrlShortener.Infrastructure/UrlShortener.Infrastructure.csproj", "UrlShortener.Infrastructure/"]
COPY ["UrlShortener.Infrastructure.Tests/UrlShortener.Infrastructure.Tests.csproj", "UrlShortener.Infrastructure.Tests/"]

RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS urlshortener
WORKDIR /app

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "UrlShortener.Api.dll"]

