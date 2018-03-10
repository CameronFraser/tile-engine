using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace TileEngineDemo
{
    class Sprite
    {
        Texture2D SpriteSheet;
        public string TextureName;
        public int Width;
        public int Height;
        public int SheetWidth;
        public int SheetHeight;
        public Vector2 Position;
        public Rectangle SourceRectangle;
        int RightBoundary;
        int BottomBoundary;

        public Sprite(string textureName, Vector2 position, int spriteWidth, int spriteHeight)
        {
            TextureName = textureName;
            Position = position;
            Width = spriteWidth;
            Height = spriteHeight;
        }

        public virtual void LoadContent(ContentManager content)
        {
            TileEngine tileEngine = GameServices.GetService<TileEngine>();
            SpriteSheet = content.Load<Texture2D>(TextureName);
            

            SheetWidth = SpriteSheet.Width;
            SheetHeight = SpriteSheet.Height;

            SourceRectangle = new Rectangle(0, 0, Width, Height);

            RightBoundary = tileEngine.TileMapTilesWide * tileEngine.TileWidth - Width;
            BottomBoundary = tileEngine.TileMapTilesHigh * tileEngine.TileHeight - Height;
        }

        public virtual void Update(GameTime gameTime)
        {
            LockSprite();
        }

        private void LockSprite()
        {
            Position.X = (int)MathHelper.Clamp(Position.X, 0, RightBoundary);
            Position.Y = (int)MathHelper.Clamp(Position.Y, 0, BottomBoundary);
        }
        
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(SpriteSheet, Position, SourceRectangle, Color.White);
        }
    }
}
