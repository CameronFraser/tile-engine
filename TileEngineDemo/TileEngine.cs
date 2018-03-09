using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.ObjectModel;
using System;
using TiledSharp;

namespace TileEngineDemo
{
    class TileEngine
    {
        public byte[,] Map;
        public Texture2D TilesetTexture;
        public string TilesetName;
        public int TileWidth;
        public int TileHeight;
        public int TilesetTilesWide;
        public int TilesetTilesHigh;
        public int TileMapTilesWide;
        public int TileMapTilesHigh;
        public Rectangle[] TilesetTiles;

        Camera Camera;

        public TileEngine(int mapWidth, int mapHeight, Collection<TmxLayerTile> tiles, TmxTileset tileset) 
        {
            Map = new byte[mapWidth, mapHeight];

            foreach (var tile in tiles)
            {
                Map[tile.X, tile.Y] = (byte)tile.Gid;
            }

            TilesetName = tileset.Name;
            TileWidth = tileset.TileWidth;
            TileHeight = tileset.TileHeight;
            TileMapTilesWide = mapWidth;
            TileMapTilesHigh = mapHeight;
        }

        public void Initialize(GraphicsDeviceManager graphics)
        {
            int rightBoundary = TileMapTilesWide * TileWidth - graphics.PreferredBackBufferWidth;
            int bottomBoundary = TileMapTilesHigh * TileHeight - graphics.PreferredBackBufferHeight;
            Camera = new Camera(Vector2.Zero, 8.0f, rightBoundary, bottomBoundary);
        }

        public void LoadContent(SpriteBatch spriteBatch, ContentManager content)
        {
            TilesetTexture = content.Load<Texture2D>(TilesetName);

            TilesetTilesWide = TilesetTexture.Width / TileWidth;
            TilesetTilesHigh = TilesetTexture.Height / TileHeight;

            int tilesetArea = TilesetTilesWide * TilesetTilesHigh;
            TilesetTiles = new Rectangle[tilesetArea];
            // Take all tiles from the tileset and create a rectangle object representing
            // The position of the tile. This way when rendering the tiles we can just access the tile we want
            // with the Gid from the tile
            int index = 0;
            for (var y = 0; y < TilesetTilesHigh; y++)
            {
                for (var x = 0; x < TilesetTilesWide; x++)
                {
                    TilesetTiles[index] = new Rectangle(x * TileWidth, y * TileHeight, TileWidth, TileHeight);
                    index++;
                }
            }
        }

        public void Update(GameTime gameTIme)
        {
            Camera.Update();
        }


        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (var x = 0; x < TileMapTilesWide; x++)
            {
                for (var y = 0; y < TileMapTilesHigh; y++)
                {
                    // If we did not subtract the camera's position it would just render the upper left hand corner of the map
                    // Subtracting the position is what gives up the appearance of a camera moving across a map
                    int offsetX = x * TileWidth - (int)Camera.Position.X;
                    int offsetY = y * TileHeight - (int)Camera.Position.Y;
                    // Destination Rectangle defines the area in which our tile will be drawn, we take the x and y values signifying
                    // our current tile and multiply by tile height and width to get pixel position
                    Rectangle destinationRect = new Rectangle(offsetX, offsetY, TileWidth, TileHeight);
                    // Tells monogame to draw in the position defined above a piece of the TilesetTexture which was initialized
                    // in the load content method
                    spriteBatch.Draw(TilesetTexture, destinationRect, TilesetTiles[Map[x, y] - 1], Color.White);
                }
            }
        }
    }
}
