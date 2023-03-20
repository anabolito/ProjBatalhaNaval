using System;
using System.ComponentModel.Design;
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
        int colunaCampo = 0;
        int posicaoLinha = 0;
        bool acertou = false;
        bool fimPartida = false;
        Jogador jogadorAtual;
        string vencedor = null;

        PreenchimentoInicialCampo(campo1);
        PreenchimentoInicialCampo(campo2);

        // -- CRIAÇÃO DOS JOGADORES--//
        Jogador jogador1 = new();
        Jogador jogador2 = new();

        jogadorAtual = jogador1;         // JOGADORATUAL COMEÇA COMO JOGADOR1
        campoJogadorAtual = campo1; //  define valor inicial do jogadorAtual

        // -- CRIAÇÃO DAS EMBARCAÇÕES--
        PortaAvioes portaAviao1 = new();
        PortaAvioes portaAviao2 = new();

        Destroyer destroyer1 = new();
        Destroyer destroyer2 = new();

        Submarino submarino1 = new();
        Submarino submarino2 = new();

        Embarcacao embarcacaoAtual = submarino1;

        InserirNaviosPorJogador();
        EscondeNavio(campoJogadorAtual);    /// TENTTIVA MATRIZ Q ESCONDE

        //COMECA COM JOGADOR 1 ATIRANDO NO CAMPO 2!!!!
        do
        {
            Console.WriteLine($"\n\t\t {jogadorAtual.Nome} ESTÁ DISPARANDO NO CAMPO" +
                $" DO JOGADOR {InformarNomeAdversario()}");

            while (jogadorAtual.Disparar(campoJogadorAtual)) // INICIALMENTE SERA JOGADOR1 ATIRANDO NO CAMPO2
            {
                AlteraOrdemJogador(); // altera o jogador atual pra decrementar a vida ( pq tomou tiro)
                jogadorAtual.DecrementaVida(); // volta no jogador anterior ( que acertou o tiro)
                Console.WriteLine(jogadorAtual.Nome + " ---> VIDAS RESTANTES: " + jogadorAtual.RetornaVida());
                if (jogadorAtual.RetornaVida() == 0)
                {
                    AlteraOrdemJogador();
                    vencedor = jogadorAtual.Nome;
                    Console.WriteLine($"\n\nJOGADOR {vencedor} VENCEU O JOGO!!");
                    return;
                }
                AlteraOrdemJogador();

            }

            AlteraOrdemJogador();
            AlteraOrdemCampo();
            Console.WriteLine($"\n\t\t {jogadorAtual.Nome} ESTÁ DISPARANDO NO CAMPO" +
                $" DO JOGADOR {InformarNomeAdversario()}");
            // MostrarCampoDeBatalha(campoJogadorAtual); //DESCOMENTAR DEPOIS
            EscondeNavio(campoJogadorAtual);

            while (jogadorAtual.Disparar(campoJogadorAtual))
            {
                AlteraOrdemJogador();
                jogadorAtual.DecrementaVida();
                Console.WriteLine(jogadorAtual.Nome + " ---> VIDAS RESTANTES: " + jogadorAtual.RetornaVida());
                if (jogadorAtual.RetornaVida() == 0)
                {
                    AlteraOrdemJogador();
                    vencedor = jogadorAtual.Nome;
                    Console.WriteLine($"\n\nJOGADOR {vencedor} VENCEU O JOGO!!");
                    return;
                }

                AlteraOrdemJogador();

            }

            AlteraOrdemJogador();
            AlteraOrdemCampo();
            Console.WriteLine($"\t\t {jogadorAtual.Nome} ESTÁ DISPARANDO NO" +
                $" CAMPO DO JOGADOR {InformarNomeAdversario()}");
            // MostrarCampoDeBatalha(campoJogadorAtual);    //DESCOMENTR DEPOIS
            EscondeNavio(campoJogadorAtual);




        } while (vencedor == null);





        string InformarNomeAdversario()
        {
            if (jogadorAtual == jogador1)
            {
                string nomeAdversario = jogador2.Nome;
                return nomeAdversario;
            }
            return jogador2.Nome;
        }

        void AlteraOrdemJogador()
        {
            if (jogadorAtual == jogador1)
            {
                jogadorAtual = jogador2;
            }
            else
            {
                jogadorAtual = jogador1;
            }
        }

        void AlteraOrdemCampo()
        {
            if (campoJogadorAtual == campo1)
            {
                campoJogadorAtual = campo2;
            }
            else
            {
                campoJogadorAtual = campo1;
            }
        }






        void InserirNaviosPorJogador()
        {
            MostrarCampoDeBatalha(campoJogadorAtual);

            Console.WriteLine();

            Console.WriteLine("  JOGADOR " + jogadorAtual.Nome + ", " + "INFORME AS COORDENADAS DESEJADAS.");

            char orientacaoJogador = jogadorAtual.RetornarOrientacao();
            portaAviao1.Alinhamento = orientacaoJogador;

            colunaCampo = TransformaLetraDaColunaEmNumero();
            ColocaNavioNaMatriz(campoJogadorAtual, colunaCampo, portaAviao1);

            Console.Clear();

            MostrarCampoDeBatalha(campoJogadorAtual); // mostra na tela o campo do jogador 1

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

            AlteraOrdemJogador(); // ALTERNA PARA JOGADOR 2
            AlteraOrdemCampo(); // ALTERNA  PARA CAMPO2!!
            Console.WriteLine();
            Console.WriteLine("  JOGADOR: " + jogadorAtual.Nome + ", " + "INFORME AS COORDENADAS DESEJADAS.");
            MostrarCampoDeBatalha(campoJogadorAtual); // mostra na tela o campo do jogador 2!!
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
            AlteraOrdemJogador(); // altera para jogador 1 apos o jogador 2 colocar seus navios
        }


        void MostrarCampoDeBatalha(char[,] matriz)
        {

            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.Write("        A    B    C    D    E    F    G    H" +
                "    I    J    K    L    M    N    O    P    Q    R    S    T \n");
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


        //**********************************************************************


        void EscondeNavio(char[,] matriz)
        {
            Console.Clear(); // ADICCIONEI
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.Write("        A    B    C    D    E    F    G    H" +
                "    I    J    K    L    M    N    O    P    Q    R    S    T \n");
            int contador = 1;

            for (int linha = 1; linha <= 20; linha++)
            {

                //contador++;
                Console.Write("  " + contador.ToString("d2") + " ");
                for (int coluna = 1; coluna <= 20; coluna++)
                {
                    if (matriz[linha - 1, coluna - 1] == 'X')
                        Console.Write(" |_" + '~' + "_");
                    else if (matriz[linha - 1, coluna - 1] == '@')
                        Console.Write(" |_" + '@' + "_");
                    else if (matriz[linha - 1, coluna - 1] == 'A')
                        Console.Write(" |_" + 'A' + "_");
                    else
                        Console.Write(" |_" + '~' + "_");
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



        //*********************************************************************
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
                    Thread.Sleep(2000);

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
                if (!int.TryParse(Console.ReadLine(), out linhaEscolhida) || (linhaEscolhida > 20))
                {
                    Console.Clear();
                    MostrarCampoDeBatalha(campoJogadorAtual);
                    Console.WriteLine("Informe APENAS numeros, entre 1 e 20!/nPressione qualquer tecla" +
                        " para continuar");
                }
               
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
                } while (contadorPosicoesNavio > 0);
            }
            contadorPosicoesNavio = navio.Tamanho - 1;

            if (navio.Alinhamento == 'H')
            {
                do
                {
                    //Console.Read(); PROBLEMA DO ERRO NA LINHA ESCOLHIDA 
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
            Console.Write("        A   B   C   D   E   F   G   H   I   " +
                "J   K   L   M   N   O   P   Q   R   S   T /n");
            int contador = 1;

            for (int linha = 1; linha <= 20; linha++)
            {
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