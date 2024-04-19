using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedule.DAL.Models
{
    public class BoardTask
    {
        public int Id { get; set; }
        public string Title { get; set; } 
        public string? Description { get; set; }
        public string Status { get; set; }
        public DateTime Deadline { get; set; }
        public int BoardId { get; set; }
        public Board? Board { get; set; }
        public int? AssignedUserId { get; set; }
        public User? AssignedUser { get; set; }
    }
}
