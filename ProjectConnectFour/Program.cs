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
   public class ComputerPlayer : Player
    {
        public ComputerPlayer(string name,char disc) : base(name, disc) 
        {
        }
        public override int GetMove(clsModel model)
        {
            Random random = new Random();
            int column;
            do
            {
                column = random.Next(0, 7);
            } while (!model.IsMoveValid(column));
            return column;
        }
    }
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
                    Console.Write("#" + board[irow, icol]);
                }
                Console.WriteLine("|");
            }
        }
        public void BoardDesign(char[,] board)
        {
            int i_rows = 6, i_cols = 7, i, ix;

            for (i = 1; i <= i_rows; i++)
            {
                Console.Write("|");
                for (ix = 1; ix <= i_cols; ix++)
                {
                    if (board[i, ix] != 'X' && board[i, ix] != 'O')
                        board[i, ix] = '*';

                    Console.Write(board[i, ix]);

                }

                Console.Write("| \n");
            }
        }
        //Boolean Function to validate players move and return true or false
        public bool IsMoveValid(int col)
        {
            bool IsValid = false;
            IsValid = col >= 0 && col < cntsCol; //&& board[0, col] == ' ';
            return IsValid;
        }
        public bool ConstructMove(int column, char disc)
        {
            //if (!IsMoveValid(column))
            //{
            //    return false;
            //}

            for (int row = cntsRows - 1; row >= 0; row--)
            {
                if (board[row, column] == ' ')
                {
                    board[row, column] = disc;
                    return true;
                }
            }

            return false;
        }
    }
    /*This will be the controller class of the program
     */
    public class clsController
    {
        private clsModel model;
        private Player FirstPlayer;
        private Player SecondPlayer;
        private Player CurPlayer;
        public clsController() { model = new clsModel(); }

        public void PlayerSetup(Player firstPlayer, Player secondPlayer)
        {
            this.FirstPlayer = firstPlayer;
            this.SecondPlayer = secondPlayer;
            CurPlayer = firstPlayer;
        }

        public void PlayGame()
        {
            bool _gameEnd = false;

            while (!_gameEnd)
            {
                model.BoardLayOut();

                int move = CurPlayer.GetMove(model);

                bool isMoveValid = model.ConstructMove(move, CurPlayer.Disc);

                if(!isMoveValid)
                {
                    Console.WriteLine("Invalid move. Please try again.");
                    continue;
                }
                
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
            ComputerPlayer Computer = new ComputerPlayer("Player 2", 'X');

            clsController Controller = new clsController();

            Controller.PlayerSetup(Human,Computer);
            Controller.PlayGame();

        }
    }
}