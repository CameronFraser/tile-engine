using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TileEngineDemo
{
    class Camera2D
    {
        float zoom;
        public float Rotation;
        public Matrix Transform;
        public Vector2 Position;
        public int BottomBoundary;
        public int RightBoundary;

        public Camera2D()
        {
            zoom = 1.0f;
            Rotation = 0.0f;
            Position = Vector2.Zero;
        }

        public float Zoom
        {
            get { return zoom; }
            set { zoom = MathHelper.Clamp(value, 0.1f, 10f); }
        }

        public void Move(Vector2 distance)
        {
            Position += distance;
        }

        public Matrix GetTransformation(GraphicsDevice graphicsDevice)
        {
            Viewport viewport = graphicsDevice.Viewport;

            Transform = Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
                        Matrix.CreateRotationX(Rotation) *
                        Matrix.CreateScale(new Vector3(zoom, zoom, 1)) *
                        Matrix.CreateTranslation(new Vector3(viewport.Width * 0.5f, viewport.Height * 0.5f, 0));
            return Transform;
        }

       public void LockCamera()
        {
            Console.WriteLine(Position);

            Position.X = MathHelper.Clamp(Position.X, 512, RightBoundary + 512);
            Position.Y = MathHelper.Clamp(Position.Y, 384, BottomBoundary + 384);
        }
    }
}
