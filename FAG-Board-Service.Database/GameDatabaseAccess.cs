using FAG_Board_Service.Contracts;
using FAG_Board_Service.Models;

namespace FAG_Board_Service.Database;

public class GameDatabaseAccess : IGameDatabaseAccess
{
    public readonly GameContext _context;
    
    public GameDatabaseAccess(GameContext context)
    {
        _context = context;
    }
    
    public async Task<int> CreateNewGameAsync(GameInfo gameInfo)
    {
        await _context.GameInfos.AddAsync(gameInfo);
        await _context.SaveChangesAsync();
        return gameInfo.Board.Id;
    }

    public async Task DeleteGameAsync(GameInfo gameInfo)
    {
        _context.Remove(gameInfo);
        await _context.SaveChangesAsync();
    }

    public Task<GameInfo?> GetGameAsync(string gameToken)
    {
        return Task.FromResult(_context.GameInfos.Find(gameToken));
    }
    
    public async Task UpdateAsync(GameInfo gameInfo)
    {
        _context.GameInfos.Update(gameInfo);
        await _context.SaveChangesAsync();
    }
}