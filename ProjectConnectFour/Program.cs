using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Connect4
{
    public abstract class Player
    {
        public  string Name { get; set; }
        public char Id { get; set; }

        public Player(string name, char id)
        {
            Name = name;
            Id = id;
        }
    }

    public class HumanPlayers : Player
    {
        public string Name;
        public char Id;

        public HumanPlayers(string  playerName, char id) : base(playerName, id)
        {

        }
    }

    public struct HumanPlayer
    {
        public string PlayerName;
        public char PlayerID;
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

        public int PlayerDrop(Player activePlayer)
        {
            int choice;

            Console.WriteLine(activePlayer.Name + "'s Turn ");
            do
            {
                Console.WriteLine("Please enter a number between 1 and 7: ");
                choice = Convert.ToInt32(Console.ReadLine());
            } while (choice < 1 || choice > 7);

            while (board[1, choice] == 'X' || board[1, choice] == 'O')
            {
                Console.WriteLine("That row is _full. Please enter a new row: ");
                choice = Convert.ToInt32(Console.ReadLine());
            }

            return choice;
        }

        public void CheckBellow(Player activePlayer, int numselect)
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

        public int Restart()
        {
            int restart;

            Console.WriteLine("Would you like to restart? Yes(1) No(2): ");
            restart = Convert.ToInt32(Console.ReadLine());

            if (restart == 1)
            {
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        board[i, j] = '*';
                    }
                }
            }
            else
            {
                Console.WriteLine("Goodbye!");
            }

            return restart;
        }

        public void Player_win(Player activePlayer)
        {
            Console.WriteLine(activePlayer.Name + " Connected Four! You _win!");
        }


    }

    public class clsController
    {
        clsModel model;
        private char[,] board;
        private int _numsel;
        private int _win;
        private int _full;
        private int _another;

        private Player _player1;
        private Player _player2;

        public clsController()
        {
            _numsel = 0;
            _win = 0;
            _full = 0;
            _another = 0;
            model = new clsModel();
        }

        public void SetPlayers(Player player1 , Player player2)
        {
            this._player1 = player1;
            this._player2 = player2;
        }

        public void StartGame()
        {
            model.DisplayBoard();

            do
            {
                _numsel = model.PlayerDrop(_player1);
                model.CheckBellow(_player1, _numsel);
                model.DisplayBoard();
                _win = model.CheckFour(_player1);

                if (_win == 1)
                {
                    model.Player_win(_player1);
                    _another = model.Restart();
                    if (_another == 2)
                    {
                        break;
                    }
                }
                _numsel = model.PlayerDrop(_player2);
                model.CheckBellow(_player2, _numsel);
                model.DisplayBoard();
                _win = model.CheckFour(_player2);
                if (_win == 1)
                {
                    model.Player_win(_player2);
                    _another = model.Restart();
                    if (_another == 2)
                    {
                        break;
                    }
                }

                _full = model._fullBoard();

                if (_full == 7)
                {
                    Console.WriteLine("The board is _full. It is a draw!");
                    _another = model.Restart();
                }

            } while (_another != 2);
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            HumanPlayers Player1 = new HumanPlayers("Gabriel", 'X');
            HumanPlayers Player2 = new HumanPlayers("Rey", 'O');

            clsController controller = new clsController();

            controller.SetPlayers(Player1, Player2);

            controller.StartGame();


        }
    }
}