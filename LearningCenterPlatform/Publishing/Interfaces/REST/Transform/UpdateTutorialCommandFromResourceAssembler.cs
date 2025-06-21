using LearningCenterPlatform.Publishing.Domain.Model.Commands;
using LearningCenterPlatform.Publishing.Interfaces.REST.Resources;

namespace LearningCenterPlatform.Publishing.Interfaces.REST.Transform
{
    public static  class UpdateTutorialCommandFromResourceAssembler
    {
        public static UpdateTutorialCommand ToCommandFromResource(UpdateTutorialResource resource)
        {
            return new UpdateTutorialCommand(resource.Id, resource.Title, resource.Summary, resource.CategoryId);
        }
    }
}
