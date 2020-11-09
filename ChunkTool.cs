using System;

namespace Corneroids
{
    public class ChunkTool
    {


        public static byte CheckBlocksAround(byte x, byte y, byte z,ref SpaceEntity entity, ref  ushort[,,] blocks) // local chunk pos
        {
            byte data = 0;

            if(x > 0)
            {
                if(blocks[x - 1, y, z] == 0)
                {
                    data += 16;
                }

            }
            else
            {
                
                data += 16;
            }
            // --------
            
            if (x < Chunk.SIZE - 1)
            {
                if (blocks[x + 1, y, z] == 0)
                {
                    data += 4;
                }

            }
            else
            {
                data += 4;
            }
            // -------

            if (y > 0)
            {
                if (blocks[x , y - 1, z] == 0)
                {
                    data += 2;
                }

            }
            else
            {
                data += 2;
            }
            // --------

            if (y < Chunk.SIZE - 1)
            {
                if (blocks[x , y + 1, z] == 0)
                {
                    data += 1;
                }

            }
            else
            {
                data += 1;
            }
            // -------
            if (z > 0)
            {
                if (blocks[x, y, z - 1] == 0)
                {
                    data += 8;
                }

            }
            else
            {
                data += 8;
            }
            // --------

            if (z < Chunk.SIZE - 1)
            {
                if (blocks[x, y, z + 1] == 0)
                {
                    data += 32;
                }

            }
            else
            {
                data += 32;
            }
            // -------



            return data;

        }


    }
}
