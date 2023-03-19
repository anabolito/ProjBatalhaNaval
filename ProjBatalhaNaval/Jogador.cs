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
            contadorDeVida = 9;  // é a soma dos tamanhos dos navios, se tomar 9 tiros perde ( 2 do submarino, 3 do destroyer e 4 do portaAviões
            this.Nome = "PLAYER " + numeroDoJogador;
            numeroDoJogador++;
            Console.WriteLine();
            
            Console.Write(" Informe o nome do jogador "+ numeroDoJogador+": ");
            this.Nome = Console.ReadLine();
            Console.WriteLine();
            Console.Clear();
        }


        public void DecrementaVida()  // metdo vai ser chamado pelo jogador na main qdo o adversario acertar tiro
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
            bool continua = false;

            
            Console.Write("\nJOGADOR " + this.Nome + " INFORME A COLUNA DA POSIÇÃO QUE DESEJA ATIRAR: ");
            coluna = Console.ReadKey(true).KeyChar;            
            colunaAlvo = char.ToUpper(coluna);
            Console.Write(colunaAlvo);
            int numeroDaColuna = TransformaLetraDaColunaEmNumero(colunaAlvo);



            do
            {
                Console.Write("\nINFORME A LINHA QUE DESEJA ATIRAR: ");
                aux = Console.ReadLine();


                
                if (!int.TryParse(aux, out linhaAlvo))
                {
                    Console.WriteLine("Informe APENAS numeros, entre 1 e 20!");
                    Thread.Sleep(450); // faz a mensagem ficar por 0.45 segundo !
                }
                else
                {
                    continua = true;
                }

            } while (continua == false);



             if (matriz[(linhaAlvo) - 1, numeroDaColuna] == '@' )
            {
                Console.WriteLine(" VOCÊ JÁ  DISPAROU NESSA COORDENADA ANTERIORMENTE!");
                return false;
            }

            if (matriz[(linhaAlvo)-1, numeroDaColuna] == 'X')
            {
                Console.WriteLine("\n  VOCÊ ACERTOU 1 ALVO!");
                matriz[(linhaAlvo) - 1, numeroDaColuna] = '@'; // marca local do tiro certeiro e evita que seja contabilizado 2 tiros no mesmo lugar
                
                return true;
            }
            else
            {
                Console.WriteLine("\n  VOCÊ ERROU O ALVO!");
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
