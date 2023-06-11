using System.Data.Common;

namespace ProjectConnectFour
{
    public abstract class Player
    {
        public string Name { get; set; }
        public char Disc { get;set; }
        public Player(string name, char disc)
        {
            Name = name;
            Disc = disc;
        }
        public abstract int GetMove(clsModel model);
    }

    public class HumanPlayer : Player //Child class derived from Super Abstract Class which is Player
    {
        public HumanPlayer(string name, char disc) : base(name, disc)
        {

        }
        public override int GetMove(clsModel model)
        {
            Console.WriteLine("Enter column (1-7): ");
            int col;//This statement will validate players move pressing between keys 1 - 7 only
            while (!int.TryParse(Console.ReadLine(), out col) || col < 0 || col > 6 || !model.IsMoveValid(col))
            {
                Console.WriteLine("Invalid move. Please try again.");
                Console.WriteLine("Enter column (1-7): ");
            }
                return col;
        }
    }
    /* This will be the Model Class of the program
     */
    public class clsModel
    {
        private char[,] board;
        public const int cntsRows = 6;
        public const int cntsCol = 7;

        public clsModel()
        {
            board = new char[cntsRows, cntsCol];
           
        }
        /*This public void method will initialize the board apperance in
         the initial run of the program
        */
        public void BoardLayOut()
        {
            Console.WriteLine("Connect 4 Game Development Project:");
            //Loop though the Rows which have 6 Rows
            for (int irow = 0; irow < cntsRows; irow++)
            {
                Console.Write("|");
                //Loop through the columns which have 7 columns
                for (int icol = 0; icol < cntsCol; icol++)
                {   //Print the has - # for every board - row,column in the board
                    Console.Write("  " + "#" + board[irow, icol] + " ");
                }
                Console.WriteLine(" |");
            }
            Console.WriteLine("   1   2   3   4   5   6   7 ");
        }
        //Boolean Function to validate players move and return true or false
        public bool IsMoveValid(int col)
        {
            bool IsValid = false;
            IsValid = col >= 0 && col < cntsCol; //&& board[0, col] == ' ';
            return IsValid;
        }
    }
    /*This will be the controller class of the program
     */
    public class clsController
    {
        private clsModel model;
        private Player FirstPlayer;
        private Player CurPlayer;
        public clsController() { model = new clsModel(); }

        public void PlayerSetup(Player firstPlayer)
        {
            this.FirstPlayer = firstPlayer;
            CurPlayer = firstPlayer;
        }

        public void PlayGame()
        {
            bool _gameEnd = false;

            while (!_gameEnd)
            {
                model.BoardLayOut();

                int move = CurPlayer.GetMove(model);
                
            }
        }
    }
    /*Main class of the program that
     */
    public class Program
    {
        public static void Main(string[] args)
        {
            HumanPlayer Human = new HumanPlayer("Player 1", 'O');

            clsController Controller = new clsController();

            Controller.PlayerSetup(Human);
            Controller.PlayGame();

        }
    }
}