using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;

namespace TileEngineDemo
{
    class Player : Sprite
    {
        public float Speed;
        public Vector2 OldPosition;

        public Player(string textureName, Vector2 position, int spriteWidth, int spriteHeight) 
            : base(textureName, position, spriteWidth, spriteHeight)
        {
           Speed = 0.3f;
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {
            OldPosition = Position;
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
            

            if (motion != Vector2.Zero)
            {
                if (motion == new Vector2(0, -1))
                {
                    SourceRectangle = new Rectangle(32, 0, 32, 32);
                }
                else if (motion == new Vector2(0, 1))
                {
                    SourceRectangle = new Rectangle(0, 0, 32, 32);
                }
                else if (motion.X >= -1 && motion.X < 0)
                {
                    SourceRectangle = new Rectangle(64, 0, 32, 32);
                }
                else if (motion.X <= 1 && motion.X > 0)
                {
                    SourceRectangle = new Rectangle(96, 0, 32, 32);
                }
            }
            
            Position += motion * (Speed * (float)gameTime.ElapsedGameTime.Milliseconds);

            base.Update(gameTime);

            Rectangle playerRect = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
            if (TileEngine.CheckIfColliding(playerRect))
            {
                Position = OldPosition;
            }
        }
    }
}
