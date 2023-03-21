using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ProjBatalhaNaval
{
    internal class Jogador
    {
        private static int numeroDoJogador = 0;

        public string Nome { get; set; }
        public int ContadorDeVida { get; set; }

        public Jogador()
        {
            ContadorDeVida = 9;  
            this.Nome = "PLAYER " + numeroDoJogador;
            numeroDoJogador++;
            Console.WriteLine();

            Console.Write(" Informe o nome do jogador " + numeroDoJogador + ": ");
            this.Nome = Console.ReadLine();
            Console.WriteLine();
            Console.Clear();
        }

        public void DecrementaVida()
        {
            ContadorDeVida--;
        }

        public int RetornaVida()
        {
            return this.ContadorDeVida;
        }

        public bool Disparar(char[,] matriz)
        {
            char colunaAlvo;
            int linhaAlvo;
            char coluna;
            string aux;
            bool linhaInvalida = true;

            Console.Write("\n  JOGADOR " + this.Nome + "," + " INFORME A COLUNA DA POSIÇÃO QUE DESEJA ATIRAR: ");
            coluna = Console.ReadKey(true).KeyChar;
            colunaAlvo = char.ToUpper(coluna);
            int numeroDaColuna = TransformaLetraDaColunaEmNumero(colunaAlvo);

            do
            {
                Console.Write("\n\n  INFORME A LINHA QUE DESEJA ATIRAR: ");

                if (!int.TryParse(Console.ReadLine(), out linhaAlvo))
                {
                    Console.WriteLine("\n  Informe APENAS números, entre 1 e 20!");
                    Thread.Sleep(2000);
                }
                else
                {
                    if (linhaAlvo > 0 && linhaAlvo < 21)
                    {
                        linhaInvalida = false;
                    }
                    else
                    {
                        Console.WriteLine("\n  Informe APENAS números, entre 1 e 20!");
                        Thread.Sleep(2000);
                    }
                }
            } while (linhaInvalida);

            

            if (matriz[(linhaAlvo) - 1, numeroDaColuna] == '@')
            {
                Console.WriteLine("  VOCÊ JÁ  DISPAROU NESSA COORDENADA ANTERIORMENTE!");
                Thread.Sleep(2000);
                return false;
            }

            if (matriz[(linhaAlvo) - 1, numeroDaColuna] == 'X')
            {
                Console.WriteLine("\n  Você acertou 1 posição!");
                matriz[(linhaAlvo) - 1, numeroDaColuna] = '@';
                Thread.Sleep(2000);
                return true;
            }
            else
            {
                matriz[(linhaAlvo) - 1, numeroDaColuna] = 'A';
                Console.WriteLine("\n  Você errou o alvo!  DEU ÁGUA!!!");
                Thread.Sleep(2000);
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
            string todasLetras = "ABCDEFGHIJKLMNOPQRST";
            int index = -1;
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
    }
}
