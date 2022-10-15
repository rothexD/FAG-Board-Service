using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FAG_Board_Service.Models;

public class GameBoardX
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; } = 0;
    public List<GameBoardY> Y { get; set; } = new List<GameBoardY>();

    public GameBoardX()
    {
        
    }
}