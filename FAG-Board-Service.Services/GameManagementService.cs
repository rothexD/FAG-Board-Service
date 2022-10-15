using System.Net;
using System.Web.Http;
using FAG_Board_Service.Contracts;
using FAG_Board_Service.Models;

namespace FAG_Board_Service.Services;

public class GameManagementService : IGameManagementService
{
    private readonly IGameDatabaseAccess dbAccess;
    
    public GameManagementService(IGameDatabaseAccess dbAccess)
    {
        this.dbAccess = dbAccess;
    }

    public async Task<string> CreateNewGameAsync(NewGameInfo gameInfo)
    {
        var newGame = new GameInfo()
        {
            GameToken = Guid.NewGuid() + DateTime.UtcNow.Ticks.ToString(),
            Board = GameBoard.CreateGameBoard(gameInfo.BoardSize)
        };
        await this.dbAccess.CreateNewGameAsync(newGame);
        
        Console.WriteLine($"Created game {newGame.GameToken}");
        return newGame.GameToken;
    }
    
    
    public async Task DeleteGameAsync(DeleteGameInfo gameInfo)
    {
       var game = await dbAccess.GetGameAsync(gameInfo.GameToken);
       if (game is null)
       {
           return;
       }
       Console.WriteLine($"Deleted game {gameInfo.GameToken}");
       await dbAccess.DeleteGameAsync(game);
    }
}