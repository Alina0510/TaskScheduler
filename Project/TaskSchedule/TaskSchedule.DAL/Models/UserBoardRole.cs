using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedule.DAL.Models
{
    public class UserBoardRole
    {
        public int Id { get; set; }
        public int? UserId { get; set; } 
        public User? User { get; set; }
        public int? BoardId { get; set; }
        public Board? Board { get; set; }
        public int? RoleId { get; set; } 
        public Role? Role { get; set; }
    }
}
