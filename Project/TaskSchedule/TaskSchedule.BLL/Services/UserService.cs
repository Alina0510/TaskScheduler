using log4net.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSchedule.BLL.Models;
using TaskSchedule.DAL;
using TaskSchedule.DAL.Models;

namespace TaskSchedule.BLL.Services
{
    public class UserService
    {
        private ApplicationContext _context;
        private ILogger _logger;
        public UserService(ApplicationContext context, ILogger logger)
        {
            _logger = logger;
            _context = context;

        }
        public async Task<User> CreateUser( string name, string email, string password)
        {
            User newUser = new User { Name = name, Email = email, Password = password };
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return newUser;
        }
        public async Task<List<UserComboboxVM>> GetUsers()
        {
            return _context.Users.Select(i => new UserComboboxVM() { Id = i.Id, Name = i.Name }).ToList();
        }
        public async Task<Role> GetUserRole( int userId, int boardId)
        {
            return _context.UserBoardRoles.Include(i => i.Role).Where(i => i.UserId == userId && i.BoardId == boardId).First().Role;
        }
        public async Task<User?> LoginUser( string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(i => i.Email == email);
            if (user == null || user.Password != password)
            {
                return null;
            }
            return user;
        }
    }
}
