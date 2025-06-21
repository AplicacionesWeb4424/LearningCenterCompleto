using LearningCenterPlatform.Publishing.Domain.Model.Entities;
using LearningCenterPlatform.Publishing.Domain.Repositories;
using LearningCenterPlatform.Shared.Infrastructure.Persistence.EFC.Configuration;
using LearningCenterPlatform.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LearningCenterPlatform.Publishing.Infrastructure.Persistence.EFC.Repositories
{
    public class AssetsRepository(AppDbContext context) : BaseRepository<Asset>(context), IAssetsRepository
    {
        public async Task<IEnumerable<Asset>> FindByAssetsIdAsync(int tutorialId)
        {
            return await Context.Set<Asset>()
                .Where(asset => asset.TutorialId == tutorialId)
                .ToListAsync();
        }
    }
}
