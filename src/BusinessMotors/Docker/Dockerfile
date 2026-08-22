# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80

# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src

COPY BusinessMotors.sln .
COPY BusinessMotors.API/BusinessMotors.API.csproj BusinessMotors.API/
COPY BusinessMotors.Application/BusinessMotors.Application.csproj BusinessMotors.Application/
COPY BusinessMotors.Domain/BusinessMotors.Domain.csproj BusinessMotors.Domain/
COPY BusinessMotors.Infrastructure/BusinessMotors.Infrastructure.csproj BusinessMotors.Infrastructure/
COPY BusinessMotors.Integration/BusinessMotors.Integration.csproj BusinessMotors.Integration/
COPY BusinessMotors.Tests/BusinessMotors.Tests.csproj BusinessMotors.Tests/

RUN dotnet restore BusinessMotors.sln

COPY . .

WORKDIR /src/BusinessMotors.API
RUN dotnet publish BusinessMotors.API.csproj -c Release -o /app/publish /p:UseAppHost=false

# Final
FROM base AS final
WORKDIR /app

COPY --from=build /app/publish .
COPY --from=build /src/BusinessMotors.API/Keys ./Keys

ENTRYPOINT ["dotnet", "BusinessMotors.API.dll"]