FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Application.Userspaces.RuntimeJournalApi/Application.Userspaces.RuntimeJournalApi.csproj", "Application.Userspaces.RuntimeJournalApi/"]
COPY ["Application.Middlewares.RuntimeJournal/Application.Middlewares.RuntimeJournal.csproj", "Application.Middlewares.RuntimeJournal/"]
COPY ["Repository.RuntimeJournal/Repository.RuntimeJournal.csproj", "Repository.RuntimeJournal/"]
COPY ["Domain.RuntimeJournal/Domain.RuntimeJournal.csproj", "Domain.RuntimeJournal/"]
COPY ["Application.Backends.RuntimeJournal/Application.Backends.RuntimeJournal.csproj", "Application.Backends.RuntimeJournal/"]
COPY ["RuntimeJournalServices/RuntimeJournalServices.csproj", "RuntimeJournalServices/"]
COPY ["Database.RuntimeJournal/Database.RuntimeJournal.csproj", "Database.RuntimeJournal/"]
COPY ["Database/Database.csproj", "Database/"]
RUN dotnet restore "Application.Userspaces.RuntimeJournalApi/Application.Userspaces.RuntimeJournalApi.csproj"
COPY . .
WORKDIR "/src/Application.Userspaces.RuntimeJournalApi"
RUN dotnet build "./Application.Userspaces.RuntimeJournalApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Application.Userspaces.RuntimeJournalApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Application.Userspaces.RuntimeJournalApi.dll"]
