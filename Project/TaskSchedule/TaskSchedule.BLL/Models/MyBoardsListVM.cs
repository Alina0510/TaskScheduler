using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedule.BLL.Models
{
    public class MyBoardsListVM
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Role { get; set; }
        public MyBoardsListVM(int id, string name, string role)
        {
            Id = id;
            Name = name;
            Role = role;
        }
    }
}
