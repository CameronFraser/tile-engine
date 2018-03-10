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

        public void Initialize()
        {
        }

        public void LoadContent(SpriteBatch spriteBatch, ContentManager content)
        {
            TilesetTexture = content.Load<Texture2D>(TilesetName);

            TilesetTilesWide = TilesetTexture.Width / TileWidth;
            TilesetTilesHigh = TilesetTexture.Height / TileHeight;

            int tilesetArea = TilesetTilesWide * TilesetTilesHigh;
            TilesetTiles = new Rectangle[tilesetArea];

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

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (var x = 0; x < TileMapTilesWide; x++)
            {
                for (var y = 0; y < TileMapTilesHigh; y++)
                {
                    Rectangle destinationRect = new Rectangle(x * TileWidth, y * TileHeight, TileWidth, TileHeight);

                    spriteBatch.Draw(TilesetTexture, destinationRect, TilesetTiles[Map[x, y] - 1], Color.White);
                }
            }
        }
    }
}
