using LearningCenterPlatform.IAM.Domain.Model.Aggregates;
using LearningCenterPlatform.IAM.Interfaces.REST.Resources;

namespace LearningCenterPlatform.IAM.Interfaces.REST.Transform;

public static class UserResourceFromEntityAssembler
{
    public static UserResource ToResourceFromEntity(User user)
    {
        return new UserResource(user.Id, user.Username);
    }
}