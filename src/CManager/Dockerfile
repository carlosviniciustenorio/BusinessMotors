FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY "CManager.API/*.csproj" "CManager.API/"
COPY "CManager.Application/*.csproj" "CManager.Application/"
COPY "CManager.Domain/*.csproj" "CManager.Domain/"
COPY "CManager.Infrastructure/*.csproj" "CManager.Infrastructure/"
COPY "CManager.Integration/*.csproj" "CManager.Integration/"
COPY "CManager.sln" "./"

RUN dotnet restore

COPY . .
WORKDIR "/src/CManager.API"
RUN dotnet build "CManager.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CManager.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CManager.API.dll"]