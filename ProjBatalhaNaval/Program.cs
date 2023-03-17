using System;
using System.Drawing;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using ProjBatalhaNaval;

internal class Program
{
    private static void Main(string[] args)
    {
        char[,] campo1 = new char[20, 20];
        char[,] campo2 = new char[20, 20];
        char[,] campoJogadorAtual;
        char[] letras = new char[20] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T' };
        int colunaCampo = 0;
        int posicaoLinha = 0;
        bool acertou = false;
        bool fimPartida = false;
        Jogador jogadorAtual;


        PreenchimentoInicialCampo(campo1);
        PreenchimentoInicialCampo(campo2);


        campoJogadorAtual = campo1; //  define valor inicial do jogadorAtual



        // -- CRIAÇÃO DOS JOGADORES--//
        Jogador jogador1 = new();
        Jogador jogador2 = new();
        jogadorAtual = jogador1;         // JOGADORATUAL COMEÇA COMO JOGADOR1


        // -- CRIAÇÃO DAS EMBARCAÇÕES--
        PortaAvioes portaAviao1 = new();
        PortaAvioes portaAviao2 = new();

        Destroyer destroyer1 = new();
        Destroyer destroyer2 = new();

        Submarino submarino1 = new();
        Submarino submarino2 = new();







        Embarcacao embarcacaoAtual = submarino1;





        //----- DEFINE ALTERNÂNCIA ENTRE OS JOGADORES ---//   CRIAR METODO QUE FAZ ESSA ALTERNANCIA!!
        if (jogadorAtual == jogador1)
        {
            campoJogadorAtual = campo1;
        }
        else if (jogadorAtual == jogador2)
        {
            campoJogadorAtual = campo2;
        }



        do
        {


            InserirNaviosPorJogador();


            jogadorAtual = jogador1;

            while (jogadorAtual.Disparar(campoJogadorAtual))
            {
                jogadorAtual.Disparar(campoJogadorAtual);
            }
            jogadorAtual.Disparar(campoJogadorAtual); // disparo!!

            jogadorAtual = jogador2;
            campoJogadorAtual = campo1;

            while (jogadorAtual.Disparar(campoJogadorAtual))
            {
                jogadorAtual.Disparar(campoJogadorAtual);
            }
            jogadorAtual.Disparar(campoJogadorAtual);







            fimPartida = true; // apenas para teste



        } while (fimPartida == false);




        void InserirNaviosPorJogador()
        {
            MostrarCampoDeBatalha(campoJogadorAtual);

            Console.WriteLine();

            Console.WriteLine("  JOGADOR: " + jogadorAtual.Nome);

            char orientacaoJogador = jogadorAtual.RetornarOrientacao();
            portaAviao1.Alinhamento = orientacaoJogador;

            colunaCampo = TransformaLetraDaColunaEmNumero();
            ColocaNavioNaMatriz(campoJogadorAtual, colunaCampo, portaAviao1);

            Console.Clear();

            MostrarCampoDeBatalha(campoJogadorAtual);

            orientacaoJogador = jogadorAtual.RetornarOrientacao();
            submarino1.Alinhamento = orientacaoJogador;

            colunaCampo = TransformaLetraDaColunaEmNumero();
            ColocaNavioNaMatriz(campoJogadorAtual, colunaCampo, submarino1);

            Console.Clear();

            MostrarCampoDeBatalha(campoJogadorAtual);

            orientacaoJogador = jogadorAtual.RetornarOrientacao();
            destroyer1.Alinhamento = orientacaoJogador;

            colunaCampo = TransformaLetraDaColunaEmNumero();
            
            ColocaNavioNaMatriz(campoJogadorAtual, colunaCampo, destroyer1);

            Console.Clear();

            MostrarCampoDeBatalha(campoJogadorAtual);

            Console.Clear();


            //------------ COLONA NAVIOS JOGADOR 2---------------------------//
            jogadorAtual = jogador2; // ALTERNA JOGADOR
            campoJogadorAtual = campo2; // ALTERNA CAMPO
            Console.WriteLine();
            Console.WriteLine("\n  JOGADOR: " + jogadorAtual.Nome);
            MostrarCampoDeBatalha(campoJogadorAtual);
            orientacaoJogador = jogadorAtual.RetornarOrientacao();
            portaAviao2.Alinhamento = orientacaoJogador;

            colunaCampo = TransformaLetraDaColunaEmNumero();
            ColocaNavioNaMatriz(campoJogadorAtual, colunaCampo, portaAviao2);

            Console.Clear();

            MostrarCampoDeBatalha(campoJogadorAtual);

            orientacaoJogador = jogadorAtual.RetornarOrientacao();
            submarino2.Alinhamento = orientacaoJogador;

            colunaCampo = TransformaLetraDaColunaEmNumero();
            ColocaNavioNaMatriz(campoJogadorAtual, colunaCampo, submarino2);

            Console.Clear();

            MostrarCampoDeBatalha(campoJogadorAtual);

            orientacaoJogador = jogadorAtual.RetornarOrientacao();
            destroyer2.Alinhamento = orientacaoJogador;

            colunaCampo = TransformaLetraDaColunaEmNumero();
            ColocaNavioNaMatriz(campoJogadorAtual, colunaCampo, destroyer2);

            Console.Clear();

            MostrarCampoDeBatalha(campoJogadorAtual);
        }


        void MostrarCampoDeBatalha(char[,] matriz)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.Write("        A    B    C    D    E    F    G    H");
            Console.Write("    I    J    K    L    M    N    O    P");
            Console.Write("    Q    R    S    T ");
            Console.WriteLine();
            int contador = 1;

            for (int linha = 1; linha <= 20; linha++)
            {

                //contador++;
                Console.Write("  " + contador.ToString("d2") + " ");
                for (int coluna = 1; coluna <= 20; coluna++)
                {

                    Console.Write(" |_" + matriz[linha - 1, coluna - 1] + "_");
                    if (coluna == 20)
                    {
                        Console.Write(" |");
                        Console.WriteLine();
                        contador++;
                    }
                }

            }
            Console.ResetColor();

        }




