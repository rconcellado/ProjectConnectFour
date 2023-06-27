using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Connect4
{   //This is an abstract class called Player
    public abstract class Player
    {   //Public variable to hold Player's Name 
        public string Name { get; set; }

        //public variable to hold Player's Id : X or O
        public char Id { get; set; }

        public Player(string name, char id)
        {
            Name = name;
            Id = id;
        }

        public override string ToString()
        {
            return $"Player Name: {Name}, ID: {Id}";
        }

    }
    /*This is a derived class of the Abstract Class Player
       It inherits the Name and Id of the player*/
    public class HumanPlayer : Player
    {
        public HumanPlayer(string playerName, char id) : base(playerName, id)
        {

        }
       
    }

    public class clsModel
    {
        private char[,] board;
        private int _win;
        private int _full;

        public clsModel()
        {
            board = new char[9, 10];
        }
        /*This methos is to diplay the board game of Connect Four 
          This board contains 6 rows and 7 columns*/
        public void DisplayBoard()
        {
            int rows = 6;
            int columns = 7;

            for (int i = 1; i <= rows; i++)
            {
                Console.Write("|");
                for (int j = 1; j <= columns; j++)
                {
                    if (board[i, j] != 'X' && board[i, j] != 'O')
                    {
                        board[i, j] = '*';
                    }
                    Console.Write(board[i, j]);
                }

                Console.Write("| \n");
            }
        }
        /*This method will verify the Players input between number 1 to 7
          It will ensure that the Players can only input between 1 to 7.*/
        
        public int PlayerNumber(Player activePlayer)
        {
            int choice;
            Console.WriteLine("\n");
            Console.WriteLine(activePlayer.Name + "'s Turn ");

            bool validInput = false;
            do
            {
                Console.WriteLine("Please enter a number between 1 and 7: ");
                string input = Console.ReadLine();

                validInput = int.TryParse(input, out choice) && choice >= 1 && choice <= 7;

                if (!validInput)
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }
            } while(!validInput);

            while (board[1, choice] == 'X' || board[1, choice] == 'O')
            {
                Console.WriteLine("That row is _full. Please enter a new row: ");
                string input = Console.ReadLine();

                validInput = int.TryParse(input, out choice) && choice >= 1 && choice <= 7;

                if (!validInput)
                {
                    Console.WriteLine("Invalid input. Please try again.");
                    continue;
                }

                if (board[1, choice] == 'X' || board[1, choice] == 'O')
                {
                    Console.WriteLine("That row is full. Please enter a new row: ");
                    validInput = false;
                }
            }

            return choice;
        }
        
        public void CheckMove(Player activePlayer, int numselect)
        {
            int length = 6;
            int turn = 0;

            do
            {
                if (board[length, numselect] != 'X' && board[length, numselect] != 'O')
                {
                    board[length, numselect] = activePlayer.Id;
                    turn = 1;
                }
                else
                {
                    --length;
                }
            } while (turn != 1);
        }
        /*This method will check if the Player connected the Four pieces of Id*/
        public int CheckFour(Player activePlayer)
        {
            char XO = activePlayer.Id;
            int _win = 0;

            for (int i = 8; i >= 1; i--)
            {
                for (int j = 9; j >= 1; j--)
                {
                    if (board[i, j] == XO &&
                        board[i - 1, j - 1] == XO &&
                        board[i - 2, j - 2] == XO &&
                        board[i - 3, j - 3] == XO)
                    {
                        _win = 1;
                    }

                    if (board[i, j] == XO &&
                        board[i, j - 1] == XO &&
                        board[i, j - 2] == XO &&
                        board[i, j - 3] == XO)
                    {
                        _win = 1;
                    }

                    if (board[i, j] == XO &&
                        board[i - 1, j] == XO &&
                        board[i - 2, j] == XO &&
                        board[i - 3, j] == XO)
                    {
                        _win = 1;
                    }

                    if (board[i, j] == XO &&
                        board[i - 1, j + 1] == XO &&
                        board[i - 2, j + 2] == XO &&
                        board[i - 3, j + 3] == XO)
                    {
                        _win = 1;
                    }

                    if (board[i, j] == XO &&
                        board[i, j + 1] == XO &&
                        board[i, j + 2] == XO &&
                        board[i, j + 3] == XO)
                    {
                        _win = 1;
                    }
                }
            }

            return _win;
        }
        /*This method will check if the board is full, otherwise continue to play*/
        public int _fullBoard()
        {
            int _full = 0;
            for (int i = 1; i <= 7; i++)
            {
                if (board[1, i] != '*')
                {
                    _full++;
                }
            }

            return _full;
        }
        /*This method will display the winner of the game*/
        public void Player_win(Player activePlayer)
        {
            Console.WriteLine(activePlayer.Name + " Connected Four! You win!");
        }


    }

    public class clsController
    {   //Instantiate class clsModel to an object so that you can it in Controller class 
        clsModel model;

        private int _numsel;
        //private char _playAgain;

        private Player _player1;
        private Player _player2;

        public clsController()
        {
            _numsel = 0;
            model = new clsModel();
        }

        public void SetPlayers(Player player1, Player player2)
        {
            this._player1 = player1;
            this._player2 = player2;
        }

        public void Play()
        {
            model.DisplayBoard();

            while (true)
            {
                _numsel = model.PlayerNumber(_player1);
                //_numsel = _player1.MakeMove();
                model.CheckMove(_player1, _numsel);
                model.DisplayBoard();

                if (model.CheckFour(_player1) == 1)
                {
                    model.Player_win(_player1);
                    break;
                }

                _numsel = model.PlayerNumber(_player2);
                //_numsel = _player1.MakeMove();
                model.CheckMove(_player2, _numsel);
                model.DisplayBoard();
                if (model.CheckFour(_player2) == 1)
                {
                    model.Player_win(_player2);
                    break;
                }

                if (model._fullBoard() == 7)
                {
                    Console.WriteLine("The board is full. It is a draw!");
                    break;
                }

            }
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {   //Instantiate the class HumanPlayer to object Player1 
            HumanPlayer Player1 = new HumanPlayer("Gabriel", 'X');
            // Output: Player Name: Gabriel, ID: X
            Console.WriteLine(Player1.ToString());

            //Instantiate the class HumanPlayer to object Player2 
            HumanPlayer Player2 = new HumanPlayer("Rey", 'O');
            // Output: Player Name: Rey, ID: O
            Console.WriteLine(Player2.ToString());


            //Instantiate the class name clsController to object controller
            clsController controller = new clsController();

            controller.SetPlayers(Player1, Player2);

            //Play is the Main method of the class clsController
            controller.Play();
        }
    }
}