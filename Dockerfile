#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Presentation/GPSTracker.API/GPSTracker.API.csproj", "Presentation/GPSTracker.API/"]
COPY ["Infrastructure/GPSTracker.Infrastructure/GPSTracker.Infrastructure.csproj", "Infrastructure/GPSTracker.Infrastructure/"]
COPY ["Infrastructure/GPSTracker.Persistance/GPSTracker.Persistance.csproj", "Infrastructure/GPSTracker.Persistance/"]
COPY ["Core/GPSTracker.Domain/GPSTracker.Domain.csproj", "Core/GPSTracker.Domain/"]
RUN dotnet restore "Presentation/GPSTracker.API/GPSTracker.API.csproj"
COPY . .
WORKDIR "/src/Presentation/GPSTracker.API"
RUN dotnet build "GPSTracker.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GPSTracker.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "GPSTracker.API.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet GPSTracker.API.dll