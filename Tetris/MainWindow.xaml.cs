using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tetris
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ImageSource[] TopoImages = new ImageSource[]
      {
            new BitmapImage(new Uri("Figuras/TileEmpty.png", UriKind.Relative)),
            new BitmapImage(new Uri("Figuras/TileCyan.png", UriKind.Relative)),
            new BitmapImage(new Uri("Figuras/TileBlue.png", UriKind.Relative)),
            new BitmapImage(new Uri("Figuras/TileOrange.png", UriKind.Relative)),
            new BitmapImage(new Uri("Figuras/TileYellow.png", UriKind.Relative)),
            new BitmapImage(new Uri("Figuras/TileGreen.png", UriKind.Relative)),
            new BitmapImage(new Uri("Figuras/TilePurple.png", UriKind.Relative)),
            new BitmapImage(new Uri("Figuras/TileRed.png", UriKind.Relative))
      };

        private readonly ImageSource[] BlocosImages = new ImageSource[]
        {
            new BitmapImage(new Uri("Figuras/Block-Empty.png", UriKind.Relative)),
            new BitmapImage(new Uri("Figuras/Block-I.png", UriKind.Relative)),
            new BitmapImage(new Uri("Figuras/Block-J.png", UriKind.Relative)),
            new BitmapImage(new Uri("Figuras/Block-L.png", UriKind.Relative)),
            new BitmapImage(new Uri("Figuras/Block-O.png", UriKind.Relative)),
            new BitmapImage(new Uri("Figuras/Block-S.png", UriKind.Relative)),
            new BitmapImage(new Uri("Figuras/Block-T.png", UriKind.Relative)),
            new BitmapImage(new Uri("Figuras/Block-Z.png", UriKind.Relative))
        };

        private readonly Image[,] ControledeImagens;
        private EstadodeJogo estadodejogo = new EstadodeJogo();
        private readonly int delaymaximo = 200;
        private readonly int delayminimo = 25;
        private readonly int quedadedelay = 5;
        private bool pause = false;


        public MainWindow()
        {
            InitializeComponent();
            ControledeImagens = ConfiguracaoTela(estadodejogo.gradedejogo);
        }

        private Image[,] ConfiguracaoTela(GradeDeJogo grade)
        {
            Image[,] ControledeImagens = new Image[grade.Linhas, grade.Colunas];
            int pixel = 25;

            for (int l = 0; l < grade.Linhas; l++)
            {
                for (int c = 0; c < grade.Colunas; c++)
                {
                    Image ControledeImagem = new Image
                    {
                        Height = pixel,
                        Width = pixel
                    };
                    Canvas.SetTop(ControledeImagem, (l - 2) * pixel + 10);
                    Canvas.SetLeft(ControledeImagem, c * pixel);
                    QuadradodoJogo.Children.Add(ControledeImagem);
                    ControledeImagens[l,c] = ControledeImagem;
                }

            }
            return ControledeImagens;
        }

        private void DesenharBlocoSegurado(Bloco Segurado)
        {
            if(Segurado == null)
            {
                BlocoSegurado.Source = BlocosImages[0];
            }
            else
            {
                BlocoSegurado.Source = BlocosImages[Segurado.Id];
            }

        }

        private void DesenharGrade(GradeDeJogo grade)
        {

            for (int l = 0; l < grade.Linhas; l++)
            {
                for (int c = 0; c < grade.Colunas; c++)
                {
                    int id = grade[l,c];
                    ControledeImagens[l, c].Opacity = 1;
                    ControledeImagens[l, c].Source = TopoImages[id];
                }

            }
         
        }

        private void BlocoAtual(Bloco bloco)
        {
            foreach (Posicoes p in bloco.PosicaoTopo())
            {
                ControledeImagens[p.Linhas, p.Colunas].Opacity = 1;
                ControledeImagens[p.Linhas, p.Colunas].Source = TopoImages[bloco.Id];
            } 

        }

        private void ProximoBlocoDesenho(FiladeBlocos fila)
        {
            Bloco proximo = fila.ProximoBloco;
            ProximaImagem.Source = BlocosImages[proximo.Id];

        }

        private void BlocoFantasma(Bloco bloco)
        {
            int fantasma = estadodejogo.DistanciadeDrop();

            foreach (Posicoes p in bloco.PosicaoTopo())
            {
                ControledeImagens[p.Linhas + fantasma, p.Colunas].Opacity = 0.15;
                ControledeImagens[p.Linhas + fantasma, p.Colunas].Source = TopoImages[bloco.Id];
            }


        }

        private void Desenho(EstadodeJogo Jogo)
        {
            DesenharGrade(Jogo.gradedejogo);
            BlocoFantasma(Jogo.BlocoAtual);
            BlocoAtual(Jogo.BlocoAtual);
            ProximoBlocoDesenho(estadodejogo.FiladeBlocos);
            DesenharBlocoSegurado(estadodejogo.BlocoSegurado);
            Placar.Text = $"Placar: {estadodejogo.Pontuacao}";

        }

        private async Task Loop()
        {
            Desenho(estadodejogo);

            while (!estadodejogo.Fimdejogo)
            {
                if (pause)
                {
                    // Se o jogo estiver pausado, aguarde um curto período e continue o loop.
                    await Task.Delay(1);
                    continue;
                }
                  Desenho(estadodejogo);
                int delay = Math.Max(delayminimo, delaymaximo - (estadodejogo.Pontuacao * quedadedelay));
                await Task.Delay(delay);
                estadodejogo.DescerBloco();
             
            }

            FimdeJogoMenu.Visibility = Visibility.Visible;
            PontuacaoFinal.Text = $"Placar Final: {estadodejogo.Pontuacao}";
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (estadodejogo.Fimdejogo)
            {
                return;
            }
            if (e.Key == Key.Escape)
            {
                pause = !pause;
                Pause.Visibility = pause ? Visibility.Visible : Visibility.Hidden;
                return;  
            }

   
            if (pause)
                return;



            switch (e.Key)
            {
                case Key.Left:
                    if(!pause)
                    estadodejogo.BlocoparaEsquerda();
                    break;
                    case Key.Right:
                    if (!pause)
                        estadodejogo.Blocoparadireita();
                    break;
                    case Key.Down:
                    if (!pause)
                        estadodejogo.DescerBloco();
                    break;
                case Key.Up:
                    if (!pause)
                        estadodejogo.RotacaoBloco();
                  break;
                case Key.LeftCtrl:
                    if (!pause)
                        estadodejogo.RotacaoSH();
                    break;
                case Key.LeftShift:
                    if (!pause)
                        estadodejogo.SegurarBloco();
                    break;
                    case Key.Space:
                    if (!pause)
                        estadodejogo.DroparBloco();
                    break;
            
                default:
                    return;

            }
            Desenho(estadodejogo);
        }

        private async void QuadradodoJogo_Loaded(object sender, RoutedEventArgs e)
        {
            await Loop();
      
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void JoguedeNovo_Click(object sender, RoutedEventArgs e)
        {
            estadodejogo = new EstadodeJogo();
            FimdeJogoMenu.Visibility = Visibility.Hidden;
            await Loop();

        }

        private  void VoltaraoJogo_Click(object sender, RoutedEventArgs e)
        {
            pause = false; 
            Pause.Visibility = Visibility.Hidden;
  

        }

    }
}
