using MediatR;

namespace GloboTicket.TicketManagement.Application.Features.Categories.Command.CreateCategory
{
    public class CreateCategoryCommand: IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;
    }
}
