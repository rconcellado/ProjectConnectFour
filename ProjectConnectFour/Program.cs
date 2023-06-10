namespace ProjectConnectFour
{
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
    }
    /*This will be the controller class of the program
     */
    public class clsController
    {
        private clsModel model;
        public clsController() { model = new clsModel(); }

        public void PlayGame()
        {
            model.BoardLayOut();
        }
    }
    /*Main class of the program that
     */
    public class Program
    {
        public static void Main(string[] args)
        {
            clsController Controller = new clsController();
            Controller.PlayGame();

        }
    }
}