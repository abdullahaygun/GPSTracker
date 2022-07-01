#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GPSTracker/Presentation/GPSTracker.API/GPSTracker.API.csproj", "GPSTracker/Presentation/GPSTracker.API/"]
COPY ["GPSTracker/Infrastructure/GPSTracker.Infrastructure/GPSTracker.Infrastructure.csproj", "GPSTracker/Infrastructure/GPSTracker.Infrastructure/"]
COPY ["GPSTracker/Infrastructure/GPSTracker.Persistance/GPSTracker.Persistance.csproj", "GPSTracker/Infrastructure/GPSTracker.Persistance/"]
COPY ["GPSTracker/Core/GPSTracker.Domain/GPSTracker.Domain.csproj", "GPSTracker/Core/GPSTracker.Domain/"]
RUN dotnet restore "GPSTracker/Presentation/GPSTracker.API/GPSTracker.API.csproj"
COPY . .
WORKDIR "/src/GPSTracker/Presentation/GPSTracker.API"
RUN dotnet build "GPSTracker.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GPSTracker.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "GPSTracker.API.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet GPSTracker.API.dll