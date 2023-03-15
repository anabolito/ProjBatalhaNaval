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

        public string Nome { get; set; }

        public Jogador()
        {
            Console.WriteLine("Informe o nome do jogador: ");
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
            char orientacao;
            do
            {
                Console.WriteLine(" Informe a orientação do navio: (V) - VERTICAL    (H) - HORIZONTAL");
                 orientacao = char.Parse(Console.ReadLine().ToUpper());
            }while (orientacao != 'V' && orientacao != 'H');

            return orientacao;
            
        }

    }
}
