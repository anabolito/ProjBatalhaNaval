using System;
using System.ComponentModel.Design;
using System.Drawing;
using System.Reflection;
using System.Runtime.CompilerServices;
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

        jogadorAtual = jogador1;
        campoJogadorAtual = campo1;

        // -- CRIAÇÃO DAS EMBARCAÇÕES--
        PortaAvioes portaAviao1 = new();
        PortaAvioes portaAviao2 = new();

        Destroyer destroyer1 = new();
        Destroyer destroyer2 = new();

        Submarino submarino1 = new();
        Submarino submarino2 = new();

        Embarcacao embarcacaoAtual = submarino1;

        InserirNaviosPorJogador();
        EscondeNavio(campoJogadorAtual);

       
        do
        {
            Console.WriteLine($"\n  {jogadorAtual.Nome} ESTÁ DISPARANDO NO CAMPO" +
                $" DO JOGADOR {InformarNomeAdversario()}");

            while (jogadorAtual.Disparar(campoJogadorAtual))
            {
                EscondeNavio(campoJogadorAtual);
                AlteraOrdemJogador();
                jogadorAtual.DecrementaVida();
                Console.WriteLine("\n  " + jogadorAtual.Nome + "   ---> VIDAS RESTANTES: " + jogadorAtual.RetornaVida());
                if (jogadorAtual.RetornaVida() == 0)
                {
                    AlteraOrdemJogador();
                    vencedor = jogadorAtual.Nome;
                    Console.WriteLine($"\n  ***JOGADOR {vencedor} VENCEU O JOGO!!***");
                    return;
                }
                AlteraOrdemJogador();

            }

            AlteraOrdemJogador();
            AlteraOrdemCampo();
            Console.WriteLine($"\n  {jogadorAtual.Nome} ESTÁ DISPARANDO NO CAMPO" +
                $" DO JOGADOR {InformarNomeAdversario()}");
           
            EscondeNavio(campoJogadorAtual);

            while (jogadorAtual.Disparar(campoJogadorAtual))
            {
                EscondeNavio(campoJogadorAtual);
                AlteraOrdemJogador();
                jogadorAtual.DecrementaVida();
                Console.WriteLine("\n  " + jogadorAtual.Nome + "   ---> VIDAS RESTANTES: " + jogadorAtual.RetornaVida());
                if (jogadorAtual.RetornaVida() == 0)
                {
                    AlteraOrdemJogador();
                    vencedor = jogadorAtual.Nome;
                    Console.WriteLine($"\n  ***JOGADOR {vencedor} VENCEU O JOGO!!***");
                    return;
                }

                AlteraOrdemJogador();

            }

            AlteraOrdemJogador();
            AlteraOrdemCampo();
            Console.WriteLine($"\n  {jogadorAtual.Nome} ESTÁ DISPARANDO NO" +
                $" CAMPO DO JOGADOR {InformarNomeAdversario()}");
            
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

            Console.WriteLine("\n  JOGADOR " + jogadorAtual.Nome + ", " + "INFORME AS COORDENADAS DESEJADAS. \n\n");

            char orientacaoJogador = jogadorAtual.RetornarOrientacao();
            portaAviao1.Alinhamento = orientacaoJogador;

            colunaCampo = TransformaLetraDaColunaEmNumero();

            ColocaNavioNaMatriz(campoJogadorAtual, colunaCampo, portaAviao1);

            Console.Clear();

            MostrarCampoDeBatalha(campoJogadorAtual); 

            orientacaoJogador = jogadorAtual.RetornarOrientacao();
            
            
            submarino1.Alinhamento = orientacaoJogador;

            colunaCampo = TransformaLetraDaColunaEmNumero();
            
            bool retorno = false;

            while (retorno == false)
            {
                retorno = ColocaNavioNaMatriz(campoJogadorAtual, colunaCampo, submarino1);

            }
            retorno = false; 

            Console.Clear();

            MostrarCampoDeBatalha(campoJogadorAtual);

            orientacaoJogador = jogadorAtual.RetornarOrientacao();
            destroyer1.Alinhamento = orientacaoJogador;

            colunaCampo = TransformaLetraDaColunaEmNumero();
            while (retorno == false)
            {
                retorno = ColocaNavioNaMatriz(campoJogadorAtual, colunaCampo, destroyer1);
            }

            retorno = false;

            Console.Clear();

            MostrarCampoDeBatalha(campoJogadorAtual);
            Console.WriteLine("Aguarde um momento...");
            Thread.Sleep(2000);

            Console.Clear();

            //------------ COLONA NAVIOS JOGADOR 2---------------------------//

            AlteraOrdemJogador();
            AlteraOrdemCampo(); 
            Console.WriteLine();
            Console.WriteLine("\n  JOGADOR: " + jogadorAtual.Nome + ", " + "INFORME AS COORDENADAS DESEJADAS.\n\n");
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
            while (retorno == false)
            {
                retorno = ColocaNavioNaMatriz(campoJogadorAtual, colunaCampo, submarino2);
            }
            retorno = false;

            Console.Clear();

            MostrarCampoDeBatalha(campoJogadorAtual);

            orientacaoJogador = jogadorAtual.RetornarOrientacao();
            destroyer2.Alinhamento = orientacaoJogador;

            colunaCampo = TransformaLetraDaColunaEmNumero();
            while (retorno == false)
            {
                retorno = ColocaNavioNaMatriz(campoJogadorAtual, colunaCampo, destroyer2);
            }
            retorno = false;


            Console.Clear();

            MostrarCampoDeBatalha(campoJogadorAtual);
            Console.WriteLine("Aguarde um momento...");
            Thread.Sleep(2000);

            AlteraOrdemJogador();
        }


        void MostrarCampoDeBatalha(char[,] matriz)
        {

            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.Write("        A    B    C    D    E    F    G    H" +
                "    I    J    K    L    M    N    O    P    Q    R    S    T \n");
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
            Console.ResetColor();

        }




        void EscondeNavio(char[,] matriz)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.Write("        A    B    C    D    E    F    G    H" +
                "    I    J    K    L    M    N    O    P    Q    R    S    T \n");
            int contador = 1;

            for (int linha = 1; linha <= 20; linha++)
            {

                
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

        bool ColocaNavioNaMatriz(char[,] matriz, int colun, Embarcacao navio)
        {

            int linhaEscolhida = 0;
            int contadorPosicoesNavio = navio.Tamanho - 1;
            bool naoCabe = false;
            bool deuCerto = false;

            do
            {

                Console.Write("  Informe a linha desejada: ");

                while (!int.TryParse(Console.ReadLine(), out linhaEscolhida))
                {
                    Console.Write("  Informe a linha desejada novamente: ");
                }

                while( linhaEscolhida > 20 || linhaEscolhida <=0)
                {
                    Console.Write("  Informe a linha desejada entre 1 e 20: ");
                    linhaEscolhida = int.Parse(Console.ReadLine());
                }
               


                do   
                {
                    naoCabe = false;

                    if( (linhaEscolhida-1) + contadorPosicoesNavio > 19)
                    {
                        naoCabe = true;
                        break;
                    }
                    if( (colun + contadorPosicoesNavio) > 19)
                    {
                        naoCabe = true;
                        break;
                    }

                    if (navio.Alinhamento =='V' && matriz[linhaEscolhida - 1 + contadorPosicoesNavio, colun] == 'X')
                    {
                        Console.WriteLine("  Posição já ocupada, escolha novamente.");
                       // contadorPosicoesNavio++;
                        naoCabe = true;
                        Console.WriteLine(contadorPosicoesNavio);
                       
                       break; 
                    }
                    if (navio.Alinhamento == 'V' && matriz[linhaEscolhida - 1 + contadorPosicoesNavio, colun] >=matriz.GetLength(0))
                    {
                        Console.WriteLine("  Posição já ocupada, escolha novamente.");
                        //contadorPosicoesNavio++;
                        naoCabe = true;
                        Console.WriteLine(contadorPosicoesNavio);

                        break;
                    }

                    if (navio.Alinhamento == 'H' && matriz[linhaEscolhida - 1, colun + contadorPosicoesNavio] == 'X')
                    {
                        Console.WriteLine(" Escolha outra posição.");
                        Console.WriteLine("  Tecle para continuar....");
                        //contadorPosicoesNavio++;
                        naoCabe = true;
                        Console.WriteLine(contadorPosicoesNavio);
                                             
                        break;
                    }

                    if (navio.Alinhamento == 'H' && matriz[linhaEscolhida - 1, colun + contadorPosicoesNavio] >= matriz.GetLength(1))
                    {
                        Console.WriteLine("Escolha outra posição.");
                        Console.WriteLine("  Tecle para continuar....");
                       // contadorPosicoesNavio++;
                        naoCabe = true;
                        Console.WriteLine(contadorPosicoesNavio);

                        break;
                    }
                    contadorPosicoesNavio--;

                } while ( (contadorPosicoesNavio > 0) || naoCabe == true);


                if (linhaEscolhida < 1 )
                {
                    Console.WriteLine(" Digite APENAS números ente 1 e 20!");
                    Console.WriteLine(" Tecle para continuar...");
                    // Console.ReadKey();
                    break;
                }

                if (linhaEscolhida >= 20)
                {
                    Console.WriteLine(" Digite APENAS números ente 1 e 20!");
                    Console.WriteLine(" Tecle para continuar...");
                    //Console.ReadKey();
                    break;
                }


            } while ((linhaEscolhida <= 0) || (linhaEscolhida > 20) || matriz[linhaEscolhida - 1 + contadorPosicoesNavio, colun] == 'X' || naoCabe == true);


            matriz[linhaEscolhida - 1, colun] = 'X';
            

            if (navio.Alinhamento == 'V')
            {
                contadorPosicoesNavio = navio.Tamanho - 1;

                do
                {
                    if (((linhaEscolhida - 1) + contadorPosicoesNavio) < matriz.GetLength(0))
                    {
                        if (matriz[(linhaEscolhida - 1) + contadorPosicoesNavio, colun] == '~')
                        {
                            matriz[(linhaEscolhida - 1) + contadorPosicoesNavio, colun] = 'X';
                            contadorPosicoesNavio--;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("  Escolha outra posição!");
                            Console.ResetColor();
                            break;
                        }
                    }
                    else
                    {

                    }


                } while (contadorPosicoesNavio > 0);
                deuCerto = true;

            }
            contadorPosicoesNavio = navio.Tamanho - 1;

            if (navio.Alinhamento == 'H')
            {


                do
                {   if(colun + contadorPosicoesNavio < matriz.GetLength(1)) 
                    matriz[(linhaEscolhida - 1), colun + contadorPosicoesNavio] = 'X';
                    contadorPosicoesNavio--;
                    deuCerto = true;

                } while (contadorPosicoesNavio > 0);
            }
            if(deuCerto)
                return true;
            else return false;
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