using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjBatalhaNaval
{
    internal abstract class Embarcacao
    {
        public int Tamanho { get; set; }

        public Embarcacao(int tamanho)
        {
            this.Tamanho = tamanho;
        }

        public string Disparar()
        {
            
            char colunaAlvo;
            char linhaAlvo;
            string alvo;
            Console.WriteLine("Informe a COLUNA que deseja atirar: ");
            colunaAlvo = char.Parse(Console.ReadLine());
          
            Console.WriteLine("Informe a LINHA que deseja atirar: ");
            linhaAlvo = char.Parse(Console.ReadLine());
          
            string retorno = colunaAlvo +" "+ linhaAlvo; // usaria primeiro caractere de retorno para char COLUNA e o terceiro caractere para a LINHA
            char col = retorno[0];
            
            return retorno;
        }    
    }
}
