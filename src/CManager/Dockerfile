FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CManager.API/CManager.API.csproj", "CManager.API/"]
RUN dotnet restore "CManager.API/CManager.API.csproj"
COPY . .
WORKDIR "/src/CManager.API"
RUN dotnet build "CManager.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CManager.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CManager.API.dll"]