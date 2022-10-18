using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Color = Microsoft.Xna.Framework.Color;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace Jeu_de_combat
{
    public class Game1 : Game
    {
        public static Game1 instance;

        public GraphicsDeviceManager _graphics { get; private set; }
        public SpriteBatch _spriteBatch { get; private set; }

        // Sprites
        private Texture2D _backgroundSprite;

        // Screen
        public int windowHeight { get; private set; }
        public int windowWidth { get; private set; }
        public Vector2 windowCenter { get; private set; }


        //

        // Scenes reference
        public MonoBehaviour _currentScene;
        public MonoBehaviour _nextScene;


        public Texture2D player1Sprite;
        public Texture2D player2Sprite;




        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            instance = this;
            InitializeGameSettings(true, true);
            _graphics.GraphicsProfile = GraphicsProfile.HiDef;
        }

        protected override void Initialize()
        {

            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Screen
            LoadScreenInfos();

            // Textures
            LoadSprites();

            // Scene reference
            _currentScene = new MenuScene();
        }

        protected override void Update(GameTime gameTime)
        {
            if (_nextScene != null)
            {
                _currentScene = _nextScene;
                _nextScene = null;
            }

            _currentScene.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //////////////////
            _spriteBatch.Begin();

            DrawBackground(_backgroundSprite);
            _currentScene.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawBackground(Texture2D backgroundTexture)
        {
            _spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, windowWidth, windowHeight), Color.White);
        }

        private void DrawMenu()
        {
            Vector2 offset;

            offset = new Vector2(0, -50);
            //startButton.Draw(screenCenter + offset);

            offset = new Vector2(0, 50);
            //startButton.Draw(screenCenter + offset);

        }


        public Vector2 GetSpriteCenter(Texture2D texture2D)
        {
            return new Vector2(texture2D.Width / 2, texture2D.Height / 2);
        }

        
        
        private void InitializeGameSettings(int width, int height, bool isMouseVisible)
        {
            SetWindowSize(width, height);
            SetCursorVisibility(isMouseVisible);
        }

        private void InitializeGameSettings(bool isFullScreen, bool isMouseVisible)
        {
            SetWindowSize(isFullScreen);
            SetCursorVisibility(isMouseVisible);
        }

        private void SetWindowSize(int width, int height)
        {
            _graphics.PreferredBackBufferWidth = width;
            _graphics.PreferredBackBufferHeight = height;
        }

        private void SetWindowSize(bool isFullScreen)
        {
            _graphics.IsFullScreen = isFullScreen;
            _graphics.HardwareModeSwitch = false;
        }

        private void SetCursorVisibility(bool isMouseVisible = true)
        {
            this.IsMouseVisible = isMouseVisible;
        }

        private void LoadScreenInfos()
        {
            windowHeight = GraphicsDevice.Viewport.Height;
            windowWidth = GraphicsDevice.Viewport.Width;
            windowCenter = GetWindowCenter(windowHeight, windowWidth);
        }

        private Vector2 GetWindowCenter(int screenHeight, int screenWidth)
        {
            return new Vector2(screenWidth / 2, screenHeight / 2);
        }

        private void LoadSprites()
        {
            _backgroundSprite = this.Content.Load<Texture2D>("Sprites/field");
            //_backgroundAnimation = new Animation("Background", 0.06f);
        }

        


    }
}