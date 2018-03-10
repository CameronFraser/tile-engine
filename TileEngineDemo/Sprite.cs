using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;

namespace TileEngineDemo
{
    class Sprite
    {
        Texture2D SpriteSheet;
        public string TextureName;
        public int Width;
        public int Height;
        public Vector2 Position;
        public Vector2 ScreenPosition;
        public float Scale;
        public float Rotation;
        Rectangle SourceRectangle;
        int MapWidth;
        int MapHeight;
        int RightBoundary;
        int BottomBoundary;


        public Sprite(string textureName, Vector2 position, int mapWidth, int mapHeight, float scale = 1.0f, float rotation = 0.0f)
        {
            TextureName = textureName;
            Position = position;
            Scale = scale;
            Rotation = rotation;
            MapWidth = mapWidth;
            MapHeight = mapHeight;
        }

        public virtual void LoadContent(ContentManager content)
        {
            SpriteSheet = content.Load<Texture2D>(TextureName);
            Width = SpriteSheet.Width;
            Height = SpriteSheet.Height;
            SourceRectangle = new Rectangle(0, 0, Width, Height);
            RightBoundary = MapWidth * 32 - Width / 4;
            BottomBoundary = MapHeight * 32 - Height;
        }

        public virtual void Update()
        {
            LockSprite();
        }

        private void LockSprite()
        {
            Position.X = MathHelper.Clamp(Position.X, 0, RightBoundary);
            Position.Y = MathHelper.Clamp(Position.Y, 0, BottomBoundary);
        }
        
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(SpriteSheet, Position, new Rectangle(0, 0, 32, 32), Color.White);
        }
    }
}
