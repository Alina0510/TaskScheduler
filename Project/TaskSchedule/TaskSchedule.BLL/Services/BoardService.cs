using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSchedule.DAL.Models;
using TaskSchedule.DAL;
using Microsoft.EntityFrameworkCore;
using TaskSchedule.BLL.Models;
using log4net.Core;

namespace TaskSchedule.BLL.Services
{
    public class BoardService
    {
        private ApplicationContext _context;
        private ILogger _logger;
        public BoardService(ApplicationContext context, ILogger logger)
        {
            _logger = logger;
            _context = context;

        }
        public async Task<List<MyBoardsListVM>> GetBoardForUser(int userId)
        {
            return _context.UserBoardRoles.Include(i => i.Board).Include(i => i.Role).Where(i => i.UserId == userId).Select(i => new MyBoardsListVM(i.BoardId.Value, i.Board.Name, i.Role.Name)).ToList();
        }
        public async  Task<Board> GetBoardById(int? boardId)
        {
            return _context.Boards.First(i => i.Id == boardId);
        }
        public async Task CreateBoard(string name, string description, int userId)
        {
            Board board = new Board() { Description = description, Name = name, OwnerId = userId };
            await _context.Boards.AddAsync(board);
            await _context.SaveChangesAsync();
            UserBoardRole role = new UserBoardRole() { BoardId = board.Id, RoleId = _context.Roles.Where(i => i.Name == "Owner").First().Id, UserId = userId };
            await _context.UserBoardRoles.AddAsync(role);
            await _context.SaveChangesAsync();
        }
    }
}
