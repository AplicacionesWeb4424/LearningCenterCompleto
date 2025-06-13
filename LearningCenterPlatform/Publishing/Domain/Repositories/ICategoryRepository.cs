using LearningCenterPlatform.Publishing.Domain.Model.Entities;
using LearningCenterPlatform.Shared.Domain.Repositories;

namespace LearningCenterPlatform.Publishing.Domain.Repositories;

/// <summary>
///     Represents the category repository in the ACME Learning Center Platform.
/// </summary>
public interface ICategoryRepository : IBaseRepository<Category>
{
}