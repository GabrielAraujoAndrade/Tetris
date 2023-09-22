using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class FiladeBlocos
    {
        private readonly Bloco[] blocos = new Bloco[]
            {   new BlocoI(), 
                new BlocoJ(),
                new BlocoL(),
                new BlocoQuadrado(),
                new BlocoSs(),
                new BlocoT(),
                new BlocoZ()
            };
        private readonly Random random = new Random();
        
        public Bloco ProximoBloco { get; private set; }

        public FiladeBlocos()
        {
            ProximoBloco = BlocoAleatorio();

        }
        private Bloco BlocoAleatorio()
        {

            return blocos[random.Next(blocos.Length)];
        }
        public Bloco Atualizar()
        {

            Bloco Atualizar = ProximoBloco;
            do
            {
                ProximoBloco = BlocoAleatorio();

            }while(Atualizar.Id == ProximoBloco.Id);

            return Atualizar;
        }

    }
}
