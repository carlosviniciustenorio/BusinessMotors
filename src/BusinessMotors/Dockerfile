FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src
COPY "BusinessMotors.API/*.csproj" "BusinessMotors.API/"
COPY "BusinessMotors.Application/*.csproj" "BusinessMotors.Application/"
COPY "BusinessMotors.Domain/*.csproj" "BusinessMotors.Domain/"
COPY "BusinessMotors.Infrastructure/*.csproj" "BusinessMotors.Infrastructure/"
COPY "BusinessMotors.Integration/*.csproj" "BusinessMotors.Integration/"
COPY "BusinessMotors.Tests/*.csproj" "BusinessMotors.Tests/"
COPY "BusinessMotors.sln" "./"

RUN dotnet restore

COPY . .
WORKDIR "/src/BusinessMotors.API"
RUN dotnet build "BusinessMotors.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BusinessMotors.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BusinessMotors.API.dll"]