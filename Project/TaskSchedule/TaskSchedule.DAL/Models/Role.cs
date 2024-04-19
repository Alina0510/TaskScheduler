using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedule.DAL.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AccessLevelEnum TaskRead { get; set; }
        public AccessLevelEnum TaskWrite { get; set; }
        public AccessLevelEnum TaskDelete { get; set; }
        public ICollection<UserBoardRole> UserBoardRoles { get; set; }
    }
}
