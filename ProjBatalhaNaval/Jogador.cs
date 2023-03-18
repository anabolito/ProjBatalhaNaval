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
        public int contadorDeVida { get; set; }

        public Jogador()
        {
            contadorDeVida = 9;  // é a soma dos tamanhos dos navios, se tomar 9 tiros perde
            this.Nome = "PLAYER " + numeroDoJogador;
            numeroDoJogador++;
            Console.WriteLine();

            Console.Write(" Informe o nome do jogador " + numeroDoJogador + ": ");
            this.Nome = Console.ReadLine();
            Console.WriteLine();
            Console.Clear();
        }

        public void DecrementaVida()  //chamar na main qdo o adversario acertar tiro
        {
            contadorDeVida--;
        }

        public int RetornaVida()
        {
            return this.contadorDeVida;
        }

        public bool Disparar(char[,] matriz)
        {
            char colunaAlvo;
            int linhaAlvo;
            char coluna;
            string aux;
            bool linhaInvalida = true;

            Console.Write("\nJOGADOR " + this.Nome + " INFORME A COLUNA DA POSIÇÃO QUE DESEJA ATIRAR: ");
            coluna = Console.ReadKey(true).KeyChar;
            colunaAlvo = char.ToUpper(coluna);
            int numeroDaColuna = TransformaLetraDaColunaEmNumero(colunaAlvo);

            do
            {
                Console.Write("\nINFORME A LINHA QUE DESEJA ATIRAR: ");

                if (!int.TryParse(Console.ReadLine(), out linhaAlvo))
                {
                    Console.WriteLine("Informe APENAS numeros, entre 1 e 20!");
                    Thread.Sleep(450);
                }
                else
                {
                    if (linhaAlvo > 0 && linhaAlvo < 21)
                    {
                        linhaInvalida = false;
                    }
                    else
                    {
                        Console.WriteLine("Informe APENAS numeros, entre 1 e 20!");
                        Thread.Sleep(450);
                    }
                }
            } while (linhaInvalida);

            if (matriz[(linhaAlvo) - 1, numeroDaColuna] == 'X')
            {
                Console.WriteLine("\n  Você acertou 1 posição!");
                matriz[(linhaAlvo) - 1, numeroDaColuna] = '@'; // marca local do tiro certeiro


                return true;
            }
            else
            {
                Console.WriteLine("\n  Você errou o alvo!");
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

            do
            {
                Console.WriteLine("\n  Informe a linha desejada: ");
                while (int.TryParse(Console.ReadLine(), out linhaEscolhida))
                {
                    Console.WriteLine($"  Digite APENAS números ente 1 e 20!" +
                        "  Pressione qualquer tecla para continuar....");
                    Console.ReadKey();
                    Console.Clear();
                    // MostrarCampoDeBatalha(campoJogadorAtual);
                }

                if (matriz[linhaEscolhida, colun] == 'X')
                {
                    Console.WriteLine("  Posição já preenchida. Escolha outra posição!");
                }
                else if ((linhaEscolhida <= 0) || (linhaEscolhida > matriz.GetLength(0)))
                {
                    Console.Clear();
                    // MostrarCampoDeBatalha(campoJogadorAtual);
                    Console.WriteLine("  Valor incorreto. Não existe essa linha!");
                }

            } while ((linhaEscolhida <= 0)
                     || (linhaEscolhida > matriz.GetLength(0))
                     || matriz[linhaEscolhida, colun] == 'X');

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
                    else { }


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