        int TransformaLetraDaColunaEmNumero()
        {

            char colunaDesejada;
            char letraColuna;
            string todasLetras = "ABCDEFGHIJKLMNOPQRST";
            int index = -1;


            do
            {

                Console.Write("  Informe a coluna para colocar a embarcação: ");
                letraColuna = Console.ReadKey(true).KeyChar;
                colunaDesejada = char.ToUpper(letraColuna);
                index = todasLetras.IndexOf(colunaDesejada);
                Console.WriteLine(colunaDesejada);
                Console.WriteLine();

                if (index < 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("  Coluna informada não foi localizada. Informe a coluna novamente!");
                    Thread.Sleep(4000);

                    Console.Clear();
                    MostrarCampoDeBatalha(campoJogadorAtual);
                }

            } while (index < 0);
            return index;
        }




        void ColocaNavioNaMatriz(char[,] matriz, int colun, Embarcacao navio)
        {

            int linhaEscolhida;
            int contadorPosicoesNavio = navio.Tamanho - 1;
            bool naoCabe = false;  //new

            do
            {


                Console.Write("  Informe a linha desejada: ");

                6linhaEscolhida = int.Parse(Console.ReadLine());      /////////////// MUITO PROBLEMA




                //new

                if (matriz[linhaEscolhida - 1 + contadorPosicoesNavio, colun] == 'X')  //new
                {
                    Console.WriteLine("Posição inválida, escolha novamente.");

                }



                /*if (linhaEscolhida<1 || linhaEscolhida>20)
                {
                    Console.WriteLine("  Digite APENAS números ente 1 e 20!");
                    Console.WriteLine("  Tecle para continuar....");
                    Console.ReadKey();
                    Console.Clear();
                    MostrarCampoDeBatalha(campoJogadorAtual);
                }*/

                /*if ((linhaEscolhida <= 0) || (linhaEscolhida > matriz.GetLength(0)))
                {
                    Console.Clear();
                    MostrarCampoDeBatalha(campoJogadorAtual);
                    Console.WriteLine("  Valor incorreto. Não existe essa linha!");

                }*/

            } while ((linhaEscolhida <= 0) || (linhaEscolhida > 20) || matriz[linhaEscolhida - 1 + contadorPosicoesNavio, colun] == 'X');  // naoCabe é novo


            matriz[linhaEscolhida - 1, colun] = 'X';




            if (navio.Alinhamento == 'V')
            {
                contadorPosicoesNavio = navio.Tamanho - 1;

                do
                {
                    if (((linhaEscolhida - 1) + contadorPosicoesNavio) <= matriz.GetLength(0))
                    {
                        if (matriz[(linhaEscolhida - 1) + contadorPosicoesNavio, colun] == '~')
                        {
                            matriz[(linhaEscolhida - 1) + contadorPosicoesNavio, colun] = 'X';
                            contadorPosicoesNavio--;
                        }
                        else
                        {
                            Console.WriteLine("  Escolha outra posicao!");
                            break;
                        }
                    }
                    else
                    {

                    }


                } while (contadorPosicoesNavio > 0);
            }
            contadorPosicoesNavio = navio.Tamanho - 1;

            if (navio.Alinhamento == 'H')
            {


                do
                {


                    //Console.Read();
                    matriz[(linhaEscolhida - 1), colun + contadorPosicoesNavio] = 'X';
                    contadorPosicoesNavio--;


                } while (contadorPosicoesNavio > 0);


            }

        }




        void PreenchimentoInicialCampo(char[,] matriz)
        {
            for (int linha = 0; linha < matriz.GetLength(0); linha++)
            {
                for (int coluna = 0; coluna < matriz.GetLength(1); coluna++)
                {
                    matriz[linha, coluna] = '~';
                }
            }
        }



        void ExibeMatrizPreenchida(char[,] matriz)
        {
            Console.Write("        A   B   C   D   E   F   G   H");
            Console.Write("   I   J   K   L   M   N   O   P");
            Console.Write("   Q   R   S   T ");
            Console.WriteLine();
            int contador = 1;

            for (int linha = 1; linha <= 20; linha++)
            {

                //contador++;
                Console.Write("  " + contador.ToString("d2") + " ");
                for (int coluna = 1; coluna <= 20; coluna++)
                {

                    Console.Write(" |_" + matriz[linha - 1, coluna - 1] + "_");
                    if (coluna == 20)
                    {
                        Console.Write(" |");
                        Console.WriteLine();
                        contador++;
                    }
                }

            }
        }


    }
}

