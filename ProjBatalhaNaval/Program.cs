internal class Program
{
    private static void Main(string[] args)
    {
        char[,] campo = new char[20, 20];
        char[,] campo2 = new char[20, 20];
        char[] letras = new char[20] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T' };
        //int LinhaCampo;    // falta letra M no vetor e da erro contagem
        int colunaCampo = 0;


        PreenchimentoInicialCampo(campo);

        MostraCampoBatalha(campo);

        //ExibeMatrizPreenchida(campo);
        colunaCampo = buscarColuna();
        ColocaNavioNaMAtriz1(campo, colunaCampo);

        MostraCampoBatalha(campo);



        void MostraCampoBatalha(char[,] matriz)
        {
            Console.Write("   A    B    C    D    E    F    G    H");
            Console.Write("    I    J    K    L    M    N    O    P");
            Console.Write("    Q    R    S    T "); // vai ate T
            Console.WriteLine();
            int contador = 1;

            for (int linha = 1; linha <= 20; linha++)
            {
                //Console.Write(contador + "  ");
                contador++;
                for (int coluna = 1; coluna <= 20; coluna++)
                {
                    Console.Write(" |_" + campo[linha - 1, coluna - 1] + "_");
                    if (coluna == 20)
                    {
                        Console.Write("|");
                        Console.WriteLine();
                    }
                }

            }
        }




        void ColocaNavioNaMAtriz1(char[,] matriz, int colun) // problema retorno coluna valor alto
        {
            Console.Clear();
            int linhaEscolhida = 1;

            do
            {
                // Console.WriteLine("coliuna --> " + colun);//
                Console.WriteLine();
                Console.WriteLine("\tInforme a linha desejada: ");
                linhaEscolhida = int.Parse(Console.ReadLine());

                if ((linhaEscolhida <= 0) || (linhaEscolhida > matriz.GetLength(0)))
                {
                    Console.WriteLine(" Valor incorreto. Não existe essa linha!");
                }

            } while ((linhaEscolhida <= 0) || (linhaEscolhida > matriz.GetLength(0)));
            matriz[linhaEscolhida - 1, colun] = 'x';
            Console.ReadKey();
        }


        void PreenchimentoInicialCampo(char[,] matriz)
        {
            for (int linha = 0; linha < matriz.GetLength(0); linha++)
            {
                for (int coluna = 0; coluna < matriz.GetLength(1); coluna++)
                {
                    campo[linha, coluna] = 'O';
                }
            }
        }

        void ExibeMatrizPreenchida(char[,] matriz)
        {
            for (int linha = 0; linha < matriz.GetLength(0); linha++)
            {
                for (int coluna = 0; coluna < matriz.GetLength(1); coluna++)
                {
                    Console.Write("  " + campo[linha, coluna] + " ");
                    if (coluna == 19)
                    {
                        Console.WriteLine();
                    }
                }
            }
        }


        int buscarColuna()
        {
            int i;
            char colunaDesejada;
            string aux;
            int sla;
            Console.WriteLine();
            Console.WriteLine("\tInforme a coluna desejada: ");
            aux = Console.ReadLine();
            colunaDesejada = aux[0];
            //sla = int.Parse(aux[1]);

            for (i = 0; i < letras.Length; i++)
            {
                if (letras[i] == Char.ToUpper(colunaDesejada))
                {
                    Console.WriteLine(" o indice é: " + i);

                    break;
                }

            }
            Console.WriteLine(" A coluna é: " + i);
            Console.ReadKey();
            return i;

        }





    }
}
