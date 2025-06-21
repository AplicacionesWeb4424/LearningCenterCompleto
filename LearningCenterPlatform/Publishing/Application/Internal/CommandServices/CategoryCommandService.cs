using LearningCenterPlatform.Publishing.Domain.Model.Commands;
using LearningCenterPlatform.Publishing.Domain.Model.Entities;
using LearningCenterPlatform.Publishing.Domain.Model.Events;
using LearningCenterPlatform.Publishing.Domain.Repositories;
using LearningCenterPlatform.Publishing.Domain.Services;
using LearningCenterPlatform.Shared.Domain.Repositories;
using Cortex.Mediator;

namespace LearningCenterPlatform.Publishing.Application.Internal.CommandServices;

/// <summary>
///     Represents the category command service in the ACME Learning Center Platform.
/// </summary>
/// <param name="categoryRepository">
///     The <see cref="ICategoryRepository" /> to use.
/// </param>
/// <param name="unitOfWork">
///     The <see cref="IUnitOfWork" /> to use.
/// </param>
public class CategoryCommandService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMediator domainEventPublisher)
    : ICategoryCommandService
{
    /// <inheritdoc />
    public async Task<Category?> Handle(CreateCategoryCommand command)
    {
        var category = new Category(command);
        await categoryRepository.AddAsync(category);
        await unitOfWork.CompleteAsync();

        await domainEventPublisher.PublishAsync(new CategoryCreatedEvent(category.Name));

        return category;
    }

    public async Task<Category?> Handle(UpdateCategoryCommand command) {
        var category = new Category(command);
        categoryRepository.Update(category);
        await unitOfWork.CompleteAsync();
        return category;
    }


    public async Task<Category?> Handle(DeleteCategoryCommand command)
    {
        var category = await categoryRepository.FindByIdAsync(command.Id);
        if (category == null) return null;
        categoryRepository.Remove(category);
        await unitOfWork.CompleteAsync();
        return category;
    }


}