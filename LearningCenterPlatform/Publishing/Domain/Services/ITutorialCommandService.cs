using LearningCenterPlatform.Publishing.Domain.Model.Aggregate;
using LearningCenterPlatform.Publishing.Domain.Model.Commands;

namespace LearningCenterPlatform.Publishing.Domain.Services;

/// <summary>
///     Represents the tutorial command service in the ACME Learning Center Platform.
/// </summary>
public interface ITutorialCommandService
{
    /// <summary>
    ///     Handles the add video asset to tutorial command in the ACME Learning Center Platform.
    /// </summary>
    /// <param name="command">
    ///     The <see cref="AddVideoAssetToTutorialCommand" /> command to handle.
    /// </param>
    /// <returns>
    ///     The updated <see cref="Tutorial" /> entity.
    /// </returns>
    Task<Tutorial?> Handle(AddVideoAssetToTutorialCommand command);

    /// <summary>
    ///     Handles the create tutorial command in the ACME Learning Center Platform.
    /// </summary>
    /// <param name="command">
    ///     The <see cref="CreateTutorialCommand" /> command to handle.
    /// </param>
    /// <returns>
    ///     The created <see cref="Tutorial" /> entity.
    /// </returns>
    Task<Tutorial?> Handle(CreateTutorialCommand command);
}