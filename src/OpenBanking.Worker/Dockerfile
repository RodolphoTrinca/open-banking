#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["OpenBanking.Worker/OpenBanking.Worker.csproj", "OpenBanking.Worker/"]
RUN dotnet restore "./OpenBanking.Worker/OpenBanking.Worker.csproj"
COPY . .
WORKDIR "/src/OpenBanking.Worker"
RUN dotnet build "./OpenBanking.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./OpenBanking.Worker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OpenBanking.Worker.dll"]