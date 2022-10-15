using FAG_Board_Service.Contracts;
using FAG_Board_Service.Exceptions;
using FAG_Board_Service.Models;
using Microsoft.AspNetCore.Mvc;

namespace FAG_Board_Service.Controllers;

[ApiController]
[Route("PlayGame")]
public class PlayGameController : ControllerBase
{
    private readonly IPlayGameService _service;

    public PlayGameController(IPlayGameService service)
    {
        _service = service;
    }

    [HttpGet("GameStatus")]
    public async Task<IActionResult> GetGameStatus([FromQuery]string gameToken)
    {
        try
        {
            var result = await _service.GetStatusOfGameAsync(gameToken);
            return Ok(result);
        }
        catch(HttpStatusException e)
        {
            return StatusCode((int)e.StatusCode, e.Message);
        }
    }
    
    [HttpPost("VisitTileOnBoard")]
    public async Task<IActionResult> VisitTileOnBoard(VisitRequest gameToken)
    {
        try
        {
            var result = await _service.VisitTileOnBoardAsync(gameToken);
            return Ok(result);
        }
        catch(HttpStatusException e)
        {
            return StatusCode((int)e.StatusCode, e.Message);
        }
    }
}