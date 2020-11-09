using System;
using Microsoft.Xna.Framework;


namespace Corneroids
{
    public class Transform
    {

        public Matrix matrix { get; private set; }

        private Vector3 _position;
        private Vector3 _rotation;
        private Vector3 _scale;
        private Quaternion _quaternion;

        private Matrix translateMatrix;
        private Matrix rotationMatrix;
        private Matrix scaleMatrix;

        public Transform ()
        {
            SetDefaultTransform();
        }
        public Transform (Vector3 position, Vector3 rotation)
        {
            this.position = position;
            this.rotation = rotation;
            this.scale = new Vector3(1, 1, 1);
        }

        public Vector3 position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;

                translateMatrix = Matrix.CreateTranslation(_position);

                UpdateMatrix();

            }
        }
        public Vector3 rotation
        {
            get
            {
                return _rotation;
            }
            set
            {
                _rotation = value;

                //rotationMatrix = matrix;

                rotationMatrix = Matrix.CreateRotationX(MathHelper.ToRadians(_rotation.X)) *
                    Matrix.CreateRotationY(MathHelper.ToRadians(_rotation.Y)) *
                    Matrix.CreateRotationZ(MathHelper.ToRadians(_rotation.Z));



                UpdateMatrix();
            }
        }

        public void Translate(Vector3 pos)
        {
            _position += pos;
            translateMatrix = Matrix.CreateTranslation(_position);

            matrix = scaleMatrix  * translateMatrix * rotationMatrix;
        }

        public Vector3 scale
        {
            get
            {
                return _scale;
            }
            set
            {
                _scale = value;

                scaleMatrix = Matrix.CreateScale(_scale);

                UpdateMatrix();
            }
        }

        private void UpdateMatrix()
        {
            matrix = scaleMatrix * rotationMatrix * translateMatrix;
        }
        
        public void SetDefaultTransform()
        {
            this.position = position;
            this.rotation = rotation;
            this.scale = new Vector3(1, 1, 1);
        }

    }
}
