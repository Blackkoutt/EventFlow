﻿using EventFlowAPI.DB.Context;
using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.Repositories.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventFlowAPI.Logic.Repositories.Repositories
{
    public sealed class EventRepository(APIContext context) : GenericRepository<Event>(context), IEventRepository
    {
        public override sealed async Task<IEnumerable<Event>> GetAllAsync(Func<IQueryable<Event>, IQueryable<Event>>? query = null)
        {
            var _table = _context.Event
                        .Include(e => e.Hall)
                        .Include(e => e.Category)
                        .Include(e => e.Details)
                        .AsSplitQuery();

            return await (query != null ? query(_table).ToListAsync() : _table.ToListAsync());
        }
        public override sealed async Task<Event?> GetOneAsync(int id)
        {
            return await _context.Event
                        .AsSplitQuery()
                        .Include(e => e.Hall)
                        .Include(e => e.Category)
                        .Include(e => e.Details)
                        .Include(e => e.Tickets)
                        .ThenInclude(t => t.TicketType)
                        .FirstOrDefaultAsync(e=> e.Id == id);
        }
    }
}
