using LearningCenterPlatform.Publishing.Domain.Model.Aggregate;
using LearningCenterPlatform.Publishing.Domain.Repositories;
using LearningCenterPlatform.Shared.Infrastructure.Persistence.EFC.Configuration;
using LearningCenterPlatform.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LearningCenterPlatform.Publishing.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
///     Represents the tutorial repository in the ACME Learning Center Platform.
/// </summary>
/// <param name="context">
///     The <see cref="AppDbContext" /> to use.
/// </param>
public class TutorialRepository(AppDbContext context) : BaseRepository<Tutorial>(context), ITutorialRepository
{
    // <inheritdoc />
    public async Task<IEnumerable<Tutorial>> FindByCategoryIdAsync(int categoryId)
    {
        return await Context.Set<Tutorial>()
            .Include(tutorial => tutorial.Category)
            .Where(tutorial => tutorial.CategoryId == categoryId)
            .ToListAsync();
    }

    public async Task<bool> ExistsByTitleAsync(string title)
    {
        return await Context.Set<Tutorial>().AnyAsync(tutorial => tutorial.Title == title);
    }

    // <inheritdoc />
    public new async Task<Tutorial?> FindByIdAsync(int id)
    {
        return await Context.Set<Tutorial>()
            .Include(tutorial => tutorial.Category)
            .FirstOrDefaultAsync(tutorial => tutorial.Id == id);
    }

    // <inheritdoc />
    public new async Task<IEnumerable<Tutorial>> ListAsync()
    {
        return await Context.Set<Tutorial>()
            .Include(tutorial => tutorial.Category)
            .ToListAsync();
    }
}