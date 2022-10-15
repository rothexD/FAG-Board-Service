using System.Net;
using FAG_Board_Service.Contracts;
using FAG_Board_Service.Exceptions;
using FAG_Board_Service.Models;

namespace FAG_Board_Service.Services;

public class PlayGameService : IPlayGameService
{
    private readonly IGameDatabaseAccess _dbAccess;

    public PlayGameService(IGameDatabaseAccess dbAccess)
    {
        _dbAccess = dbAccess;
    }

    public async Task<GameStatus> GetStatusOfGameAsync(VisitRequest visitRequest)
    {
        var game = await _dbAccess.GetGameAsync(visitRequest.GameToken);
        if (game is null)
        {
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
            throw new HttpStatusException(HttpStatusCode.NotFound, "could not find matching game to token");
        }
        if (game.Board.RemainingItems == 0)
        {
            return new GameStatus()
            {
                TilesWithItemsLeftToVisit = 0
            };
        }
        if (visitRequest.X > game.Board.Size ||visitRequest.X < 0 )
        {
            throw new HttpStatusException(HttpStatusCode.BadRequest, "X is out of bounds");
        }
        if (visitRequest.Y > game.Board.Size || visitRequest.Y < 0)
        {
            throw new HttpStatusException(HttpStatusCode.BadRequest, "Y is out of bounds");
        }

        var returnStatus = new GameStatus
        {
            BoardSize = game.Board.Size
        };
        
        if (game.Board.Board[visitRequest.X][visitRequest.Y] == GameTile.Coin)
        {
            game.Board.Board[visitRequest.X][visitRequest.Y] = GameTile.Path;
            game.Board.RemainingItems--;
            
            await _dbAccess.UpdateAsync(game);
            returnStatus.TilesWithItemsLeftToVisit = game.Board.RemainingItems;
        }
        return returnStatus;
    }
}