using FAG_Board_Service.Models;

namespace FAG_Board_Service.Contracts;

public interface IPlayGameService
{
    public Task<GameStatus> GetStatusOfGameAsync(string gameToken);
    public Task<GameStatus> VisitTileOnBoardAsync(VisitRequest visitRequest);
}