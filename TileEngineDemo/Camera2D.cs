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
        private int BottomBoundary;
        private int RightBoundary;
        Viewport Viewport;

        public Camera2D()
        {
            zoom = 1.0f;
            Rotation = 0.0f;
            Position = Vector2.Zero;
        }

        public void Initialize(GraphicsDeviceManager graphics)
        {
            TileEngine tileEngine = GameServices.GetService<TileEngine>();
            RightBoundary = tileEngine.TileMapTilesWide * tileEngine.TileWidth - graphics.PreferredBackBufferWidth;
            BottomBoundary = tileEngine.TileMapTilesHigh * tileEngine.TileHeight - graphics.PreferredBackBufferHeight;
        }

        public float Zoom
        {
            get { return zoom; }
            set { zoom = MathHelper.Clamp(value, 0.1f, 3f); }
        }

        public Matrix GetTransformation(GraphicsDevice graphicsDevice)
        {
            Viewport = graphicsDevice.Viewport;

            Transform = Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
                        Matrix.CreateRotationX(Rotation) *
                        Matrix.CreateScale(new Vector3(zoom, zoom, 1)) *
                        Matrix.CreateTranslation(new Vector3(Viewport.Width * 0.5f, Viewport.Height * 0.5f, 0));

            return Transform;
        }

       public void LockCamera()
        {
            Position.X = (int)MathHelper.Clamp(Position.X, Viewport.Width / 2, RightBoundary + Viewport.Width / 2);
            Position.Y = (int)MathHelper.Clamp(Position.Y, Viewport.Height / 2, BottomBoundary + Viewport.Height / 2);
        }
    }
}
