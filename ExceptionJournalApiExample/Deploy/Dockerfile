FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ExceptionJournalApiExample.csproj ./
COPY ./ ./
RUN dotnet restore ExceptionJournalApiExample.csproj
RUN dotnet publish ExceptionJournalApiExample.csproj -c Release -o /app

# Рантайм-образ
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Копируем опубликованный код
COPY --from=build /app ./

# Запуск
ENTRYPOINT ["dotnet", "ExceptionJournalApiExample.dll"]