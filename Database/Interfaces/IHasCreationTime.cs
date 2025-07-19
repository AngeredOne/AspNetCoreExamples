namespace Database.Interfaces;

/// <summary>
/// Контракт хранимой модели для хранения объектов с атрибутом времени создания записи
/// </summary>
public interface IHasCreationTime
{
    DateTime CreatedAt { get; set; }
}