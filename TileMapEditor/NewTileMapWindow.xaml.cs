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
using System.Windows.Shapes;
using TileMapEditor.TileEditor;

namespace TileMapEditor
{
    /// <summary>
    /// Interaction logic for NewTileMapWindow.xaml
    /// ToDo: Only numbers are valid for inputs.
    /// </summary>
    public partial class NewTileMapWindow : Window
    {
        public TileMap TileMap { get; }

        public NewTileMapWindow()
        {
            InitializeComponent();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            int tileSize = Int32.Parse(textbox_TileWidth.Text);
            int width = Int32.Parse(textbox_MapWidth.Text);
            int height = Int32.Parse(textbox_MapHeight.Text);

            TileMap map = new TileMap(width, height, tileSize);

            this.DialogResult = true;
        }
    }
}
