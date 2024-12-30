using My_site.DAL.Entities;
using My_site.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_site.Services.Services
{
    public class UserActionLogger : IUserActionLogger
    {
        private readonly IUserActionLogRepository _logRepository;

        public UserActionLogger(IUserActionLogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task LogAsync(string username, string action, string controller, string method, string details = null)
        {
            var log = new UserActionLog
            {
                Username = username,
                Action = action,
                Controller = controller,
                Method = method,
                Timestamp = DateTime.UtcNow,
                Details = details
            };

            await _logRepository.LogActionAsync(log);
        }
        public async Task<Dictionary<string, int>> GetUserActionCounts()
        {
            return await _logRepository.GetUserActionCounts();
        }
    }
}
