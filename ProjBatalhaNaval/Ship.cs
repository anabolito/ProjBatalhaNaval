using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjBatalhaNaval
{
    public abstract class Ship
    {
        public int Size { get; set; }
        public char Alignment { get; set; }

        public Ship()
        {
            
        }

        public void ChooseAlignment(char a)
        {
            this.Alignment = a;
        }

        public override string ToString()
        {
            return $"Tamanho:  {this.Size}\nAlinhamento: {this.Alignment}";
        }
    }
}
