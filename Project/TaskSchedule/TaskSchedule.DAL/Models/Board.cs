using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedule.DAL.Models
{
    public class Board
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int OwnerId { get; set; }
        public User? Owner { get; set; }
        public ICollection<BoardTask> BoardTasks { get; set; }
        public ICollection<UserBoardRole> UserBoardRoles { get; set; }
    }
}
