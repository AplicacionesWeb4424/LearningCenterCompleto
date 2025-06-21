using LearningCenterPlatform.Publishing.Domain.Model.Aggregate;
using LearningCenterPlatform.Publishing.Domain.Model.Commands;
using LearningCenterPlatform.Publishing.Domain.Repositories;
using LearningCenterPlatform.Publishing.Domain.Services;
using LearningCenterPlatform.Publishing.Infrastructure.Persistence.EFC.Repositories;
using LearningCenterPlatform.Shared.Domain.Repositories;

namespace LearningCenterPlatform.Publishing.Application.Internal.CommandServices;

public class TutorialCommandService(
    ITutorialRepository tutorialRepository,
    ICategoryRepository categoryRepository,
    IAssetsRepository assetsRepository,
    IUnitOfWork unitOfWork)
    : ITutorialCommandService
{
    /// <inheritdoc />
    public async Task<Tutorial?> Handle(AddVideoAssetToTutorialCommand command)
    {
        var tutorial = await tutorialRepository.FindByIdAsync(command.TutorialId);
        if (tutorial is null) throw new Exception("Tutorial not found");
        tutorial.AddVideo(command.VideoUrl);
        await unitOfWork.CompleteAsync();
        return tutorial;
    }

    /// <inheritdoc />
    public async Task<Tutorial?> Handle(CreateTutorialCommand command)
    {
        var category = await categoryRepository.FindByIdAsync(command.CategoryId);
        if (category is null) throw new Exception("Category not found");
        if (await tutorialRepository.ExistsByTitleAsync(command.Title))
            throw new Exception("Tutorial with the same title already exists");
        var tutorial = new Tutorial(command);
        await tutorialRepository.AddAsync(tutorial);
        await unitOfWork.CompleteAsync();
        tutorial.Category = category;
        return tutorial;
    }

    public async Task<Tutorial?> Handle(UpdateTutorialCommand command)
    {
        //  var tutorial = await tutorialRepository.FindByIdAsync(command.Id);
        //if (tutorial is null) throw new Exception("Tutorial not found");

        var tutorial = new Tutorial(command);
        if (await tutorialRepository.ExistsByTitleAsync(command.Title))
            throw new Exception("Tutorial with the same title already exists");

        tutorialRepository.Update(tutorial);
        await unitOfWork.CompleteAsync();
        return tutorial;
    }

    public async Task <Tutorial?> Handle(DeleteTutorialCommand command)
    {
        var assets = await assetsRepository.FindByAssetsIdAsync(command.Id);

        foreach (var asset in assets)
        {
            assetsRepository.Remove(asset);
        }

        var tutorial = await tutorialRepository.FindByIdAsync(command.Id);
        if (tutorial is null) throw new Exception("Tutorial not found");
        tutorialRepository.Remove(tutorial);
        await unitOfWork.CompleteAsync();
        return tutorial;
    }
}