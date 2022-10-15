using System.Net;
using FAG_Board_Service.Contracts;
using FAG_Board_Service.Exceptions;
using FAG_Board_Service.Models;
using Microsoft.EntityFrameworkCore;

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

    public async Task<GameInfo?> GetGameAsync(string gameToken)
    {
        return await _context.GameInfos
            .Include(x => x.Board)
            .ThenInclude(x => x.X)
            .ThenInclude(x => x.Y)
            .FirstOrDefaultAsync(item => item.GameToken == gameToken);
    }
    
    public async Task UpdateAsync(GameInfo gameInfo)
    {
        _context.GameInfos.Update(gameInfo);
        await _context.SaveChangesAsync();
    }
}