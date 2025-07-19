namespace Database.Interfaces;

/// <summary>
/// Контракт модели данных для сохранения во внеш. хранилищах
/// </summary>
public interface IDbObject
{
    /// <summary>
    /// Уникальный инкрементный идентификатор
    /// </summary>
    Guid Id { get; set; }
}