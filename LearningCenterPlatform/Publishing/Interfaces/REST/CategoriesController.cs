using System.Net.Mime;
using LearningCenterPlatform.Publishing.Domain.Model.Queries;
using LearningCenterPlatform.Publishing.Domain.Services;
using LearningCenterPlatform.Publishing.Interfaces.REST.Resources;
using LearningCenterPlatform.Publishing.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LearningCenterPlatform.Publishing.Interfaces.REST;

/// <summary>
///     The category controller
/// </summary>
/// <param name="categoryCommandService">
///     The <see cref="ICategoryCommandService" /> category command service
/// </param>
/// <param name="categoryQueryService">
///     The <see cref="ICategoryQueryService" /> category query service
/// </param>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Category Endpoints")]
public class CategoriesController(
    ICategoryCommandService categoryCommandService,
    ICategoryQueryService categoryQueryService) : ControllerBase
{
    /// <summary>
    ///     Get category by id
    /// </summary>
    /// <param name="categoryId">
    ///     The category id
    /// </param>
    /// <returns>
    ///     The <see cref="CategoryResource" /> category
    /// </returns>
    [HttpGet("{categoryId:int}")]
    [SwaggerOperation(
        Summary = "Get all categories",
        Description = "Get all categories",
        OperationId = "GetAllCategories")]
    [SwaggerResponse(StatusCodes.Status200OK, "The list of categories", typeof(CategoryResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "No categories found")]
    public async Task<IActionResult> GetCategoryById(int categoryId)
    {
        var getCategoryByIdQuery = new GetCategoryByIdQuery(categoryId);
        var category = await categoryQueryService.Handle(getCategoryByIdQuery);
        if (category is null) return NotFound();
        var resource = CategoryResourceFromEntityAssembler.ToResourceFromEntity(category);
        return Ok(resource);
    }

    /// <summary>
    ///     Create a new category
    /// </summary>
    /// <param name="resource">
    ///     The <see cref="CreateCategoryResource" /> category resource
    /// </param>
    /// <returns>
    ///     The <see cref="CategoryResource" /> category created, or a bad request if the category could not be created
    /// </returns>
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new category",
        Description = "Create a new category",
        OperationId = "CreateCategory")]
    [SwaggerResponse(StatusCodes.Status201Created, "The category was created", typeof(CategoryResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The category could not be created")]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryResource resource)
    {
        var createCategoryCommand = CreateCategoryCommandFromResourceAssembler.ToCommandFromResource(resource);
        var category = await categoryCommandService.Handle(createCategoryCommand);
        if (category is null) return BadRequest();
        var categoryResource = CategoryResourceFromEntityAssembler.ToResourceFromEntity(category);
        return CreatedAtAction(nameof(GetCategoryById), new { categoryId = category.Id }, categoryResource);
    }

    [HttpPut]
    [SwaggerOperation(
        Summary = "Update a category",
        Description = "Update a category",
        OperationId = "UpdateCategory")]
    [SwaggerResponse(StatusCodes.Status201Created, "The category was updated", typeof(CategoryResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The category could not be updated")]
    public async Task<IActionResult> UpdateCategory( [FromBody] UpdateCategoryResource resource)
    {
        var updateCategoryCommand = UpdateCategoryCommandFromResourceAssembler.ToCommandFromResource(resource);
        var category = await categoryCommandService.Handle(updateCategoryCommand);
        if (category is null) return NotFound();
        var categoryResource = CategoryResourceFromEntityAssembler.ToResourceFromEntity(category);
        return Ok(categoryResource);
    }
    [HttpDelete]
    [SwaggerOperation(
        Summary = "Delete a category",
        Description = "Detele a category",
        OperationId = "DeleteCategory")]
    [SwaggerResponse(StatusCodes.Status201Created, "The category was deleted", typeof(CategoryResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The category could not be Deleted")]
    public async Task<IActionResult> DeleteCategory([FromBody] DeleteCategoryResource resource)
    {
        var deleteCategoryCommand = DeleteCategoryCommandFromResourceAssembler.ToCommandFromResource(resource);
        var category = await categoryCommandService.Handle(deleteCategoryCommand);
        if (category is null) return NotFound();
        var categoryResource = CategoryResourceFromEntityAssembler.ToResourceFromEntity(category);
        return Ok(categoryResource);
    }

    /// <summary>
    ///     Get all categories
    /// </summary>
    /// <returns>
    ///     The list of <see cref="CategoryResource" /> categories
    /// </returns>
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all categories",
        Description = "Get all categories",
        OperationId = "GetAllCategories")]
    [SwaggerResponse(StatusCodes.Status200OK, "The list of categories", typeof(IEnumerable<CategoryResource>))]
    public async Task<IActionResult> GetAllCategories()
    {
        var categories = await categoryQueryService.Handle(new GetAllCategoriesQuery());
        var categoryResources = categories.Select(CategoryResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(categoryResources);
    }
}