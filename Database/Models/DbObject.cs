using System.ComponentModel.DataAnnotations;

namespace Database.Models;

/// <summary>
/// Базовый класс для доменных моделей, совместимый с EfCore
/// </summary>
public abstract class DbObject : Interfaces.IDbObject
{
    [Key]
    public Guid Id { get; set; } = Guid.Empty;
}