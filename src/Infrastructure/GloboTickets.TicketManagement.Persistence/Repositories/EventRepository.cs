using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GloboTickets.TicketManagement.Persistence.Repositories;

public class EventRepository : BaseRepository<Event>, IEventRepository
{
    public EventRepository(GloboTicketDbContext _dbContext) : base(_dbContext)
    {
    }
    
    public Task<bool> IsEventNameAndDateUnique(string name, DateTime date)
    {
        return _dbContext.Events
            .AnyAsync(e => e.Name.Equals(name) && e.Date.Date.Equals(date.Date));
        
    }
}