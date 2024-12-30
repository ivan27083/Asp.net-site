using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_site.DAL.Entities
{
    public class UserActionLog
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string Method { get; set; }
        public DateTime Timestamp { get; set; }
        public string Details { get; set; }
    }
}
