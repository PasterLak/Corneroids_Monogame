using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Corneroids
{
    public class Resources
    {


        public static Texture2D LoadTexture2D(string link)
        {
            FileStream fs = new FileStream(link ,FileMode.Open);
            Texture2D texture = Texture2D.FromStream(Engine.graphics.GraphicsDevice, fs);
            
            fs.Dispose();
            return texture;

        }

        public static Vector2[] GetBlockTexureCoord(byte posX, byte posY, byte blockSizeInPixels = 32)
        {
            Vector2[] pos = new Vector2[4];

            float step = 1f / (Chunk.texture.Height / blockSizeInPixels);

            pos[0] = new Vector2(posX * step, posY * step);
            pos[1] = new Vector2(posX * step, posY * step + step);
            pos[2] = new Vector2(posX * step + step, posY * step + step);
            pos[3] = new Vector2(posX * step + step, posY * step);

            return pos;
        }

        public static Texture2D GetBlockTexure( byte posX, byte posY, byte blockSizeInPixels = 32)
        {
            Texture2D block = new Texture2D(Engine.graphicsDevice, blockSizeInPixels, blockSizeInPixels);

            Color[] atlasColors = new Color[Chunk.texture.Width * Chunk.texture.Height];
            Color[,] color = new Color[blockSizeInPixels, blockSizeInPixels];
            //Color[,] color = new Color[blockSizeInPixels, blockSizeInPixels];
            Chunk.texture.GetData(atlasColors);

            for (byte x = 0; x < blockSizeInPixels; x++)
            {
                for (byte y = 0; y < blockSizeInPixels; y++)
                {
                    color[x, y] = atlasColors[(x+ posX) + (y + posY) * Chunk.texture.Width];
                }
            }
            atlasColors = new Color[blockSizeInPixels * blockSizeInPixels];

            for (byte x = 0; x < blockSizeInPixels; x++)
            {
                for (byte y = 0; y < blockSizeInPixels; y++)
                {
                    atlasColors[1024 - x * y] = color[x, y];
                }
            }

            block.SetData(atlasColors);

            return block;
        }
       
    }
}
