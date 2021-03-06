﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Corneroids
{
    public class Camera
    {
        
        private float nearPlaneDistance = 0.1f;
        private float farPlaneDistance = 1000f;
        

        public Vector3 target;
        private Vector3 position;
        private Vector3 rotation = Vector3.Zero;

        private Vector3 oldPos;

        public Vector3 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
                UpdateLookAt();
            }
        }
        public Vector3 Rotation
        {
            get
            {
                return rotation;
            }
            set
            {
                rotation = value;
                UpdateLookAt();
            }
        }

        public Matrix projectionMatrix { get; set; }

        public Matrix viewMatrix
        {
            get
            {
                return Matrix.CreateLookAt(position, target, Vector3.Up); 
            }
            
        }
        

        private float speed = 8f;

        private Vector3 mouseRotationBuffer;

        private BoundingSphere boundingSphere;

        public Camera(Vector3 position)
        {
            this.position = position;
            this.target = Vector3.UnitZ;

           


            projectionMatrix = Matrix.CreatePerspectiveFieldOfView
               (
                   MathHelper.ToRadians(Settings.Fov),
                   Engine.graphicsDevice.DisplayMode.AspectRatio,
                   nearPlaneDistance,
                   farPlaneDistance
               );

            MoveTo(position, rotation);

            //Engine.mouse.CursorBehavior = MouseDevice.Behavior.Wrapped;


            boundingSphere = new BoundingSphere(position, 0.25f);
            
        }

        
        private void MoveTo(Vector3 pos, Vector3 rot)
        {

            Position = pos;
            Rotation = rot;

        }

        private void UpdateLookAt()
        {
            Matrix rotationMatrix = Matrix.CreateRotationX(rotation.X) * Matrix.CreateRotationY(rotation.Y);
            //Matrix rotationMatrix = Matrix.CreateFromYawPitchRoll(rotation.Y, rotation.X, rotation.Z);

            //rotationMatrix = Matrix.CreateFromQuaternion(new Quaternion(rotation, 0));

            Vector3 lookAtOffset = Vector3.Transform(Vector3.UnitZ, rotationMatrix);

            boundingSphere.Center = Position;

            if (Engine.asteroid.CheckCollision() == true)
            {
                boundingSphere.Center = oldPos;
                position = oldPos;
            }
            else
            {
                
                target = position + lookAtOffset;

            }

            

        }

        private Vector3 PreviewMove(Vector3 amount)
        {
            Matrix rotate = Matrix.CreateRotationY(rotation.Y) ;

            //Matrix rotate = Matrix.CreateFromYawPitchRoll(rotation.Y, rotation.X, rotation.Z);

            //rotate = Matrix.CreateFromQuaternion(new Quaternion(Rotation, 0));

            Vector3 movement = new Vector3(amount.X, amount.Y, amount.Z);
            movement = Vector3.Transform(movement, rotate);

            return position + movement;
        }

        private void Move(Vector3 scale)
        {
            MoveTo(PreviewMove(scale ), Rotation);
        }

        public void Update(GameTime time)
        {
            float delta = (float)time.ElapsedGameTime.TotalSeconds;

            Vector3 movingVector = Vector3.Zero;

            if (Input.GetButton(Keys.A))
                movingVector.X = 1;
            if(Input.GetButton(Keys.D))
                movingVector.X = -1;

            if(Input.GetButton(Keys.W))
                movingVector.Z = 1;
            if(Input.GetButton(Keys.S))
                movingVector.Z = -1;

            if (Input.GetButton(Keys.LeftControl))
                movingVector.Y = -1;
            if (Input.GetButton(Keys.Space))
                movingVector.Y = 1;

            if(movingVector != Vector3.Zero)
            {
                oldPos = position;
                movingVector.Normalize();

                if (Input.GetButton(Keys.LeftShift))
                {
                    movingVector *= speed * 6 * delta;
               
                }
                    
                else
                {
                    movingVector *= speed * delta;
               
                }

                
                    Move(movingVector);

                




            }
             

            if(Engine.mouse.GetTranslationY() != 0 || Engine.mouse.GetTranslationX() != 0)
            {
                mouseRotationBuffer.Y -= 0.1f * delta * Engine.mouse.GetTranslationY();
                mouseRotationBuffer.X -= 0.1f * delta * Engine.mouse.GetTranslationX();

                if (mouseRotationBuffer.Y < MathHelper.ToRadians(-75f))
                    mouseRotationBuffer.Y = mouseRotationBuffer.Y - (mouseRotationBuffer.Y - MathHelper.ToRadians(-75f));
                

                if (mouseRotationBuffer.Y > MathHelper.ToRadians(75f))
                    mouseRotationBuffer.Y = mouseRotationBuffer.Y - (mouseRotationBuffer.Y - MathHelper.ToRadians(75f));
                

                Rotation = new Vector3(-MathHelper.Clamp(mouseRotationBuffer.Y, MathHelper.ToRadians(-75f), MathHelper.ToRadians(75f)),
                    MathHelper.WrapAngle(mouseRotationBuffer.X), 0
                    );
            }

            
            //Vector3 v = new Vector3(20,0,0);
            //boundingSphere.Contains(ref v, out ContainmentType r);
            //System.Console.WriteLine(r);

        }


        public void DrawDebug( )
        {
            Engine.spriteBatch.DrawString(
                Engine.font, $"x:{(int)position.X} y:{(int)position.Y} z:{(int)position.Z}" ,
                new Vector2(5, Engine.window.ClientBounds.Height - Engine.window.ClientBounds.Height / 100 * 25),
                Color.White);
            

        }

        public bool CollidesWith (Chunk ch)
        {

            for (byte x = 0; x < Chunk.SIZE; x++)
            {
                for (byte y = 0; y < Chunk.SIZE; y++)
                {
                    for (byte z = 0; z < Chunk.SIZE; z++)
                    {
                        if (boundingSphere.Intersects(ch.boundingBox[x,y,z]))
                            return true;
                    }
                }
            }
            


            return false;
        }

    }
}
