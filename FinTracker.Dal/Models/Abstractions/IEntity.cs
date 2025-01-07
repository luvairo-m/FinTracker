namespace FinTracker.Dal.Models.Abstractions;

/// <summary>
/// Сущность в базе данных с идентификатором.
/// </summary>
public interface IEntity
{
    Guid Id { get; set; }
}