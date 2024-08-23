using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Domain.Entities;

namespace GloboTickets.TicketManagement.Persistence.Repositories;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(GloboTicketDbContext dbContext) : base(dbContext)
    {
    }
}