using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskSchedule.BLL.Services;
using TaskSchedule.DAL;
using TaskSchedule.DAL.Models;
using Xunit;

public class BoardServiceTests
{
    private BoardService _boardService;
    private ApplicationContext _context;

    private DbContextOptions<ApplicationContext> CreateNewContextOptions()
    {
        var options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        return options;
    }

    private void SeedDatabase(ApplicationContext context)
    {
        var roles = new List<Role>
        {
            new Role { Id = 1, Name = "Owner" },
            new Role { Id = 2, Name = "Member" }
        };

        var boards = new List<Board>
        {
            new Board { Id = 1, Name = "Board 1", Description = "Description 1", OwnerId = 1 },
            new Board { Id = 2, Name = "Board 2", Description = "Description 2", OwnerId = 2 }
        };

        var userBoardRoles = new List<UserBoardRole>
        {
            new UserBoardRole { BoardId = 1, UserId = 1, RoleId = 1 },
            new UserBoardRole { BoardId = 2, UserId = 2, RoleId = 2 }
        };

        context.Roles.AddRange(roles);
        context.Boards.AddRange(boards);
        context.UserBoardRoles.AddRange(userBoardRoles);

        context.SaveChanges();
    }

    [Fact]
    public async Task GetBoardForUser_ShouldReturnCorrectBoards()
    {
        var options = CreateNewContextOptions();
        using (var context = new ApplicationContext(options))
        {
            SeedDatabase(context);
            _boardService = new BoardService(context);

            var result = await _boardService.GetBoardForUser(1);

            Assert.Single(result);
            Assert.Equal(1, result[0].Id);
            Assert.Equal("Board 1", result[0].Name);
            Assert.Equal("Owner", result[0].Role);
        }
    }

    [Fact]
    public async Task GetBoardById_ShouldReturnCorrectBoard()
    {
        var options = CreateNewContextOptions();
        using (var context = new ApplicationContext(options))
        {
            SeedDatabase(context);
            _boardService = new BoardService(context);

            var result = await _boardService.GetBoardById(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Board 1", result.Name);
        }
    }

    [Fact]
    public async Task CreateBoard_ShouldCreateNewBoard()
    {
        var options = CreateNewContextOptions();
        using (var context = new ApplicationContext(options))
        {
            SeedDatabase(context);
            _boardService = new BoardService(context);

            await _boardService.CreateBoard("New Board", "New Description", 3);

            var board = context.Boards.FirstOrDefault(b => b.Name == "New Board");
            var userBoardRole = context.UserBoardRoles.FirstOrDefault(ubr => ubr.UserId == 3);

            Assert.NotNull(board);
            Assert.Equal("New Board", board.Name);
            Assert.Equal("New Description", board.Description);
            Assert.Equal(3, board.OwnerId);

            Assert.NotNull(userBoardRole);
            Assert.Equal(board.Id, userBoardRole.BoardId);
            Assert.Equal(1, userBoardRole.RoleId);
        }
    }
}
