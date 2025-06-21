using LearningCenterPlatform.Publishing.Domain.Model.Events;
using LearningCenterPlatform.Shared.Application.Internal.EventHandlers;

namespace LearningCenterPlatform.Publishing.Application.Internal.EventHadlers
{
    public class CategoryCreatedEventHandler : IEventHandler<CategoryCreatedEvent>
    {
        public Task Handle(CategoryCreatedEvent domainEvent, CancellationToken cancellationToken)
        {
            return On(domainEvent);
        }

        private static Task On(CategoryCreatedEvent domainEvent)
        {
            Console.WriteLine("Created Category: {0}", domainEvent.Name);
            return Task.CompletedTask;
        }

    }
}
