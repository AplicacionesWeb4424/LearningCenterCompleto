using LearningCenterPlatform.Publishing.Domain.Model.Commands;
using LearningCenterPlatform.Publishing.Interfaces.REST.Resources;

namespace LearningCenterPlatform.Publishing.Interfaces.REST.Transform
{
    public static class DeleteTutorialCommandFromResourceAssembler
    {
        public static DeleteTutorialCommand ToCommandFromResource(DeleteTutorialResource resource)
        {
            return new DeleteTutorialCommand(resource.Id);
        }
    }
}
