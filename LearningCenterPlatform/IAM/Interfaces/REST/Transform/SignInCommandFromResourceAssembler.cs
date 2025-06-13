using LearningCenterPlatform.IAM.Domain.Model.Commands;
using LearningCenterPlatform.IAM.Interfaces.REST.Resources;

namespace LearningCenterPlatform.IAM.Interfaces.REST.Transform;

public static class SignInCommandFromResourceAssembler
{
    public static SignInCommand ToCommandFromResource(SignInResource resource)
    {
        return new SignInCommand(resource.Username, resource.Password);
    }
}