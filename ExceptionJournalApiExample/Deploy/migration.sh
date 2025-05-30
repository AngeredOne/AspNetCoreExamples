#!/bin/bash
set -e

Host=${DB__Host}
Port=${DB__Port}
User=${DB__User}
Password=${DB__Password}
DbName=${DB__DbName}

# Проверка необходимых переменных
: "${Host:?Need Host env var}"
: "${Port:?Need Port env var}"
: "${User:?Need User env var}"
: "${Password:?Need Password env var}"
: "${DbName:?Need DbName env var}"

# Сборка строки подключения из отдельных полей
CONNECTION_STRING="Host=${Host};Port=${Port};Database=${DbName};Username=${User};Password=${Password}"

echo "Applying EF Core migrations"

dotnet ef database update --no-build \
  --context AppDbContext \
  --connection "$CONNECTION_STRING" \
  --project /src/ExceptionJournalApiExample.csproj \
  --startup-project /src/ExceptionJournalApiExample.csproj