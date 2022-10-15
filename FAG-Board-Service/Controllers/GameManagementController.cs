using FAG_Board_Service.Contracts;
using FAG_Board_Service.Exceptions;
using FAG_Board_Service.Models;
using Microsoft.AspNetCore.Mvc;

namespace FAG_Board_Service.Controllers;

[ApiController]
[Route("GameManagement")]
public class GameManagement : ControllerBase
{
    private readonly IGameManagementService _service;

    public GameManagement(IGameManagementService service)
    {
        _service = service;
    }

    [HttpPost("CreateGame")]
    public async Task<IActionResult> StartGameForPlayer(NewGameInfo newGameInfo)
    {
        try
        {
            var result = await _service.CreateNewGameAsync(newGameInfo);
            return Ok(result);
        }
        catch (HttpStatusException e)
        {
            return StatusCode((int) e.StatusCode, e.Message);
        }
    }

    [HttpDelete("RemoveGame")]
    public async Task<IActionResult> DeleteGameForPlayer(DeleteGameInfo deleteGameInfo)
    {
        try
        {
            await _service.DeleteGameAsync(deleteGameInfo);
            return Ok();
        }
        catch (HttpStatusException e)
        {
            return StatusCode((int) e.StatusCode, e.Message);
        }
    }
}