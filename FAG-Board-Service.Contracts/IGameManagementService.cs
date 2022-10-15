using System.Net;
using FAG_Board_Service.Models;

namespace FAG_Board_Service.Contracts;

public interface IGameManagementService
{
    public Task<string> CreateNewGameAsync(NewGameInfo gameInfo);
    public Task DeleteGameAsync(DeleteGameInfo gameInfo);
    
}