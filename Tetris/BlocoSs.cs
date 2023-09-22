using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    internal class BlocoSs:Bloco
    {
        private readonly Posicoes[][] Topos = new Posicoes[][]
      {
            new Posicoes[] {new(0,1),new(0,2), new(1,0), new(1,1)},
            new Posicoes[] {new(0,1),new(1,1), new(1,2), new(2,2)},
            new Posicoes[] {new(1,1),new(1,2), new(2,0), new(2,1)},
            new Posicoes[] {new(0,0),new(1,0), new(1,1), new(2,1)},
      };
        public override int Id => 5;
        protected override Posicoes ComecoMovimento => new Posicoes(0, 3);

        protected override Posicoes[][] Topo => Topos;


    }
}
