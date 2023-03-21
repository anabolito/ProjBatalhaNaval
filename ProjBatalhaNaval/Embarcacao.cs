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
        public char Alinhamento { get; set; }

        public string Nome { get; set; }

        public int contadorVida { get; set; }

        public Jogador Piloto { get; set; } 

        public Embarcacao()
        {
            
        }

        
    }
}