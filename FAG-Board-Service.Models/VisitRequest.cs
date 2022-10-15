namespace FAG_Board_Service.Models;

public class VisitRequest
{
    public string GameToken { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
}