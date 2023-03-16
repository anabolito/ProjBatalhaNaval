using System;
using System.Collections.Generic;
using System.Linq;
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
            numeroDoJogador++;
            Console.WriteLine();
            Console.Write("\t\t\t\tInforme o nome do jogador"+ numeroDoJogador+": ");
            this.Nome = Console.ReadLine();
        }



        public string Disparar()
        {

            char colunaAlvo;
            string linhaAlvo;
            string alvo;
            char coluna;
            int linha;

            Console.WriteLine("Informe a COLUNA que deseja atirar: ");
            coluna = char.Parse(Console.ReadLine());
            colunaAlvo = char.ToUpper(coluna);

            Console.WriteLine("Informe a LINHA que deseja atirar: ");
            linhaAlvo = Console.ReadLine();
           // linhaAlvo = int.ToUpper(linha);

            string retorno = colunaAlvo  + linhaAlvo;
            char col = retorno[0];

            return retorno;
        }

        public char RetornarOrientacao()
        {
            char aux;
            char orientacao;
            do
            {
                Console.WriteLine(" Informe a orientação do navio: (V) - VERTICAL    (H) - HORIZONTAL");
                 aux = Console.ReadKey(true).KeyChar;
                orientacao = Char.ToUpper(aux); // alterado
            } while (orientacao != 'V' && orientacao != 'H');

            return orientacao;
            
        }



        int TransformaLetraDaColunaEmNumero()
        {

            char colunaDesejada;
            char letraColuna;
            string todasLetras = "ABCDEFGHIJKLMNOPQRST";
            int index = -1;

            Console.WriteLine("Informe a coluna para colocar a embarcação: ");
            letraColuna = Console.ReadKey(true).KeyChar;
            colunaDesejada = char.ToUpper(letraColuna);

            do
            {
                index = todasLetras.IndexOf(colunaDesejada);

                if (index < 0)
                {
                    Console.WriteLine("Coluna informada não foi localizada. Informe a coluna novamente!");
                }

            } while (index < 0);
            return index;
        }




        void ColocaNavioNaMatriz(char[,] matriz, int colun, Embarcacao navio)
        {

            int linhaEscolhida;
            string resposta;

            do
            {


                Console.WriteLine("\tInforme a linha desejada: ");
                resposta = Console.ReadLine();
                linhaEscolhida = int.Parse(resposta);
                if(matriz[linhaEscolhida, colun] == 'X')
                    Console.WriteLine(" Posição já preenchida! Escolha outra.");

                if (!int.TryParse(resposta, out linhaEscolhida))
                {
                    Console.WriteLine("Digite APENAS numeros ( ente 1 e 20)!!!");
                    Console.WriteLine("Tecle para continuar....");
                    Console.Clear();
                   // MostrarCampoDeBatalha(campoJogadorAtual);
                }

                if ((linhaEscolhida <= 0) || (linhaEscolhida > matriz.GetLength(0)))
                {
                    Console.Clear();
                   // MostrarCampoDeBatalha(campoJogadorAtual);
                    Console.WriteLine(" Valor incorreto. Não existe essa linha!");

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
                            Console.WriteLine(" Escolha outra posição!");
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

            /*
            int TransformaLetraDaColunaEmNumero()
            {
                int contadorLetras;
                char colunaDesejada;
                bool letraEncontrada = false;

                Console.WriteLine("Informe a coluna para colocar a embarcação: ");
                colunaDesejada = Console.ReadKey(true).KeyChar;

                do
                {

                    for (contadorLetras = 0; contadorLetras < letras.Length; contadorLetras++)
                    {
                        if (letras[contadorLetras] == Char.ToUpper(colunaDesejada))
                        {
                            letraEncontrada = true;
                            break;

                        }
                    }

                    if (!letras.Contains(colunaDesejada))
                    {
                        Console.WriteLine("Coluna informada não foi localizada. Informe a coluna novamente!");
                    }


                } while (letraEncontrada = false);
                return contadorLetras;  // com erro estava retornando 20!!
            }*/

        }

    }
}
