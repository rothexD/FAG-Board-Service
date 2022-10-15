using System.ComponentModel.DataAnnotations;

namespace FAG_Board_Service.Models;

public class GameBoard
{
    [Key] 
    public int Id { get; set; } = 0;
    public int Size { get; set; } = 0;
    public int RemainingItems { get; set; } = 0;
    public List<GameBoardX> X { get; set; }
    
    public GameBoard()
    {
    }
    
    public static GameBoard CreateGameBoard(int size)
    {
        var newBoard = new GameBoard();
        
        Random dice = new Random();
        newBoard.Size = size;

        newBoard.X = new List<GameBoardX>();
        
        for (int i = 0; i < size; i++)
        {
            newBoard.X.Insert(i,new GameBoardX());
            
            for (int j = 0; j < size; j++)
            {
                var diceRoll = dice.Next(1, 7);
                if (diceRoll % 3 == 0)
                {
                    newBoard.X[i].Y.Insert(j, new GameBoardY()
                    {
                        TileInfo = GameTile.Coin
                    });
                    newBoard.RemainingItems++;
                    continue;
                }
                newBoard.X[i].Y.Insert(j,  new GameBoardY()
                {
                    TileInfo = GameTile.Path
                });
            }
        }

        return newBoard;
    }
}