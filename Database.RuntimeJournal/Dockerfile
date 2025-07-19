FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Database.RuntimeJournal/Database.RuntimeJournal.csproj", "Database.RuntimeJournal/"]
COPY ["Domain.RuntimeJournal/Domain.RuntimeJournal.csproj", "Domain.RuntimeJournal/"]
COPY ["Database/Database.csproj", "Database/"]
COPY ["Repository.RuntimeJournal/Repository.RuntimeJournal.csproj", "Repository.RuntimeJournal/"]
RUN dotnet restore "Database.RuntimeJournal/Database.RuntimeJournal.csproj"
COPY . .
WORKDIR "/src/Database.RuntimeJournal"
RUN dotnet build "./Database.RuntimeJournal.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Database.RuntimeJournal.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Database.RuntimeJournal.dll"]
