using ImageTools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileMapEditor.TileEditor
{
    public class TileMap
    {
        int Width { get; set; }
        int Height { get; set; }
        int TileSize { get; set; }
        public TileSet TileSet { get; set; }

        uint[,] tiles;

        public TileMap(int width, int height, int tileSize)
        {
            Width = width;
            Height = height;
            TileSize = tileSize;

            tiles = new uint[width, height];
        }

        public void SetTile(uint x, uint y, uint type)
        {
            tiles[x, y] = type;
        }

        public Bitmap getBitmap(TileSet set) 
        {
            FrameBuffer frameBuffer = new FrameBuffer(Width * set.PixelSize, Height * set.PixelSize);


            return frameBuffer.Bitmap;
        }
    }
}
