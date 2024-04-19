using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedule.DAL.Models
{
    public class User : IdentityUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<Board> Boards { get; set; }
        public ICollection<UserBoardRole> UserBoardRoles { get; set; }
        public ICollection<BoardTask> BoardTasks { get; set; }
    }
}
