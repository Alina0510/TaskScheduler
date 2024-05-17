using Microsoft.EntityFrameworkCore;
using TaskSchedule.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TaskSchedule.DAL
{
    public class ApplicationContext : IdentityDbContext
    {
        public virtual DbSet<Board> Boards { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<BoardTask> BoardTasks { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserBoardRole> UserBoardRoles { get; set; }
        public ApplicationContext()
        {

        }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }
    }
}
