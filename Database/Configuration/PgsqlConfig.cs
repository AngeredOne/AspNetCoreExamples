namespace Database.Configuration;

public class PgsqlConfig
{
    public string Host { get; set; } = null!;
    public int Port { get; set; }
    public string DbName { get; set; } = null!;
    public string User { get; set; } = null!;
    public string Password { get; set; } = null!;
}