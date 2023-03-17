using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ProjBatalhaNaval
{
    internal class Jogador
    {
        private static int numeroDoJogador = 0;

        public string Nome { get; set; }
        

        public Jogador()
        {
            this.Nome = "PLAYER " + numeroDoJogador;
            numeroDoJogador++;
            Console.WriteLine();
            
            Console.Write(" Informe o nome do jogador "+ numeroDoJogador+": ");
            this.Nome = Console.ReadLine();
            Console.WriteLine();
            Console.Clear();
        }



        public bool Disparar(char[,] matriz)
        {

            char colunaAlvo;
            string linhaAlvo;            
            char coluna;
            

            Console.WriteLine("  Informe a COLUNA que deseja atirar: ");
            coluna = char.Parse(Console.ReadLine());
            colunaAlvo = char.ToUpper(coluna);
            int numeroDaColuna = TransformaLetraDaColunaEmNumero(colunaAlvo);

            Console.WriteLine("  Informe a LINHA que deseja atirar: ");
            linhaAlvo = Console.ReadLine();
            

            if (matriz[int.Parse(linhaAlvo)-1, numeroDaColuna] == 'X')
            {
                Console.WriteLine("  Você acertou 1 posição!");
                return true;
            }
            else
            {
                Console.WriteLine("  Você errou o alvo!");
            }

            return false;
        }



        public char RetornarOrientacao()
        {
            char aux;
            char orientacao;
            do
            {
                Console.Write("\n  Informe a orientação do navio: (V) - VERTICAL | (H) - HORIZONTAL: ");
                 aux = Console.ReadKey(true).KeyChar;
                orientacao = Char.ToUpper(aux);
                Console.WriteLine(orientacao);
                Console.WriteLine();
                
            } while (orientacao != 'V' && orientacao != 'H');

            return orientacao;
            
        }



        public int TransformaLetraDaColunaEmNumero()
        {

            char colunaDesejada;
            char letraColuna;
            string todasLetras = "ABCDEFGHIJKLMNOPQRST";
            int index = -1;

            Console.Write("\n  Informe a coluna para colocar a embarcação: ");
            letraColuna = Console.ReadKey(true).KeyChar;
            colunaDesejada = char.ToUpper(letraColuna);

            do
            {
                index = todasLetras.IndexOf(colunaDesejada);

                if (index < 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("  Coluna informada não foi localizada. Informe a coluna novamente!");
                }

            } while (index < 0);
            return index;
        }


        public int TransformaLetraDaColunaEmNumero(char letraColuna)
        {

            char colunaDesejada;
            //char letraColuna;
            string todasLetras = "ABCDEFGHIJKLMNOPQRST";
            int index = -1;

          //  Console.WriteLine("Informe a coluna para colocar a embarcação: ");
           // letraColuna = Console.ReadKey(true).KeyChar;
            colunaDesejada = char.ToUpper(letraColuna);

            do
            {
                index = todasLetras.IndexOf(colunaDesejada);

                if (index < 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("  Coluna informada não foi localizada. Informe a coluna novamente!");
                }

            } while (index < 0);
            return index;
        }




        public void ColocaNavioNaMatriz(char[,] matriz, int colun, Embarcacao navio)
        {

            int linhaEscolhida;
            string resposta;

            do
            {


                Console.WriteLine("\n  Informe a linha desejada: ");
                resposta = Console.ReadLine();
                linhaEscolhida = int.Parse(resposta);
                if(matriz[linhaEscolhida, colun] == 'X')
                    Console.WriteLine("  Posição já preenchida. Escolha outra posição!");

                if (!int.TryParse(resposta, out linhaEscolhida))
                {
                    Console.WriteLine("  Digite APENAS números ente 1 e 20!");
                    Console.WriteLine("  Tecle para continuar....");
                    Console.Clear();
                   // MostrarCampoDeBatalha(campoJogadorAtual);
                }

                if ((linhaEscolhida <= 0) || (linhaEscolhida > matriz.GetLength(0)))
                {
                    Console.Clear();
                   // MostrarCampoDeBatalha(campoJogadorAtual);
                    Console.WriteLine("  Valor incorreto. Não existe essa linha!");

                }

            } while ((linhaEscolhida <= 0) || (linhaEscolhida > matriz.GetLength(0)) || matriz[linhaEscolhida, colun] == 'X');


            matriz[linhaEscolhida - 1, colun] = 'X';


            int contadorPosicoesNavio = navio.Tamanho - 1;

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
                            Console.WriteLine("  Escolha outra posição!");
                            break;
                            
                        }
                    }
                    else{ }


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

    }
}
