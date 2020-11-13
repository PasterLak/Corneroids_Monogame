using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Corneroids
{
    public class BillboardSystem
    {
        private VertexBuffer vertexBuffer;
        private IndexBuffer indexBuffer;
        private VertexPositionTexture[] particles;
        private int[] indices;

        private int nBillBoards;
        private Vector2 size;
        private Texture2D texture;

        private GraphicsDevice graphicsDevice;
        private BasicEffect effect;

        public BillboardSystem(GraphicsDevice graphicsDevice, ContentManager content,
                        Texture2D texture, Vector2 size, Vector3[] particlePositions)
        {
            this.nBillBoards = particlePositions.Length;
            this.size = size;
            this.graphicsDevice = graphicsDevice;
            this.texture = texture;

            //effect = content.Load<Effect>("BillboardEffect");
            effect = new BasicEffect(graphicsDevice);

            GenerateParticles(particlePositions);
        }

        private void GenerateParticles(Vector3[] particlePositions)
        {
            particles = new VertexPositionTexture[nBillBoards * 4];

            indices = new int[nBillBoards * 6];

            int x = 0;

            for (int i = 0; i < nBillBoards * 4; i += 4)
            {
                Vector3 pos = particlePositions[i / 4];

                particles[i] = new VertexPositionTexture(pos, new Vector2(0,0));

                particles[i + 1] = new VertexPositionTexture(pos, new Vector2(0, 1));

                particles[i + 2] = new VertexPositionTexture(pos, new Vector2(1, 1));

                particles[i + 3] = new VertexPositionTexture(pos, new Vector2(1, 0));

                indices[x++] = i + 0;
                indices[x++] = i + 3;
                indices[x++] = i + 2;
                indices[x++] = i + 2;
                indices[x++] = i + 1;
                indices[x++] = i + 0;
            }

            vertexBuffer = new VertexBuffer(graphicsDevice, typeof(VertexPositionTexture),
                nBillBoards * 4, BufferUsage.WriteOnly);

            vertexBuffer.SetData(particles);

            indexBuffer = new IndexBuffer(graphicsDevice, IndexElementSize.ThirtyTwoBits,
                nBillBoards * 6, BufferUsage.WriteOnly);

            indexBuffer.SetData(indices);
        }


        private void SetEffectParameters(Camera camera, Vector3 up, Vector3 right)
        {
            effect.World =  Engine.world.matrix;
            effect.View = camera.viewMatrix;
            effect.Projection = camera.projectionMatrix;
            // Effect not Basic 
            //effect.Parameters["ParticleTexture"].SetValue(texture);
            //effect.Parameters["View"].SetValue(camera.viewMatrix);
            //effect.Parameters["Projection"].SetValue(camera.projectionMatrix);
            //effect.Parameters["Size"].SetValue(size/ 2f);
            //effect.Parameters["Up"].SetValue(up);
            //effect.Parameters["Side"].SetValue(right);

            //effect.CurrentTechnique.Passes[0].Apply();

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                //GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 8, 0, 12);

                //graphics.DrawPrimitives(PrimitiveType.TriangleList, 2, (int)(triangles.Count / 3f));
                graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList,
                0, 0, 4 * nBillBoards, 0, nBillBoards * 2);

            }
        }

        public void Draw(Camera camera, Vector3 up, Vector3 right)
        {
            graphicsDevice.SetVertexBuffer(vertexBuffer);
            graphicsDevice.Indices = indexBuffer;

            SetEffectParameters(camera, up, right);

            graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList,
                0, 0, 4 * nBillBoards, 0, nBillBoards * 2);

            graphicsDevice.SetVertexBuffer(null);
            graphicsDevice.Indices = null;
        }
    }
}
