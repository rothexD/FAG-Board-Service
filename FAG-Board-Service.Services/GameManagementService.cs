using System.Net;
using System.Web.Http;
using FAG_Board_Service.Contracts;
using FAG_Board_Service.Models;
using Microsoft.Extensions.Logging;

namespace FAG_Board_Service.Services;

public class GameManagementService : IGameManagementService
{
    private readonly IGameDatabaseAccess _dbAccess;
    private readonly ILogger<GameManagementService> _logger;
    public GameManagementService(IGameDatabaseAccess dbAccess, ILogger<GameManagementService> logger)
    {
        this._dbAccess = dbAccess;
        _logger = logger;
    }

    public async Task<string> CreateNewGameAsync(NewGameInfo gameInfo)
    {
        var newGame = new GameInfo()
        {
            GameToken = Guid.NewGuid() + DateTime.UtcNow.Ticks.ToString(),
            Board = GameBoard.CreateGameBoard(gameInfo.BoardSize)
        };
        await this._dbAccess.CreateNewGameAsync(newGame);
        
        _logger.LogInformation($"Created game {newGame.GameToken}");
        return newGame.GameToken;
    }
    
    
    public async Task DeleteGameAsync(DeleteGameInfo gameInfo)
    {
       var game = await _dbAccess.GetGameAsync(gameInfo.GameToken);
       if (game is null)
       {
           return;
       }
       _logger.LogInformation($"Deleted game {gameInfo.GameToken}");
       await _dbAccess.DeleteGameAsync(game);
    }
}