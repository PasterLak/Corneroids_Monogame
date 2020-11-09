using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Corneroids
{
    public sealed class Skybox
    {
        private ushort size = 1024;
        private float rotation = 0f;

        private Texture2D texture;

        private VertexBuffer vertexBuffer;
        private BasicEffect effect;
        private List<VertexPositionTexture> verts = new List<VertexPositionTexture>();
        private IndexBuffer indexBuffer;
        private Vector3 pos = Vector3.Zero;
        private List<short> triangles = new List<short>();
        private int count;

        private Matrix center;
        private Matrix scale;
        private Matrix translate;
        private Matrix rot;

        private Vector3 lastCameraPosition = Vector3.Zero;


        public Skybox(Texture2D texture)
        {
            this.texture = texture;

            GenerateBlock();
            CreateMesh();
        }


        private void GenerateBlock()
        {
            Vector2[] uv = { new Vector2 (0,0), new Vector2(0,1), new Vector2(1,1) , new Vector2(1,0) };

            GenerateSide(0, ref uv);
            GenerateSide(1, ref uv);
            GenerateSide(2, ref uv);
            GenerateSide(3, ref uv);
            GenerateSide(4, ref uv);
            GenerateSide(5, ref uv);
        }


        private void GenerateSide(byte sideID, ref Vector2[] uv)
        {

            switch (sideID)
            {
                case 0:

                    this.verts.Add(new VertexPositionTexture(Voxel.point[7] + pos, uv[0]));
                    this.verts.Add(new VertexPositionTexture(Voxel.point[6] + pos, uv[1]));
                    this.verts.Add(new VertexPositionTexture(Voxel.point[5] + pos, uv[2]));
                    this.verts.Add(new VertexPositionTexture(Voxel.point[4] + pos, uv[3]));

                    break;
                case 1:

                    this.verts.Add(new VertexPositionTexture(Voxel.point[4] + pos, uv[0]));
                    this.verts.Add(new VertexPositionTexture(Voxel.point[5] + pos, uv[1]));
                    this.verts.Add(new VertexPositionTexture(Voxel.point[1] + pos, uv[2]));
                    this.verts.Add(new VertexPositionTexture(Voxel.point[0] + pos, uv[3]));

                    break;
                case 2:

                    this.verts.Add(new VertexPositionTexture(Voxel.point[0] + pos, uv[0]));
                    this.verts.Add(new VertexPositionTexture(Voxel.point[1] + pos, uv[1]));
                    this.verts.Add(new VertexPositionTexture(Voxel.point[2] + pos, uv[2]));
                    this.verts.Add(new VertexPositionTexture(Voxel.point[3] + pos, uv[3]));

                    break;
                case 3:

                    this.verts.Add(new VertexPositionTexture(Voxel.point[3] + pos, uv[0]));
                    this.verts.Add(new VertexPositionTexture(Voxel.point[2] + pos, uv[1]));
                    this.verts.Add(new VertexPositionTexture(Voxel.point[6] + pos, uv[2]));
                    this.verts.Add(new VertexPositionTexture(Voxel.point[7] + pos, uv[3]));

                    break;
                case 4:

                    this.verts.Add(new VertexPositionTexture(Voxel.point[3] + pos, uv[0]));
                    this.verts.Add(new VertexPositionTexture(Voxel.point[7] + pos, uv[1]));
                    this.verts.Add(new VertexPositionTexture(Voxel.point[4] + pos, uv[2]));
                    this.verts.Add(new VertexPositionTexture(Voxel.point[0] + pos, uv[3]));

                    break;
                case 5:

                    this.verts.Add(new VertexPositionTexture(Voxel.point[6] + pos, uv[0]));
                    this.verts.Add(new VertexPositionTexture(Voxel.point[2] + pos, uv[1]));
                    this.verts.Add(new VertexPositionTexture(Voxel.point[1] + pos, uv[2]));
                    this.verts.Add(new VertexPositionTexture(Voxel.point[5] + pos, uv[3]));

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
                 Engine.graphicsDevice, typeof(VertexPositionTexture),
                 verts.Count, BufferUsage.WriteOnly
             );

            vertexBuffer.SetData(verts.ToArray());

            effect = new BasicEffect(Engine.graphicsDevice);
            effect.TextureEnabled = true;
            effect.Texture = texture;
            effect.LightingEnabled = false;

            indexBuffer = new IndexBuffer(Engine.graphicsDevice, typeof(short), triangles.Count, BufferUsage.WriteOnly);

            indexBuffer.SetData(triangles.ToArray());

             center = Matrix.CreateTranslation(new Vector3(-0.5f, -0.5f, -0.5f));
             scale = Matrix.CreateScale(size);
           
             rot = Matrix.CreateRotationY(rotation);


        }



        public void Render()
        {

            //if(Engine.camera.Position != lastCameraPosition)
            //{
                translate = Matrix.CreateTranslation(Engine.camera.Position);
                effect.World = center * rot * scale * translate;
                effect.View = Engine.camera.viewMatrix;
                effect.Projection = Engine.camera.projectionMatrix;

                lastCameraPosition = Engine.camera.Position;
            //}
            

            Engine.graphicsDevice.SetVertexBuffer(vertexBuffer);

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();


                Engine.graphicsDevice.DrawUserIndexedPrimitives
                (
                PrimitiveType.TriangleList,
                verts.ToArray(),
                 0, verts.Count,
                 triangles.ToArray(), 0,
                 (int)(triangles.Count / 3f)
                );

            }
        }

    }
}
