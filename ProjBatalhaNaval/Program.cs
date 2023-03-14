internal class Program
{
    private static void Main(string[] args)
    {
        char[,] board = new char[20, 20];

        FillBoard(); //inicializa a matriz com espaços vazios as coordenadas da matriz
        PrintBoard();

        void FillBoard()
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = '0';
                }
            }
        }

        void PrintBoard()
        {
            char[] letter = new char[20] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T' };
            int numbers = 1;
            for (int count = 0; count < board.GetLength(0); count++)
            {
                if (count == 0)
                    Console.Write("     ");
                Console.Write(letter[count] + "   ");
            }
            Console.WriteLine();
            CloseLine();
            for (int i = 0; i < board.GetLength(0); i++)
            {
                if (numbers < 10)
                    Console.Write(numbers + " ");
                else
                    Console.Write(numbers);
                numbers++;
                for (int j = 0; j < board.GetLength(0); j++)
                {
                    if (j == 0)
                        Console.Write(" |");
                    Console.Write(" " + board[i, j] + " ");
                    if (j < board.Length - 1)
                        Console.Write("|");
                }
                Console.WriteLine();
                if (i < board.Length - 1)
                    FillLine();
            }
            Console.WriteLine();
        }

        void FillLine()
        {
            int pipe = 0;
            for (int i = 0; i < (board.GetLength(0) * 3); i++)
            {
                if (i == 0)
                    Console.Write("   |");
                Console.Write('_');
                pipe++;
                if (pipe % 3 == 0 && pipe != 0)
                    Console.Write('|');
            }
            Console.WriteLine();
        }

        void CloseLine()
        {
            int space = 0;
            for (int i = 0; i < (board.GetLength(0) * 3); i++)
            {
                if (i == 0)
                    Console.Write("    ");
                Console.Write('_');
                space++;
                if (space % 3 == 0 && space != 0)
                    Console.Write(' ');
            }
            Console.WriteLine();
        }
    }
}