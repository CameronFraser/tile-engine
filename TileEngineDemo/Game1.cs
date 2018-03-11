using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TiledSharp;

namespace TileEngineDemo
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        TileEngine TileEngine;

        Camera2D Camera;

        Sprite Player;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            graphics.ApplyChanges();

            Camera = new Camera2D
            {
                Position = new Vector2(0, 0)
            };

            GameServices.AddService(Camera);
        }
        
        protected override void Initialize()
        {
            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            TmxMap map = new TmxMap("../../../../Maps/test.tmx");

            TileEngine = new TileEngine(map.Width, map.Height, map.Layers[0].Tiles, map.Tilesets[0], map.ObjectGroups);

            GameServices.AddService(TileEngine);

            Camera.Initialize(graphics);

            TileEngine.LoadContent(spriteBatch, Content);

            Player = new Player("porky", new Vector2(1500, 1500), 32, 32);

            Player.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
           
            Player.Update(gameTime);
            
            Camera.Position = Player.Position;
            Camera.LockCamera();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Camera.GetTransformation(GraphicsDevice));
            TileEngine.Draw(gameTime, spriteBatch);
            Player.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
