using ProjBatalhaNaval;

internal class Program
{
    private static void Main(string[] args)
    {
        char[,] campo1 = new char[20, 20];
        char[,] campo2 = new char[20, 20];
        char[] letras = new char[20] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T' };
        int colunaCampo = 0;
        int posicaoLinha = 0;

        bool acertou = false;


        Jogador jogador1 = new();
        char orientacaoJogador = jogador1.RetornarOrientacao();
        PortaAvioes portaAviao1 = new();
        portaAviao1.Alinhamento = orientacaoJogador;


      //  --------------------------------// NAO APAGAR//-------------------------------
        // Jogador jogador2 = new();
        //Jogador jogadorAtual = new();
       // jogadorAtual.Nome = jogador1.Nome;

        //string retornoDisparo = jogador1.Disparar();
       // char coordenadaDisparoColuna = retornoDisparo[0];
       // string coordenadaDisparoLinha = retornoDisparo.Substring(1); // DEU CERTO!!

       // Console.WriteLine("O retorno do disparo foi: " + retornoDisparo);
       // Console.WriteLine(" A coordenada da coluna foi: " + coordenadaDisparoColuna);
       // Console.WriteLine(" A coordenada da linha foi: " + coordenadaDisparoLinha);

        //  int numeroColunaTiro = buscarColuna(coordenadaDisparoColuna); // chama o metodo e passa a letra




        PreenchimentoInicialCampo(campo1);

        MostraCampoBatalha(campo1);

        Console.WriteLine("\nInforme a coluna que deseja colocar   seu navio: ");
        char capturaResposta = Console.ReadKey(true).KeyChar;
        colunaCampo = TransformaLetraDaColunaEmNumero(capturaResposta);
        ColocaNavioNaMAtriz(campo1, colunaCampo, portaAviao1);

        MostraCampoBatalha(campo1);




        //--------------------- O JOGADOR ATIRA!! É METODO  DO OBJETO JOGADOR!
        // --- BARCO TERIA METODO afunda() ... atingido()  etc.. bool verificarDestruido()
        //OBJETO TABULEIRO...verifica  se " de uagua" ou acertou" -- objeto tabuleiro retorna se tiro foi na agua  ou acertou
        // usuario passa posicao para o objeto tabuleiro!!!
        //tabuleiro retorna true se acertou algo..e false se deu agua

        // minha ideia: objeto jogador chama o tabuleiro  e passa  as coordenadas!!







        void MostraCampoBatalha(char[,] matriz)
        {
            Console.Write("   A    B    C    D    E    F    G    H");
            Console.Write("    I    J    K    L    M    N    O    P");
            Console.Write("    Q    R    S    T ");
            Console.WriteLine();
            int contador = 1;

            for (int linha = 1; linha <= 20; linha++)
            {

                contador++;
                for (int coluna = 1; coluna <= 20; coluna++)
                {

                    Console.Write(" |_" + campo1[linha - 1, coluna - 1] + "_");
                    if (coluna == 20)
                    {
                        Console.Write("|");
                        Console.WriteLine();
                    }
                }

            }
        }









        int TransformaLetraDaColunaEmNumero(char letraInformada)
        {
            int contador;
            char colunaDesejada;
            string aux;
            bool letraEncontrada = false;

            do
            {

                for (contador = 0; contador < letras.Length; contador++)
                {
                    if (letras[contador] == Char.ToUpper(letraInformada))
                    {
                        letraEncontrada = true;
                        break;
                    }

                }
                if (letraEncontrada = false)
                {
                    Console.WriteLine("Coluna não encontrada!");
                }
                /*else
                {
                    Console.WriteLine(" A coluna é: " + contador);
                    Console.ReadKey();
                }*/

                return contador;
                //esta retornando 20 pa qq letra q nao exista!!!   CONSERTAR


            } while (letraEncontrada = false);
        }



        void ColocaNavioNaMAtriz(char[,] matriz, int colun, Embarcacao navio)
        {
            //Console.Clear();

            int linhaEscolhida;
            string resposta;
            do
            {

                Console.WriteLine();
                Console.Write("\tInforme a linha desejada: ");
                //linhaEscolhida = int.Parse(Console.ReadLine());
                resposta = Console.ReadLine();
                if (!int.TryParse(resposta, out linhaEscolhida))
                {
                    Console.WriteLine("Digite APENAS numeros ( ente 1 e 20)!!!");
                    Console.WriteLine("Tecle para continuar....");
                    //Console.ReadKey();
                    Console.Clear();
                    MostraCampoBatalha(campo1);
                }

                if ((linhaEscolhida <= 0) || (linhaEscolhida > matriz.GetLength(0)))
                {
                    Console.Clear();
                    MostraCampoBatalha(campo1);
                    Console.WriteLine(" Valor incorreto. Não existe essa linha!");
                    // Console.Clear();
                    // MostraCampoBatalha(campo);
                }

            } while ((linhaEscolhida <= 0) || (linhaEscolhida > matriz.GetLength(0)));
            matriz[linhaEscolhida - 1, colun] = 'X'; // insere valor

            int contadorPosicoesNavio = navio.Tamanho - 1;

            if (navio.Alinhamento == 'V')
            {
                contadorPosicoesNavio = navio.Tamanho - 1;

                do
                {
                    if (matriz[(linhaEscolhida - 1) + contadorPosicoesNavio, colun] == '~')
                    {
                        matriz[(linhaEscolhida - 1) + contadorPosicoesNavio, colun] = 'X';
                        contadorPosicoesNavio--;
                    }
                    else
                    {
                        Console.WriteLine(" Escolha outra posicao!");
                    }





                } while (contadorPosicoesNavio > 0);
            }
            contadorPosicoesNavio = navio.Tamanho - 1;

            if (navio.Alinhamento == 'H')
            {
                

                do
                {


                    Console.Read();
                    matriz[(linhaEscolhida - 1),  colun + contadorPosicoesNavio] = 'X';
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
                    campo1[linha, coluna] = '~';
                }
            }
        }

        void ExibeMatrizPreenchida(char[,] matriz)
        {
            for (int linha = 0; linha < matriz.GetLength(0); linha++)
            {
                for (int coluna = 0; coluna < matriz.GetLength(1); coluna++)
                {
                    Console.Write("  " + campo1[linha, coluna] + " ");
                    if (coluna == 19)
                    {
                        Console.WriteLine();
                    }
                }
            }
        }
        //alterar para passar  a letra da coluna por parametro!!
        // tratar possiveis entradas incorretas !!!


        /*
            int buscarColuna()
            {
                int contador;
                char colunaDesejada;
                string aux;

                Console.WriteLine();
                Console.WriteLine("\tInforme a coluna desejada: ");
                aux = Console.ReadLine();
                colunaDesejada = aux[0];                

                for (contador = 0; contador < letras.Length; contador++)
                {
                    if (letras[contador] == Char.ToUpper(colunaDesejada))
                    {
                        Console.WriteLine(" o indice é: " + contador);

                        break;
                    }

                }
                Console.WriteLine(" A coluna é: " + contador);
                Console.ReadKey();
                return contador;

            }


        */


    }
}

