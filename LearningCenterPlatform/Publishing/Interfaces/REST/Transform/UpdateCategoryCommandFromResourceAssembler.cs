using LearningCenterPlatform.Publishing.Domain.Model.Commands;
using LearningCenterPlatform.Publishing.Interfaces.REST.Resources;
namespace LearningCenterPlatform.Publishing.Interfaces.REST.Transform
{
    public static  class UpdateCategoryCommandFromResourceAssembler
    {
        public static UpdateCategoryCommand ToCommandFromResource(UpdateCategoryResource resource)
        {
            return new UpdateCategoryCommand(resource.Id, resource.Name);
        }
    }
}
