using FAG_Board_Service.Models;
using Microsoft.EntityFrameworkCore;

namespace FAG_Board_Service.Database;

public class GameContext : DbContext
{
    public GameContext(DbContextOptions<GameContext> options) : base(options)
    {
        this.Database.EnsureCreated();
    }
    
    public DbSet<GameInfo> GameInfos { get; set; }
}