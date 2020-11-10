using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Corneroids
{
    public class SpaceEntity 
    {
        public const byte SIZE = 32; // 48 MAX
        public const byte SIZE_HALF = SIZE / 2;

        public uint BlocksCount { get; set; }
        public ulong Mass { get; set; }
        public Transform transform { get; set; }

        private Chunk[,,] _chunks = new Chunk[SIZE, SIZE, SIZE];
        private byte[,,] chunksData = new byte[SIZE, SIZE, SIZE];
        private List<Chunk> chunksToDraw = new List<Chunk>();
        private List<Vector3Sbyte> existingChunks = new List<Vector3Sbyte>();

     
        private Vector3 worldPos;
        

        public SpaceEntity(Vector3 worldPos)
        {
            transform = new Transform();
            this.worldPos = worldPos;
          
        }

        private Chunk GetChunk(sbyte localX, sbyte localY, sbyte localZ)
        {
            return _chunks[localX + SIZE_HALF, localY + SIZE_HALF, localZ + SIZE_HALF];
        }
        private Chunk GetChunk(Vector3Sbyte localPos)
        {
            return _chunks[localPos.x + SIZE_HALF, localPos.y + SIZE_HALF, localPos.z + SIZE_HALF];
        }
        private void SetChunk(sbyte localX, sbyte localY, sbyte localZ, Chunk chunk)
        {
            
            _chunks[localX + SIZE_HALF, localY + SIZE_HALF, localZ + SIZE_HALF] = chunk;
        }

        private byte GetChunkData(sbyte localX, sbyte localY, sbyte localZ)
        {
            return chunksData[localX + SIZE_HALF, localY + SIZE_HALF, localZ + SIZE_HALF];
        }
        private void SetChunkData(sbyte localX, sbyte localY, sbyte localZ, byte state)
        {

            chunksData[localX + SIZE_HALF, localY + SIZE_HALF, localZ + SIZE_HALF] = state;
        }

        public void AddChunk(sbyte x, sbyte y, sbyte z)
        {
            if (ChunkInAsteroid(x, y, z) == false) return;

            SetChunkData(x,y,z,1);
            existingChunks.Add(new Vector3Sbyte(x,y,z));
        }

        public void CreateChunks()
        {
            foreach (Vector3Sbyte chunkData in existingChunks)
            {
                CreateChunk(chunkData.x, chunkData.y, chunkData.z);
            }

            foreach(Vector3Sbyte chunkData in existingChunks)
            {
                GetChunk(chunkData).CreateStructure();
            }
        }

        public bool CheckBlockExist(Vector3Sbyte chunkPos, Vector3Byte blockPos, out ushort blockID)
        {
            blockID = 0;

            if (GetChunk(chunkPos.x, chunkPos.y, chunkPos.z) != null)
            {
                blockID = GetChunk(chunkPos.x, chunkPos.y, chunkPos.z).GetBlockID(blockPos);

                if(blockID > 0) return true;
                else return false;

            }
            
            return false;
        }

        private void CreateChunk(sbyte x, sbyte y, sbyte z) 
        {

            Vector3 localChunkPos = ChunkPositionToLocal(x,y,z);

            Vector3 world = worldPos + localChunkPos;

            SetChunk(x,y,z,new Chunk(world, new Vector3Sbyte(x,y,z), this));

            chunksToDraw.Add(GetChunk(x, y, z));
        }

        public bool ChunkExist(sbyte x, sbyte y, sbyte z)
        {

            if (ChunkInAsteroid(x, y, z) == false)
                return false;

            if (GetChunkData(x,y,z) == 1)
                return true;

            return false;
        }
        public bool ChunkExist(Vector3Sbyte local)
        {

            if (ChunkInAsteroid(local.x, local.y, local.z) == false)
                return false;

            if (GetChunkData(local.x, local.y, local.z) == 1)
                return true;

            return false;
        }

        private bool ChunkInAsteroid(sbyte x, sbyte y, sbyte z)
        {
            if (x >= SIZE_HALF || y >= SIZE_HALF || z >= SIZE_HALF)
                return false;
            if(x <= -SIZE_HALF || y <= -SIZE_HALF || z <= -SIZE_HALF)
                return false;


            return true;
        }

        private Vector3 ChunkPositionToLocal(sbyte x, sbyte y, sbyte z)
        {
            
            Vector3 position = new Vector3(x * Chunk.SIZE, y * Chunk.SIZE, z * Chunk.SIZE);

            return position;
        }


        public void Draw()
        {
            //Rotation = new Vector3(0, 45, 0);


            for (ushort i = 0; i < chunksToDraw.Count; i++) // 32 max or Int
            {
                if (Vector3.Distance(chunksToDraw[i].chunkPosition, Engine.camera.Position) <= Settings.viewDistance)
                {
                    chunksToDraw[i].DrawChunk();
                   
                }
                
            }
        }

    }
}
