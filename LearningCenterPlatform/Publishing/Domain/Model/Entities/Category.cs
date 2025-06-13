using LearningCenterPlatform.Publishing.Domain.Model.Commands;

namespace LearningCenterPlatform.Publishing.Domain.Model.Entities;

/// <summary>
///     Represents a category in the ACME Learning Center Platform.
/// </summary>
public class Category
{
    /// <summary>
    ///     Default constructor for the category entity
    /// </summary>
    public Category(UpdateCategoryCommand command)
    {   Id = command.Id;
        Name = command.Name;
    }

    /// <summary>
    ///     Constructor for the category entity
    /// </summary>
    /// <param name="name">
    ///     The name of the category
    /// </param>
    public Category(string name)
    {
        Name = name;
    }

    public Category(CreateCategoryCommand command)
    {
        Name = command.Name;
    }

    public int Id { get; set; }
    public string Name { get; set; }
}