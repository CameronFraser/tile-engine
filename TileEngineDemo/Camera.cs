using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace TileEngineDemo
{
    class Camera
    {
        public Vector2 Position;
        public float Speed;
        public int RightBoundary;
        public int BottomBoundary;

        public Camera(Vector2 position, float speed, int rightBoundary, int bottomBoundary)
        {
            this.Position = position;
            this.Speed = speed;
            this.RightBoundary = rightBoundary;
            this.BottomBoundary = bottomBoundary;
        }

        public void Update()
        {
            KeyboardState kState = Keyboard.GetState();
            Vector2 motion = new Vector2();

            if (kState.IsKeyDown(Keys.Up))
                motion.Y--;
            if (kState.IsKeyDown(Keys.Down))
                motion.Y++;
            if (kState.IsKeyDown(Keys.Left))
                motion.X--;
            if (kState.IsKeyDown(Keys.Right))
                motion.X++;
            if (motion != Vector2.Zero)
                motion.Normalize();

            Position += motion * Speed;

            LockCamera();
        }

        public void LockCamera()
        {
            Position.X = MathHelper.Clamp(Position.X, 0, RightBoundary);
            Position.Y = MathHelper.Clamp(Position.Y, 0, BottomBoundary);
        }

    }
}
