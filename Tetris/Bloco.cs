using System;
using System.Collections.Generic;

namespace Tetris
{
    public abstract class Bloco
    {
        protected abstract Posicoes[][] Topo { get; }
        protected abstract Posicoes ComecoMovimento { get; }
        public abstract int Id { get; }

        private int Rotacoes;
        private Posicoes Movimento;

        public Bloco()
        {
            Movimento = new Posicoes(ComecoMovimento.Linhas, ComecoMovimento.Colunas);
        }

        public IEnumerable<Posicoes> PosicaoTopo()
        {
            foreach (Posicoes P in Topo[Rotacoes])
            {
                yield return new Posicoes(P.Linhas + Movimento.Linhas, P.Colunas + Movimento.Colunas);
            } 
        }
        public void RotacaoBloco()
        {
            Rotacoes = (Rotacoes+1) % Topo.Length;
        }
        public void RotacaoBlocoSH()
        {
            if(Rotacoes == 0)
            {
                Rotacoes = Topo.Length - 1;
            }
            else
            {
                Rotacoes--;
            }
        }
        public void Movimentacao(int Linhas,int Colunas)
        {
            Movimento.Linhas += Linhas;
            Movimento.Colunas += Colunas;
        }
        public void reset()
        {
            Rotacoes = 0;
            Movimento.Linhas = ComecoMovimento.Linhas;
            Movimento.Colunas = ComecoMovimento.Colunas;
        }



    }
}
