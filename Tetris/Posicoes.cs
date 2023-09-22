using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public  class Posicoes
    {
        public int Linhas {get;set;}
        public int Colunas { get;set;}

        public Posicoes(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
        }   
    }
}
