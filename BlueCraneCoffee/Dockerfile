# Runtime base image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 10000

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BlueCraneCoffee/BlueCraneCoffee.csproj", "BlueCraneCoffee/"]
RUN dotnet restore "./BlueCraneCoffee/BlueCraneCoffee.csproj"
COPY . .
WORKDIR "/src/BlueCraneCoffee"
RUN dotnet build "./BlueCraneCoffee.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BlueCraneCoffee.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Bind to the port Render provides (defaults to 10000)
ENTRYPOINT ["sh", "-c", "ASPNETCORE_URLS=http://0.0.0.0:${PORT:-10000} exec dotnet BlueCraneCoffee.dll"]
