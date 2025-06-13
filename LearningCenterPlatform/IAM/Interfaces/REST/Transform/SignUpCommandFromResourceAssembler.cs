using LearningCenterPlatform.IAM.Domain.Model.Commands;
using LearningCenterPlatform.IAM.Interfaces.REST.Resources;

namespace LearningCenterPlatform.IAM.Interfaces.REST.Transform;

public static class SignUpCommandFromResourceAssembler
{
    public static SignUpCommand ToCommandFromResource(SignUpResource resource)
    {
        return new SignUpCommand(resource.Username, resource.Password);
    }
}