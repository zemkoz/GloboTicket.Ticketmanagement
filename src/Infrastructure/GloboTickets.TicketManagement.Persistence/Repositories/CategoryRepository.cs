using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GloboTickets.TicketManagement.Persistence.Repositories;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(GloboTicketDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Category>> GetCategoriesWithEvents(bool requestIncludeHistory)
    {
        var query = _dbContext.Categories.AsQueryable();
        if (requestIncludeHistory)
        {
            query = query.Include(x => x.Events);
        }

        return await query.ToListAsync();
    }
}