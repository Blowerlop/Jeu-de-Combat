using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeu_de_combat
{
    internal class EndScene : MonoBehaviour, IScene
    {
        #region Variables

        private List<MonoBehaviour> _componentsBehaviour = new List<MonoBehaviour>();



        // Buttons
        Button _restartButton;
        Button _quitButton;
        private Texture2D _buttonSprite;
        private SpriteFont _buttonFont;
        private SpriteFont _titleFont;
        public static string text;

        #endregion

        public EndScene()
        {
            LoadSprites();
            Instantiation();
            AddComponentsBehaviour(_restartButton, _quitButton);
        }



        public void AddComponentsBehaviour(params MonoBehaviour[] componentsBehaviour)
        {
            foreach (MonoBehaviour component in componentsBehaviour)
            {
                _componentsBehaviour.Add(component);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var component in _componentsBehaviour)
                component.Draw(gameTime, spriteBatch);

            float x = _titleFont.MeasureString(text).X / 2;
            float y = _titleFont.MeasureString(text).Y / 2;
            spriteBatch.DrawString(_titleFont, text, new Vector2(Game1.instance.windowWidth / 2, 200), new Color(169, 19, 19), 0.0f, new Vector2(x, y), 1.0f, SpriteEffects.None, 0);
        }

        public void Instantiation()
        {
            _restartButton = new Button(
                _buttonSprite,
                Game1.instance.windowCenter - Game1.instance.GetSpriteCenter(_buttonSprite) + new Vector2(0, -30),
                "RESTART",
                _buttonFont
                )
            { textColor = new Color(169, 19, 19) };

            _quitButton = new Button(
                _buttonSprite,
                Game1.instance.windowCenter - Game1.instance.GetSpriteCenter(_buttonSprite) + new Vector2(0, 50),
                "QUIT",
                _buttonFont
                )
            {
                textColor = new Color(169, 19, 19)
            };




            _restartButton.Click += SceneManager.Selection;
            _quitButton.Click += SceneManager.Quit;
        }

        public void LoadSprites()
        {
            _buttonSprite = Game1.instance.Content.Load<Texture2D>("Sprites/Transparent");
            _buttonFont = Game1.instance.Content.Load<SpriteFont>("Fonts/Button");
            _titleFont = Game1.instance.Content.Load<SpriteFont>("Fonts/Title");
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _componentsBehaviour)
                component.Update(gameTime);
        }
    }
}
