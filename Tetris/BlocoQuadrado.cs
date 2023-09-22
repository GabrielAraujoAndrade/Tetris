using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class BlocoQuadrado: Bloco
    {
        private readonly Posicoes[][] Topos = new Posicoes[][]
       {
            new Posicoes[] {new(0,0),new(0,1), new(1,0), new(1,1)}
       };
        public override int Id => 4;
        protected override Posicoes ComecoMovimento => new Posicoes(0,4);

        protected override Posicoes[][] Topo => Topos;

    }
}
