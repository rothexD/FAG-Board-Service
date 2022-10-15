using FAG_Board_Service.Contracts;
using FAG_Board_Service.Exceptions;
using FAG_Board_Service.Models;
using Microsoft.AspNetCore.Mvc;

namespace FAG_Board_Service.Controllers;

public class PlayGameController : ControllerBase
{
    private readonly IPlayGameService _service;

    public PlayGameController(IPlayGameService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetGameStatus(VisitRequest gameToken)
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
    
    [HttpPost]
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