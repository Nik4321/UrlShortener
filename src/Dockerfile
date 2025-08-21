# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["UrlShortener.sln", "./"]
COPY ["UrlShortener.Api/UrlShortener.Api.csproj", "UrlShortener.Api/"]
COPY ["UrlShortener.Data/UrlShortener.Data.csproj", "UrlShortener.Data/"]
COPY ["UrlShortener.Infrastructure/UrlShortener.Infrastructure.csproj", "UrlShortener.Infrastructure/"]
COPY ["UrlShortener.Infrastructure.Tests/UrlShortener.Infrastructure.Tests.csproj", "UrlShortener.Infrastructure.Tests/"]

RUN dotnet restore
COPY . .

RUN dotnet build -c Release --no-restore

FROM build AS publish
RUN dotnet publish "UrlShortener.Api/UrlShortener.Api.csproj" -c Release -o /app/publish/api --no-build

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS urlshortener
ENV ASPNETCORE_URLS=http://+:80
WORKDIR /app
COPY --from=publish /app/publish/api .
ENTRYPOINT ["dotnet", "UrlShortener.Api.dll"]

