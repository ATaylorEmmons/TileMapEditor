using ImageTools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileMapEditor.TileEditor
{

    public class TileSet
    {
        public int PixelSize { get; private set; }
        FrameBuffer FrameBuffer { get; set; }

        public TileSet(int pixelSize, FrameBuffer image)
        {
            PixelSize = pixelSize;
            FrameBuffer = image;
        }

        public FrameBuffer getTilePixels(int id)
        {
            FrameBuffer frameBuffer = new FrameBuffer(PixelSize, PixelSize);

            Color c;

            switch(id)
            {
                case 0:
                    c = Color.Green;
                    break;
                case 1:
                    c = Color.Brown;
                    break;
                case 2:
                    c = Color.Blue;
                    break;
                default:
                    c = Color.Beige;
                    break;
            }


            for(int i = 0; i < PixelSize; i++)
            {
                for(int j = 0; j < PixelSize; j++)
                {

                    frameBuffer.SetPixel(i, j, c);
                }
            }


            return frameBuffer;
        }
    }
}
