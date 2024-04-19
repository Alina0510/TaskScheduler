using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSchedule.BLL.Models;
using TaskSchedule.DAL.Models;
using TaskSchedule.DAL;
using Microsoft.EntityFrameworkCore;

namespace TaskSchedule.BLL.Services
{
    public class TaskService
    {
        private ApplicationContext _context;
        public TaskService(ApplicationContext context)
        {

            _context = context;

        }
        public async Task CreateTask(string name, string description, DateTime dateTime, int userId, int boardId)
        {
            await _context.BoardTasks.AddAsync(new BoardTask() { AssignedUserId = userId, BoardId = boardId, Deadline = dateTime, Description = description, Status = "ToDo", Title = name });
            await _context.SaveChangesAsync();
        }
        public async ValueTask<(List<BoardTask>, List<BoardTask>, List<BoardTask>)> GetTasksByBoardId(int? boardId)
        {
            var list = _context.BoardTasks.Where(i => i.BoardId == boardId).ToList();
            return new(list.Where(i => i.Status == "ToDo").ToList(), list.Where(i => i.Status == "InReview").ToList(), list.Where(i => i.Status == "Done").ToList());
        }
        public async ValueTask<List<MyTaskVM>> GetMyTasksByUserId(int? userId)
        {
            var boardTasks = _context.BoardTasks.Include(i => i.Board).Include(i => i.AssignedUser).Where(i => i.AssignedUserId == userId).ToList();
            var userBoardRoles = _context.UserBoardRoles.Include(i => i.Role).Where(i => i.UserId == userId).ToList();
            List<MyTaskVM> result = new List<MyTaskVM> ();
            foreach (var task in boardTasks)
            {
                result.Add(new MyTaskVM()
                {
                    Id = task.Id,
                    BoardId = task.BoardId,
                    BoardName = task.Board.Name,
                    BoardTask = task,
                    Deadline = task.Deadline,
                    Status = task.Status,
                    Title = task.Title,
                    TaskRole = userBoardRoles.Where(i => i.BoardId == task.BoardId).First().Role
                });
            }
            return result;
        }
        public async Task<BoardTask> GetTaskById(int? taskId)
        {
            return _context.BoardTasks.First(i => i.Id == taskId);
        }
        public async ValueTask SaveTask( BoardTask task)
        {
            _context.Update(task);
            _context.SaveChanges();
        }
        public async ValueTask DeleteTask(int taskId)
        {
            _context.BoardTasks.Remove(_context.BoardTasks.First(i => i.Id == taskId));
            _context.SaveChanges();
        }
    }
}
