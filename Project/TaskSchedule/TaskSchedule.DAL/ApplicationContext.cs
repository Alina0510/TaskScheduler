using Microsoft.EntityFrameworkCore;
using TaskSchedule.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TaskSchedule.DAL
{
    public class ApplicationContext : IdentityDbContext
    {
        public DbSet<Board> Boards { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<BoardTask> BoardTasks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserBoardRole> UserBoardRoles { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }
    }
}
