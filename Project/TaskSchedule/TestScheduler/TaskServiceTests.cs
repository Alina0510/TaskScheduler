using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskSchedule.BLL.Models;
using TaskSchedule.BLL.Services;
using TaskSchedule.DAL;
using TaskSchedule.DAL.Models;
using Xunit;

public class TaskServiceTests
{
    private TaskService _taskService;
    private ApplicationContext _context;

    private DbContextOptions<ApplicationContext> CreateNewContextOptions()
    {
        return new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    private void SeedDatabase(ApplicationContext context)
    {
        var boards = new List<Board>
    {
        new Board { Id = 1, Name = "Board 1", Description = "Description 1", OwnerId = 1 },
        new Board { Id = 2, Name = "Board 2", Description = "Description 2", OwnerId = 2 }
    };

        var roles = new List<Role>
    {
        new Role { Id = 1, Name = "Owner" },
        new Role { Id = 2, Name = "Member" }
    };

        var users = new List<User>
    {
        new User { Id = 1, UserName = "User1", Name = "User One", Password = "Password1" },
        new User { Id = 2, UserName = "User2", Name = "User Two", Password = "Password2" }
    };

        var userBoardRoles = new List<UserBoardRole>
    {
        new UserBoardRole { BoardId = 1, UserId = 1, RoleId = 1 },
        new UserBoardRole { BoardId = 2, UserId = 2, RoleId = 2 }
    };

        var tasks = new List<BoardTask>
    {
        new BoardTask { Id = 1, Title = "Task 1", Description = "Description 1", BoardId = 1, AssignedUserId = 1, Deadline = DateTime.Now, Status = "ToDo" },
        new BoardTask { Id = 2, Title = "Task 2", Description = "Description 2", BoardId = 1, AssignedUserId = 1, Deadline = DateTime.Now, Status = "InReview" },
        new BoardTask { Id = 3, Title = "Task 3", Description = "Description 3", BoardId = 2, AssignedUserId = 2, Deadline = DateTime.Now, Status = "Done" }
    };

        context.Boards.AddRange(boards);
        context.Roles.AddRange(roles);
        context.Users.AddRange(users);
        context.UserBoardRoles.AddRange(userBoardRoles);
        context.BoardTasks.AddRange(tasks);

        context.SaveChanges();
    }


    [Fact]
    public async Task CreateTask_ShouldAddTaskToDatabase()
    {
        var options = CreateNewContextOptions();
        using (var context = new ApplicationContext(options))
        {
            _taskService = new TaskService(context);

            await _taskService.CreateTask("New Task", "New Description", DateTime.Now, 1, 1);

            var task = context.BoardTasks.FirstOrDefault(t => t.Title == "New Task");
            Assert.NotNull(task);
            Assert.Equal("New Description", task.Description);
            Assert.Equal(1, task.AssignedUserId);
            Assert.Equal(1, task.BoardId);
            Assert.Equal("ToDo", task.Status);
        }
    }

    [Fact]
    public async Task GetTasksByBoardId_ShouldReturnCorrectTasks()
    {
        var options = CreateNewContextOptions();
        using (var context = new ApplicationContext(options))
        {
            SeedDatabase(context);
            _taskService = new TaskService(context);

            var (toDo, inReview, done) = await _taskService.GetTasksByBoardId(1);

            Assert.Single(toDo);
            Assert.Equal("Task 1", toDo[0].Title);

            Assert.Single(inReview);
            Assert.Equal("Task 2", inReview[0].Title);

            Assert.Empty(done);
        }
    }

    [Fact]
    public async Task GetMyTasksByUserId_ShouldReturnCorrectTasks()
    {
        var options = CreateNewContextOptions();
        using (var context = new ApplicationContext(options))
        {
            SeedDatabase(context);
            _taskService = new TaskService(context);

            var tasks = await _taskService.GetMyTasksByUserId(1);

            Assert.Equal(2, tasks.Count);
            Assert.Equal("Task 1", tasks[0].Title);
            Assert.Equal("Task 2", tasks[1].Title);
        }
    }

    [Fact]
    public async Task GetTaskById_ShouldReturnCorrectTask()
    {
        var options = CreateNewContextOptions();
        using (var context = new ApplicationContext(options))
        {
            SeedDatabase(context);
            _taskService = new TaskService(context);

            var task = await _taskService.GetTaskById(1);

            Assert.NotNull(task);
            Assert.Equal("Task 1", task.Title);
        }
    }

    [Fact]
    public async Task SaveTask_ShouldUpdateTaskInDatabase()
    {
        var options = CreateNewContextOptions();
        using (var context = new ApplicationContext(options))
        {
            SeedDatabase(context);
            _taskService = new TaskService(context);

            var task = await _taskService.GetTaskById(1);
            task.Title = "Updated Task";
            await _taskService.SaveTask(task);

            var updatedTask = await _taskService.GetTaskById(1);
            Assert.Equal("Updated Task", updatedTask.Title);
        }
    }
}
