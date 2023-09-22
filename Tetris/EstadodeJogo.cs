using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class EstadodeJogo
    {
        private Bloco blocoatual;
        public Random random=new Random();
        public Bloco BlocoSegurado { get; private set; }
        public bool Blocoocupado { get; private set; }
        public Bloco BlocoAtual
        {

            get => blocoatual; 
            private set
            {
                blocoatual = value;
                blocoatual.reset();

                for (int i = 0; i < 2; i++)
                {
                    blocoatual.Movimentacao(1, 0);

                    if (!PosicaoBloco())
                    {
                        blocoatual.Movimentacao(-1,0);
                    }

                }
            }
        }

        public GradeDeJogo gradedejogo { get; }

        public FiladeBlocos FiladeBlocos { get; }

        public int Pontuacao { get; set; } 

        public bool Fimdejogo { get; private set; }

        public EstadodeJogo()
        {
            gradedejogo= new GradeDeJogo(22,10);
            FiladeBlocos = new FiladeBlocos();
            BlocoAtual = FiladeBlocos.Atualizar();
            Blocoocupado = true;
        }

        public void SegurarBloco()
        {
            if(!Blocoocupado)
            {
                return;
            }

            if(BlocoSegurado == null)
            {
                BlocoSegurado = BlocoAtual;
                BlocoAtual = FiladeBlocos.Atualizar();
            }
            else
            {
                Bloco temp = BlocoAtual;
                BlocoAtual = BlocoSegurado;
                BlocoSegurado = temp;
            }
            Blocoocupado=false;
        }
        
        private bool PosicaoBloco()
        {
            foreach (Posicoes p in BlocoAtual.PosicaoTopo() )
            {
                if (!gradedejogo.Foradagrade(p.Linhas, p.Colunas))
                {
                    return false;

                }
               
            }
                 return true;
        }

        public void RotacaoBloco()
        { 
            BlocoAtual.RotacaoBloco();
            if (!PosicaoBloco())
            {
                BlocoAtual.RotacaoBloco();
            }

        }
        public void RotacaoSH()
        {
            BlocoAtual.RotacaoBlocoSH();
            if (!PosicaoBloco())
            {
                BlocoAtual.RotacaoBlocoSH();
            }

        }

        public  void BlocoparaEsquerda()
        {
            BlocoAtual.Movimentacao(0, -1);
            if (!PosicaoBloco())
            {

                BlocoAtual.Movimentacao(0,1);
            }

        }
        public void Blocoparadireita()
        {


            BlocoAtual.Movimentacao(0, 1);
            if (!PosicaoBloco())
            {

                BlocoAtual.Movimentacao(0, -1);
            }
        }

        private bool fimdejogo()
        {
            return !(gradedejogo.LinhaVazia(0) && gradedejogo.LinhaVazia(1));
        }
        private void ColocarBloco()
        {
            foreach (Posicoes p  in BlocoAtual.PosicaoTopo())
            {
                gradedejogo[p.Linhas, p.Colunas] = blocoatual.Id;
            }

            Pontuacao+= gradedejogo.LimpartodasasLinhas();
          
     
            if (fimdejogo())
            {
                Fimdejogo = true;
            }
            else
            {
                BlocoAtual = FiladeBlocos.Atualizar();
                Blocoocupado = true;
            }
        }
        public void DescerBloco()
        {
            BlocoAtual.Movimentacao(1, 0);

            if (!PosicaoBloco())
            {
                BlocoAtual.Movimentacao(-1, 0);
                ColocarBloco();
            }

        }

        private int DropdoTopo(Posicoes p)
        {
            int drop = 0;

            while (gradedejogo.Foradagrade(p.Linhas + drop + 1, p.Colunas))
            {
                drop++;
            }

            return drop;
        }

        public int DistanciadeDrop()
        {
            int drop = gradedejogo.Linhas;

            foreach (Posicoes p in BlocoAtual.PosicaoTopo())
            {
                drop = System.Math.Min(drop, DropdoTopo(p));
            }

            return drop;
        }

        public void DroparBloco()
        {
            BlocoAtual.Movimentacao(DistanciadeDrop(), 0);
            ColocarBloco();
        }

    }
}
