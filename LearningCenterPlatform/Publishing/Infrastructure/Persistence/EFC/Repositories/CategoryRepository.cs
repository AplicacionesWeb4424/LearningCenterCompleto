using LearningCenterPlatform.Publishing.Domain.Model.Entities;
using LearningCenterPlatform.Publishing.Domain.Repositories;
using LearningCenterPlatform.Shared.Infrastructure.Persistence.EFC.Configuration;
using LearningCenterPlatform.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace LearningCenterPlatform.Publishing.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
///     Represents the category repository in the ACME Learning Center Platform.
/// </summary>
/// <param name="context">
///     The <see cref="AppDbContext" /> to use.
/// </param>
public class CategoryRepository(AppDbContext context) : BaseRepository<Category>(context), ICategoryRepository;