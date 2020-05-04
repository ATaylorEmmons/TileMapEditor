using ImageTools;
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
using TileMapEditor.TileEditor;


namespace TileMapEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TileSet tileSet;
        TileMap tileMap;

        public MainWindow()
        {
            InitializeComponent();
            FrameBuffer temp = new FrameBuffer(60, 60);

            tileSet = new TileSet(60, temp);
            DrawTiles();
        }

        public void create_newTileMap(object sender, RoutedEventArgs e)
        {
            NewTileMapWindow newTileMapWindow = new NewTileMapWindow();

            if(newTileMapWindow.ShowDialog() == true)
            {
                tileMap = newTileMapWindow.TileMap;
            }
        }

        public void DrawTiles()
        {
            Rectangle tile = new Rectangle();
            tile.Width = 64;
            tile.Height = 64;

            tile.Stroke = new SolidColorBrush(Colors.Beige);
            tile.StrokeThickness = 3;

            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap("Brick.png");

            FrameBuffer buffer = new FrameBuffer(bmp);

            tile.Fill = new ImageBrush(buffer.ImageSource);

            Canvas.SetLeft(tile, 0);
            Canvas.SetTop(tile, 0);

            canvas_TileView.Children.Add(tile);
        }

    }
}
