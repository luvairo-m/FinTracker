using MediatR;

namespace FinTracker.Logic.Handlers.Category.DeleteCategory;

public class DeleteCategoryCommand : IRequest
{
    public DeleteCategoryCommand(Guid categoryId)
    {
        CategoryId = categoryId;
    }

    public Guid CategoryId { get; set; }
}