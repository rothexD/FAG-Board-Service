using System.Net;
using FAG_Board_Service.Contracts;
using FAG_Board_Service.Exceptions;
using FAG_Board_Service.Models;
using Microsoft.Extensions.Logging;

namespace FAG_Board_Service.Services;

public class PlayGameService : IPlayGameService
{
    private readonly IGameDatabaseAccess _dbAccess;
    private readonly ILogger<PlayGameService> _logger;
    public PlayGameService(IGameDatabaseAccess dbAccess, ILogger<PlayGameService> logger)
    {
        _dbAccess = dbAccess;
        _logger = logger;
    }

    public async Task<GameStatus> GetStatusOfGameAsync(string gameToken)
    {
        var game = await _dbAccess.GetGameAsync(gameToken);
        if (game is null)
        {
            _logger.LogError($"could not get status for \"{gameToken}\", game was not found");
            throw new HttpStatusException(HttpStatusCode.NotFound, "could not find matching game to token");
        }
        return new GameStatus()
        {
            TilesWithItemsLeftToVisit = game.Board.RemainingItems,
            BoardSize = game.Board.Size
        };
    }
    
    
    public async Task<GameStatus> VisitTileOnBoardAsync(VisitRequest visitRequest)
    {
        var game = await _dbAccess.GetGameAsync(visitRequest.GameToken);
        if (game is null)
        {
            _logger.LogError($"could not get game for \"{visitRequest.GameToken}\", game was not found");
            throw new HttpStatusException(HttpStatusCode.NotFound, "could not find matching game to token");
        }
        if (game.Board.RemainingItems == 0)
        {
            return new GameStatus()
            {
                TilesWithItemsLeftToVisit = game.Board.RemainingItems
            };
        }
        if (visitRequest.X >= game.Board.Size ||visitRequest.X < 0 )
        {
            _logger.LogError($"visit request for \"{visitRequest.GameToken}\", tried to go out of bounds X with {visitRequest.X}");
            throw new HttpStatusException(HttpStatusCode.BadRequest, "X is out of bounds");
        }
        if (visitRequest.Y >= game.Board.Size || visitRequest.Y < 0)
        {
            _logger.LogError($"visit request for \"{visitRequest.GameToken}\", tried to go out of bounds Y with {visitRequest.X}");
            throw new HttpStatusException(HttpStatusCode.BadRequest, "Y is out of bounds");
        }

        var returnStatus = new GameStatus
        {
            BoardSize = game.Board.Size,
            TilesWithItemsLeftToVisit = game.Board.RemainingItems
        };
        
        if (game.Board.X[visitRequest.X].Y[visitRequest.Y].TileInfo == GameTile.Coin)
        {
            _logger.LogInformation($"visit request for \"{visitRequest.GameToken}\" with X:{visitRequest.X} and Y:{visitRequest.Y} found a coin");
            game.Board.X[visitRequest.X].Y[visitRequest.Y].TileInfo = GameTile.Path;
            game.Board.RemainingItems--;
            
            await _dbAccess.UpdateAsync(game);
            returnStatus.TilesWithItemsLeftToVisit = game.Board.RemainingItems;
        }
        
        _logger.LogInformation($"visit request returned");
        return returnStatus;
    }
}