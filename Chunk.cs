
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Corneroids
{
    public sealed class Chunk
    { 
        
        public const byte SIZE = 32;
        public static Texture2D texture;
        private SpaceEntity spaceEntity;
        
        public Vector3 chunkPosition { get; private set; }
        public Vector3Sbyte chunkLocalPosition { get; private set; }

        private ushort[,,] blocks = new ushort[SIZE, SIZE, SIZE]; // 0 = no; 1+ = id

        private Vector3 pos = Vector3.Zero;
        
        // Mesh data
        private byte[,,] sides = new byte[SIZE, SIZE, SIZE];

        private GraphicsDevice graphics;
        private int count = 0;
        private List<VertexPositionTexture> verts = new List<VertexPositionTexture>();
        private List<short> triangles = new List<short>();
     
        private VertexBuffer vertexBuffer;
        private BasicEffect effect;
        private IndexBuffer indexBuffer;
        public BoundingBox boundingBox;

        private Camera camera;


        public Chunk(Vector3 worldPos, Vector3Sbyte chunkLocalPosition,  SpaceEntity spaceEntity)
        {
            chunkPosition = worldPos; 
            this.spaceEntity = spaceEntity;
            this.graphics = Engine.graphicsDevice;
            this.chunkLocalPosition = chunkLocalPosition;
            this.camera = Engine.camera;

            //Testing.Start();
            CreateData();

            
            //Testing.End();
        }

        public void CreateStructure()
        {
            FillSidesData();
            CreateBlocks();
            CreateMesh();
        }

        public ushort GetBlockID(byte x, byte y, byte z)
        {
            return blocks[x,y,z];
        }
        public ushort GetBlockID(Vector3Byte posInChunk)
        {
            return blocks[posInChunk.x, posInChunk.y, posInChunk.z];
        }

        public void CreateData()
        {
            for(byte x = 0; x < SIZE; x++)
            {
                for (byte y = 0; y < SIZE; y++)
                {
                    for (byte z = 0; z < SIZE; z++)
                    {
                        // id
                        blocks[x, y, z] = 1;
                        spaceEntity.BlocksCount++;
                        spaceEntity.Mass += 10;
                    }
                }
            }

            
        }

        private void FillSidesData()
        {
            for (byte x = 0; x < SIZE; x++)
            {
                for (byte y = 0; y < SIZE; y++)
                {
                    for (byte z = 0; z < SIZE; z++)
                    {
                        if(blocks[x,y,z] > 0)
                        sides[x, y, z] = CheckBlocksAround(x, y, z );
                        

                    }

                }

            }

        }
        private void CreateBlocks()
        {
            
            for (byte x = 0; x < SIZE; x++)
            {
                for (byte y = 0; y < SIZE; y++)
                {
                    for (byte z = 0; z < SIZE; z++)
                    {
                        
                        if (blocks[x, y, z] > 0)
                        {
                            //pos = new Vector3(x - Data.BLOCK_CENTER, y - Data.BLOCK_CENTER, z - Data.BLOCK_CENTER);
                            pos = new Vector3(x, y, z) + chunkPosition;
                            

                            GenerateBlock(sides[x, y, z], blocks[x,y,z]);

                            
                        }


                    }

                }

            }
            
        }

        private void GenerateBlock(byte sides, ushort blockID)
        {
            Vector2[] uv = Resources.GetBlockTexureCoord((byte)Engine.world.random.Next(10), 0);


            if (sides >= 32)
            {
                GenerateSide(0, ref uv);
                sides -= 32;
            }
            if (sides >= 16)
            {
                GenerateSide(1, ref uv);
                sides -= 16;
            }
            if (sides >= 8)
            { 
                GenerateSide(2, ref uv);
                sides -= 8;
            }
            if (sides >= 4)
            {
                GenerateSide(3, ref uv);
                sides -= 4;
            }
            if (sides >= 2)
            {
                GenerateSide(4, ref uv);
                sides -= 2;
            }
            if (sides == 1)
            {
                GenerateSide(5, ref uv);
                
            }

        }

        private void GenerateSide(byte sideID, ref Vector2[] uv)
        {

            

            switch (sideID)
            {
                case 0:

                    this.verts.Add(new VertexPositionTexture( Voxel.point[4] + pos, uv[0] ));
                    this.verts.Add(new VertexPositionTexture( Voxel.point[5] + pos, uv[1] ));
                    this.verts.Add(new VertexPositionTexture( Voxel.point[6] + pos, uv[2] ));
                    this.verts.Add(new VertexPositionTexture( Voxel.point[7] + pos, uv[3] ));

                    break;
                case 1 :

                    this.verts.Add(new VertexPositionTexture(Voxel.point[0] + pos, uv[0] ));
                    this.verts.Add(new VertexPositionTexture(Voxel.point[1] + pos, uv[1] ));
                    this.verts.Add(new VertexPositionTexture(Voxel.point[5] + pos, uv[2] ));
                    this.verts.Add(new VertexPositionTexture(Voxel.point[4] + pos, uv[3] ));

                    break;
                case 2:

                    this.verts.Add(new VertexPositionTexture(Voxel.point[3] + pos, uv[0] ));
                    this.verts.Add(new VertexPositionTexture(Voxel.point[2] + pos, uv[1] ));
                    this.verts.Add(new VertexPositionTexture(Voxel.point[1] + pos, uv[2] ));
                    this.verts.Add(new VertexPositionTexture(Voxel.point[0] + pos, uv[3] ));

                    break;
                case 3:

                    this.verts.Add(new VertexPositionTexture(Voxel.point[7] + pos, uv[0]));
                    this.verts.Add(new VertexPositionTexture(Voxel.point[6] + pos, uv[1]));
                    this.verts.Add(new VertexPositionTexture(Voxel.point[2] + pos, uv[2]));
                    this.verts.Add(new VertexPositionTexture(Voxel.point[3] + pos, uv[3]));

                    break;
                case 4:

                    this.verts.Add(new VertexPositionTexture(Voxel.point[0] + pos, uv[0]));
                    this.verts.Add(new VertexPositionTexture(Voxel.point[4] + pos, uv[1]));
                    this.verts.Add(new VertexPositionTexture(Voxel.point[7] + pos, uv[2]));
                    this.verts.Add(new VertexPositionTexture(Voxel.point[3] + pos, uv[3]));

                    break;
                case 5:

                    this.verts.Add(new VertexPositionTexture(Voxel.point[5] + pos, uv[0]));
                    this.verts.Add(new VertexPositionTexture(Voxel.point[1] + pos, uv[1]));
                    this.verts.Add(new VertexPositionTexture(Voxel.point[2] + pos, uv[2]));
                    this.verts.Add(new VertexPositionTexture(Voxel.point[6] + pos, uv[3]));

                    break;
                

            }

            triangles.Add((short)(0 + count * 4));
            triangles.Add((short)(1 + count * 4));
            triangles.Add((short)(2 + count * 4));
            triangles.Add((short)(2 + count * 4));
            triangles.Add((short)(3 + count * 4));
            triangles.Add((short)(0 + count * 4));

            count++;

        }

       

        private void CreateMesh()
        {
            
            vertexBuffer = new VertexBuffer
             (
                 graphics , typeof(VertexPositionTexture),
                 verts.Count, BufferUsage.WriteOnly
             );
            
            vertexBuffer.SetData(verts.ToArray());
            
            effect = new BasicEffect(graphics);

            effect.TextureEnabled = true;
            effect.Texture = texture;
            
            
            indexBuffer = new IndexBuffer(graphics, typeof(short), triangles.Count, BufferUsage.WriteOnly);
            
            indexBuffer.SetData(triangles.ToArray());

            Vector3[] v = new Vector3[]
                {
                   new Vector3(chunkPosition.X/2, chunkPosition.Y/2, chunkPosition.Z/2),
                   new Vector3(chunkPosition.X/2 + SIZE, chunkPosition.Y/2 + + SIZE, chunkPosition.Z/2 + + SIZE),

                };

            Vector3 min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            Vector3 max = new Vector3(float.MinValue, float.MinValue, float.MinValue);
            
            Vector3 t1 = Vector3.Transform(v[0], Engine.world.matrix);
            Vector3 t2 = Vector3.Transform(v[1], Engine.world.matrix);

            min = Vector3.Min(min, t1);
            max = Vector3.Max(max, t2);
            boundingBox = new BoundingBox( t1, t2);

            //Console.WriteLine(new Vector3(chunkPosition.X, chunkPosition.Y, chunkPosition.Z));


        }


        public void RemoveBlock(Vector3Byte posInChunk)
        {
            blocks[posInChunk.x, posInChunk.y, posInChunk.z] = 0;
            sides[posInChunk.x, posInChunk.y, posInChunk.z] = 0;
        }

        public void DrawChunk()
        {

            graphics.SetVertexBuffer(vertexBuffer);
            graphics.Indices = indexBuffer;
            graphics.SamplerStates[0] = SamplerState.PointWrap;
        
            effect.World = spaceEntity.transform.matrix;
            effect.View = camera.viewMatrix;
            effect.Projection = camera.projectionMatrix;
            effect.Alpha = 1f;

            effect.EnableDefaultLighting();
            effect.LightingEnabled = Lighting.LightingEnabled;
            effect.AmbientLightColor = Lighting.AmbientLightColor;
            effect.VertexColorEnabled = false;

            effect.DirectionalLight0.DiffuseColor = new Vector3(0.5f, 0, 0); // a red light
            effect.DirectionalLight0.Direction = new Vector3(1, 0, 0);  // coming along the x-axis
            effect.DirectionalLight0.SpecularColor = new Vector3(0, 1, 0); // with green highlights

            effect.PreferPerPixelLighting = Lighting.PreferPerPixelLighting;
            effect.FogEnabled = Lighting.FogEnabled;
            effect.FogStart = Lighting.FogStart;
            effect.FogEnd = Lighting.FogEnd;
            effect.FogColor = Lighting.FogColor;

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                //GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 8, 0, 12);
                
                //graphics.DrawPrimitives(PrimitiveType.TriangleList, 2, (int)(triangles.Count / 3f));
                graphics.DrawUserIndexedPrimitives
                    (PrimitiveType.TriangleList, verts.ToArray(),
                    0, verts.Count, triangles.ToArray(), 0, (int)(triangles.Count/3f));

            }
        }



        public byte CheckBlocksAround(byte x, byte y, byte z) // local chunk pos
        {
            byte data = 0;

            if (x > 0)
            {
                if (blocks[x - 1, y, z] == 0)
                {
                    data += 16;
                }

            }
            else
            {
                if(!spaceEntity.ChunkExist(chunkLocalPosition + Vector3Sbyte.Left))
                {
                    data += 16;
                }
                else
                {
                    if (spaceEntity.CheckBlockExist(chunkLocalPosition + Vector3Sbyte.Left, new Vector3Byte(SIZE-1, y, z), out ushort blockID))
                    {
                        if (blockID == 2) data += 16;
                    }
                    else
                    {
                        data += 16;
                    }

                }

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
                if (!spaceEntity.ChunkExist(chunkLocalPosition + Vector3Sbyte.Right))
                {
                    data += 4;
                }
                else
                {
                    if (spaceEntity.CheckBlockExist(chunkLocalPosition + Vector3Sbyte.Right, new Vector3Byte(0, y, z), out ushort blockID))
                    {
                        if(blockID == 2) data += 4;
                    }
                    else
                    {
                        data += 4;
                    }
                    
                }
            }
            // -------

            if (y > 0)
            {
                if (blocks[x, y - 1, z] == 0)
                {
                    data += 2;
                }

            }
            else
            {
                if (!spaceEntity.ChunkExist(chunkLocalPosition + Vector3Sbyte.Down))
                {
                    data += 2;
                }
                else
                {
                    if (spaceEntity.CheckBlockExist(chunkLocalPosition + Vector3Sbyte.Down, new Vector3Byte(x, SIZE-1, z), out ushort blockID))
                    {
                        if (blockID == 2) data += 2;
                    }
                    else
                    {
                        data += 2;
                    }

                }
            }
            // --------

            if (y < Chunk.SIZE - 1)
            {
                if (blocks[x, y + 1, z] == 0)
                {
                    data += 1;
                }

            }
            else
            {
                if (!spaceEntity.ChunkExist(chunkLocalPosition + Vector3Sbyte.Up))
                {
                    data += 1;
                }
                else
                {
                    if (spaceEntity.CheckBlockExist(chunkLocalPosition + Vector3Sbyte.Up, new Vector3Byte(x, 0, z), out ushort blockID))
                    {
                        if (blockID == 2) data += 1;
                    }
                    else
                    {
                        data += 1;
                    }

                }
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
                if (!spaceEntity.ChunkExist(chunkLocalPosition + Vector3Sbyte.Forward))
                {
                    data += 8;
                }
                else
                {
                    if (spaceEntity.CheckBlockExist(chunkLocalPosition + Vector3Sbyte.Forward, new Vector3Byte(x, y, SIZE-1), out ushort blockID))
                    {
                        if (blockID == 2) data += 8;
                    }
                    else
                    {
                        data += 8;
                    }

                }

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
                if (!spaceEntity.ChunkExist(chunkLocalPosition + Vector3Sbyte.Backward))
                {
                    data += 32;
                }
                else
                {
                    if (spaceEntity.CheckBlockExist(chunkLocalPosition + Vector3Sbyte.Backward, new Vector3Byte(x, y, 0), out ushort blockID))
                    {
                        if (blockID == 2) data += 32;
                    }
                    else
                    {
                        data += 32;
                    }

                }
            }
            // -------



            return data;

        }

        

    }
}
