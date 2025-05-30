FROM mcr.microsoft.com/dotnet/sdk:8.0 AS migrate

WORKDIR /src

# Установить EF CLI
RUN dotnet tool install -g dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

# Копируем проект и исходники для миграций
COPY ExceptionJournalApiExample.csproj ./
COPY ./ ./

# Восстанавливаем пакеты (без публикации)
RUN dotnet restore ExceptionJournalApiExample.csproj

# Точка входа: script миграции
COPY migration.sh ./migration.sh
RUN chmod +x migration.sh
ENTRYPOINT ["./migration.sh"]