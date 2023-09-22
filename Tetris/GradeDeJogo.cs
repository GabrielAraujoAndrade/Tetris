using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
   public class GradeDeJogo
    {
        private readonly int[,] grade;
        public Random Rand = new Random();
        public int Linhas { get; }
        public int Colunas { get; }

        public int this [int l,int c]
        {
            get => grade[l,c];
            set => grade[l,c] = value;
        }

        public GradeDeJogo(int L, int C)
        {
            Linhas = L;
            Colunas = C;
            grade = new int[L, C];
        }

        public bool Dentrodagrade (int l, int c)
        {
            return l >= 0 && l < Linhas && c >= 0 && c < Colunas;

        }
        public bool Foradagrade(int l, int c)
        {
            
            return Dentrodagrade(l, c) && grade[l,c] == 0;
            
        }
        public bool LinhaCheia(int l)
        {
            for (int c = 0; c < Colunas; c++)
            {
                if (grade[l, c] == 0)
                    return false;

            }
            return true;
        }
        public bool LinhaVazia(int l)
        {

            for (int c = 0; c < Colunas; c++)
            {
                if (grade[l, c] != 0)
                    return false;

            }
            return true;
        }
        public void LimpaLinha(int l)
        {
            for (int c = 0; c < Colunas; c++)
            {
                grade[l, c] = 0;
            }
        }
        public void DesceLinha(int l,int NumL)
        {
            for (int c = 0; c < Colunas; c++)
            {
                grade[l+NumL, c] = grade[l,c];
                grade[l, c] = 0;
            }

        }
        public int LimpartodasasLinhas()
        {
            int Limpador = 0;

            for (int l = Linhas-1; l >=0; l--)
            {
                if (LinhaCheia(l))
                {
                    LimpaLinha(l);
                    Limpador++;
                }
                else if(Limpador > 0)
                {
                    DesceLinha(l, Limpador);
                }
            }

            return Limpador;
        }

    }
}
