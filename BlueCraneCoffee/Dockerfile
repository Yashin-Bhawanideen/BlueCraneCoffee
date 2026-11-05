# Runtime base image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 10000

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY . .

# Show what the build context contains, then find and publish the project
RUN echo "=== Top level of build context ===" && ls -la \
    && echo "=== All project files found ===" && find . -name "*.csproj" -o -name "*.sln" \
    && CSPROJ=$(find . -name "QuoteManager_Pro.csproj" | head -n 1) \
    && if [ -z "$CSPROJ" ]; then echo "ERROR: QuoteManager_Pro.csproj not found in build context"; exit 1; fi \
    && echo "Using project: $CSPROJ" \
    && dotnet restore "$CSPROJ" \
    && dotnet publish "$CSPROJ" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false --no-restore

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# Bind to the port Render provides (defaults to 10000)
ENTRYPOINT ["sh", "-c", "ASPNETCORE_URLS=http://0.0.0.0:${PORT:-10000} exec dotnet QuoteManager_Pro.dll"]
