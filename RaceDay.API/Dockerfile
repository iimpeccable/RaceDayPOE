# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["RacingDays.API/RacingDays.API.csproj", "RacingDays.API/"]
RUN dotnet restore "RacingDays.API/RacingDays.API.csproj"
COPY RacingDays.API/ RacingDays.API/
WORKDIR "/src/RacingDays.API"
RUN dotnet build "RacingDays.API.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "RacingDays.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RacingDays.API.dll"]
