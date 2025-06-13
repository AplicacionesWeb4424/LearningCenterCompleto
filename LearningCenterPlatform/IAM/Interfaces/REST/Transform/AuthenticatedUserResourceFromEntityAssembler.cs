using LearningCenterPlatform.IAM.Domain.Model.Aggregates;
using LearningCenterPlatform.IAM.Interfaces.REST.Resources;

namespace LearningCenterPlatform.IAM.Interfaces.REST.Transform;

public static class AuthenticatedUserResourceFromEntityAssembler
{
    public static AuthenticatedUserResource ToResourceFromEntity(
        User user, string token)
    {
        return new AuthenticatedUserResource(user.Id, user.Username, token);
    }
}