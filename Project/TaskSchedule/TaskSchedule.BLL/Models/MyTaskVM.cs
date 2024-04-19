using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSchedule.DAL.Models;

namespace TaskSchedule.BLL.Models
{
    public class MyTaskVM
    {
        public int Id { get; set; }
        public int BoardId { get; set; }
        public string Title { get; set; }
        public string BoardName { get; set; }
        public string Status { get; set; }
        public DateTime Deadline { get; set; }
        public Role TaskRole { get; set; }
        public BoardTask BoardTask { get; set; }
    }
}
