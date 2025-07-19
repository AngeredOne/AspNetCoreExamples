using Database.Configuration;

namespace Database.Utils;

public static class PostgreConfigUtils
{
    public static string ToConnectionString(this PgsqlConfig config) =>
        $"Host={config.Host};Port={config.Port};Database={config.DbName};Username={config.User};Password={config.Password}";
}