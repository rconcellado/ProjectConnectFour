using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Connect4
{
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

        public int PlayerDrop(HumanPlayer activePlayer)
        {
            int choice;

            Console.WriteLine(activePlayer.PlayerName + "'s Turn ");
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

        public void CheckBellow(HumanPlayer activePlayer, int numselect)
        {
            int length = 6;
            int turn = 0;

            do
            {
                if (board[length, numselect] != 'X' && board[length, numselect] != 'O')
                {
                    board[length, numselect] = activePlayer.PlayerID;
                    turn = 1;
                }
                else
                {
                    --length;
                }
            } while (turn != 1);
        }

        public int CheckFour(HumanPlayer activePlayer)
        {
            char XO = activePlayer.PlayerID;
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

        public void Player_win(HumanPlayer activePlayer)
        {
            Console.WriteLine(activePlayer.PlayerName + " Connected Four! You _win!");
        }


    }

    public class clsController
    {
        clsModel model;
        private HumanPlayer _playerOne;
        private HumanPlayer _playerTwo;
        private char[,] board;
        private int _numsel;
        private int _win;
        private int _full;
        private int _another;

        Game game = new Game();


        public clsController()
        {
            //board = new char[9, 10];
            _numsel = 0;
            _win = 0;
            _full = 0;
            _another = 0;
            model = new clsModel();
        }

        public void StartGame()
        {
            Console.WriteLine("Let's Play Connect Four");
            Console.WriteLine("First Player, Please enter your name: ");
            _playerOne.PlayerName = Console.ReadLine();
            _playerOne.PlayerID = 'X';

            Console.WriteLine("Second Player, Please enter your name: ");
            _playerTwo.PlayerName = Console.ReadLine();
            _playerTwo.PlayerID = 'O';

            model.DisplayBoard();

            do
            {
                _numsel = model.PlayerDrop(_playerOne);
                model.CheckBellow(_playerOne, _numsel);
                model.DisplayBoard();
                _win = model.CheckFour(_playerOne);

                if (_win == 1)
                {
                    model.Player_win(_playerOne);
                    _another = game.Restart();
                    if (_another == 2)
                    {
                        break;
                    }
                }
                _numsel = model.PlayerDrop(_playerTwo);
                model.CheckBellow(_playerTwo, _numsel);
                model.DisplayBoard();
                _win = model.CheckFour(_playerTwo);
                if (_win == 1)
                {
                    model.Player_win(_playerTwo);
                    _another = game.Restart();
                    if (_another == 2)
                    {
                        break;
                    }
                }

                _full = model._fullBoard();

                if (_full == 7)
                {
                    Console.WriteLine("The board is _full. It is a draw!");
                    _another = game.Restart();
                }

            } while (_another != 2);
        }
    }

    public class GameBoard
    {
        private char[,] board;
        private int rows;
        private int columns;

        public GameBoard()
        {
            rows = 6;
            columns = 7;
            board = new char[rows, columns];
        }

        public void DisplayBoard()
        {
            for (int i = 0; i < rows; i++)
            {
                Console.Write("|");
                for (int j = 0; j < columns; j++)
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
    }

    public class Game
    {
        private char[,] board;

        public Game()
        {
            board = new char[6, 7];
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
    }

    public class Program
    {
        static void Main(string[] args)
        {
            clsController controller = new clsController();
            controller.StartGame();
        }
    }
}