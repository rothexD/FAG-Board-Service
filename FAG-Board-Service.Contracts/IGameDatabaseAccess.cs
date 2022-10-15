using FAG_Board_Service.Models;

namespace FAG_Board_Service.Contracts;

public interface IGameDatabaseAccess
{
    public Task<int> CreateNewGameAsync(GameInfo gameInfo);
    public Task DeleteGameAsync(GameInfo gameInfo);
    public Task<GameInfo?> GetGameAsync(string gameToken);
    public Task UpdateAsync(GameInfo gameInfo);
}