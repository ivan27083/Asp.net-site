using Microsoft.EntityFrameworkCore;
using My_site.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_site.DAL.Repositories
{
    public class UserActionLogRepository : IUserActionLogRepository
    {
        private readonly ApplicationContext _context;

        public UserActionLogRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task LogActionAsync(UserActionLog log)
        {
            await _context.UserActionLogs.AddAsync(log);
            await _context.SaveChangesAsync();
        }
        public async Task<IQueryable<UserActionLog>> GetAll()
        {
            return _context.UserActionLogs.AsNoTracking();
        }

        public async Task<Dictionary<string, int>> GetUserActionCounts()
        {
            return await _context.UserActionLogs
                .AsNoTracking()
                .GroupBy(log => log.Username)
                .Select(group => new { Username = group.Key, Count = group.Count() })
                .ToDictionaryAsync(g => g.Username, g => g.Count);
        }
    }
}
