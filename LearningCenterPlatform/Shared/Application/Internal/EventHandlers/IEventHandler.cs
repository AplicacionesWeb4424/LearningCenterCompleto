using Cortex.Mediator.Notifications;
using LearningCenterPlatform.Shared.Domain.Model.Events;

namespace LearningCenterPlatform.Shared.Application.Internal.EventHandlers
{
    public interface IEventHandler<in TEvent> : INotificationHandler <TEvent> where TEvent : IEvent
    {
    }
}
