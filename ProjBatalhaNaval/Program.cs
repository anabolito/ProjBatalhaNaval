using System;
using System.Drawing;
using System.Reflection;
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







        Embarcacao embarcacaoAtual = submarino1; //.................. DEU CERTO!
                                                 // Console.WriteLine("tamanho do navio : " + navio.Tamanho);





        //----- DEFINE ALTERNÂNCIA ENTRE OS JOGADORES ---//
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

            MostrarCampoDeBatalha(campoJogadorAtual);

            char orientacaoJogador = jogadorAtual.RetornarOrientacao();
            portaAviao1.Alinhamento = orientacaoJogador;

            colunaCampo = TransformaLetraDaColunaEmNumero();
            ColocaNavioNaMatriz(campoJogadorAtual, colunaCampo, portaAviao1);

            MostrarCampoDeBatalha(campoJogadorAtual);

            orientacaoJogador = jogadorAtual.RetornarOrientacao();
            submarino1.Alinhamento = orientacaoJogador;

            colunaCampo = TransformaLetraDaColunaEmNumero();
            ColocaNavioNaMatriz(campoJogadorAtual, colunaCampo, submarino1);

            MostrarCampoDeBatalha(campoJogadorAtual);

            orientacaoJogador = jogadorAtual.RetornarOrientacao();
            destroyer1.Alinhamento = orientacaoJogador;

            colunaCampo = TransformaLetraDaColunaEmNumero();
            ColocaNavioNaMatriz(campoJogadorAtual, colunaCampo, destroyer1);
            MostrarCampoDeBatalha(campoJogadorAtual);





            fimPartida = true; // apenas para teste



        } while (fimPartida == false);




        //  --------------------------------// NAO APAGAR//-------------------------------


        //string retornoDisparo = jogador1.Disparar();
        // char coordenadaDisparoColuna = retornoDisparo[0];
        // string coordenadaDisparoLinha = retornoDisparo.Substring(1); // DEU CERTO!! !!!







        //--------------------- O JOGADOR ATIRA!! É METODO  DO OBJETO JOGADOR!
        // --- BARCO TERIA METODO afunda() ... atingido()  etc.. bool verificarDestruido()
        //OBJETO TABULEIRO...verifica  se " de uagua" ou acertou" -- objeto tabuleiro retorna se tiro foi na agua  ou acertou
        // usuario passa posicao para o objeto tabuleiro!!!
        //tabuleiro retorna true se acertou algo..e false se deu agua

        // minha ideia: objeto jogador chama o tabuleiro  e passa  as coordenadas!!










        void MostrarCampoDeBatalha(char[,] matriz)
        {
            // Console.BackgroundColor = ConsoleColor.DarkGray;

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


                Console.WriteLine("Informe a coluna para colocar a embarcação: ");
                letraColuna = Console.ReadKey(true).KeyChar;
                colunaDesejada = char.ToUpper(letraColuna);
                index = todasLetras.IndexOf(colunaDesejada);

                if (index < 0)
                {
                    Console.WriteLine("Coluna informada não foi localizada. Informe a coluna novamente!");
                    Console.ReadLine();

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

                Console.WriteLine("\tInforme a linha desejada: ");
                
               linhaEscolhida = int.Parse(Console.ReadLine());      /////////////// MUITO PROBLEMA

                //new
                for (contadorPosicoesNavio = navio.Tamanho - 1; contadorPosicoesNavio > 0; contadorPosicoesNavio--) //new
                {
                    if (matriz[linhaEscolhida - 1 + contadorPosicoesNavio , colun] == 'X')  //new
                    {
                        naoCabe = true;
                        return;//new
                    }
                }


                if ((linhaEscolhida != 1) && (linhaEscolhida != 2) && (linhaEscolhida != 3) && (linhaEscolhida != 4) && (linhaEscolhida != 5) && (linhaEscolhida != 6) && (linhaEscolhida != 7) &&
                    (linhaEscolhida != 8) && (linhaEscolhida != 9) && (linhaEscolhida != 10) && (linhaEscolhida != 11) && (linhaEscolhida != 12) && (linhaEscolhida != 13) && (linhaEscolhida != 14) &&
                    (linhaEscolhida != 15) && (linhaEscolhida != 16) && (linhaEscolhida != 17) && (linhaEscolhida != 18) && (linhaEscolhida != 19) && (linhaEscolhida != 20))
                {
                    Console.WriteLine("Digite APENAS numeros ( ente 1 e 20)!!!");
                    Console.WriteLine("Tecle para continuar....");
                    Console.Clear();
                    MostrarCampoDeBatalha(campoJogadorAtual);
                }

                if ((linhaEscolhida <= 0) || (linhaEscolhida > matriz.GetLength(0)))
                {
                    Console.Clear();
                    MostrarCampoDeBatalha(campoJogadorAtual);
                    Console.WriteLine(" Valor incorreto. Não existe essa linha!");

                }

            } while ((linhaEscolhida <= 0) || (linhaEscolhida > matriz.GetLength(0)) || naoCabe == true);  // naoCabe é novo


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
                            Console.WriteLine(" Escolha outra posicao!");
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


                    Console.Read();
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

