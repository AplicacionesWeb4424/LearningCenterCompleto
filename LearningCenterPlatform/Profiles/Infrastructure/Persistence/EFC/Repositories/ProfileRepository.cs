using LearningCenterPlatform.Profiles.Domain.Model.Aggregates;
using LearningCenterPlatform.Profiles.Domain.Model.ValueObjects;
using LearningCenterPlatform.Profiles.Domain.Repositories;
using LearningCenterPlatform.Shared.Infrastructure.Persistence.EFC.Configuration;
using LearningCenterPlatform.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace LearningCenterPlatform.Profiles.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// Profile repository implementation  
/// </summary>
/// <param name="context">
/// The database context
/// </param>
public class ProfileRepository(AppDbContext context) 
    : BaseRepository<Profile>(context), IProfileRepository
{
    /// <inheritdoc />
    public async Task<Profile?> FindProfileByEmailAsync(EmailAddress email)
    {
        return Context.Set<Profile>().FirstOrDefault(p => p.Email == email);
    }
}