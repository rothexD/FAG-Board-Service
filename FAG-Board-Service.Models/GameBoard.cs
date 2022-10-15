namespace FAG_Board_Service.Models;

public class GameBoard
{
    public int Id { get; set; }
    public int Size { get; set; } = 0;
    public int RemainingItems { get; set; } = 0;
    public List<List<GameTile>> Board { get; set; } = new List<List<GameTile>>();
    
    public GameBoard()
    {
    }
    
    public static GameBoard CreateGameBoard(int size)
    {
        var newBoard = new GameBoard();
        
        Random dice = new Random();
        newBoard.Size = size;

        for (int i = 0; i < size; i++)
        {
            newBoard.Board[i] = new List<GameTile>();
        }
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                var diceRoll = dice.Next(1, 7);

                if (diceRoll % 3 == 0)
                {
                    newBoard.Board[i][j] = GameTile.Coin;
                    newBoard.RemainingItems++;
                    continue;
                }
                newBoard.Board[i][j] = GameTile.Path;
            }
        }

        return newBoard;
    }
}