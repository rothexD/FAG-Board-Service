using System.ComponentModel.DataAnnotations;

namespace FAG_Board_Service.Models;

public class GameInfo
{
    [Key]
    public string GameToken{ get; set; }
    public GameBoard Board{ get; set; }
}