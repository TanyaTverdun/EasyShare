namespace EasyShare.Application.Features.Categories.Commands.CreateCategory;

public record CreateCategoryRequest
{
    public required string Name { get; init; }

    public CreateCategoryCommand ToCommand() =>
        new CreateCategoryCommand { Name = this.Name };
}
