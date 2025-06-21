using LearningCenterPlatform.Publishing.Domain.Model.Entities;
using LearningCenterPlatform.Shared.Domain.Repositories;

namespace LearningCenterPlatform.Publishing.Domain.Repositories
{
    public interface IAssetsRepository : IBaseRepository<Asset>
    {
        Task<IEnumerable<Asset>> FindByAssetsIdAsync(int tutorialId);

    }
}
