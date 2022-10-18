using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeu_de_combat
{
    public class MenuScene : MonoBehaviour, IScene
    {
        #region Variables

        private List<MonoBehaviour> _componentsBehaviour = new List<MonoBehaviour>();

        // Buttons
        Button _startButton;
        Button _quitButton;
        private Texture2D _buttonSprite;


        private SpriteFont _buttonFont;
        private SpriteFont _titleFont;

        #endregion

        #region Updates

        public MenuScene()
        {
            LoadSprites();
            Instantiation();
            AddComponentsBehaviour(_startButton, _quitButton);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _componentsBehaviour)
                component.Update(gameTime);

          
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var component in _componentsBehaviour)
                component.Draw(gameTime, spriteBatch);


            float x = _titleFont.MeasureString("Le Jeu De La Bagarre").X / 2;
            float y = _titleFont.MeasureString("Le Jeu De La Bagarre").Y / 2;
            spriteBatch.DrawString(_titleFont, "Le Jeu De La Bagarre", new Vector2(Game1.instance.windowWidth / 2, 200), new Color(169,19,19), 0.0f, new Vector2(x,y), 1.0f, SpriteEffects.None, 0);
        }
        #endregion

        #region Methods

        public void LoadSprites()
        {
            _buttonSprite = Game1.instance.Content.Load<Texture2D>("Sprites/transparent");
            _buttonFont = Game1.instance.Content.Load<SpriteFont>("Fonts/Button");
            _titleFont = Game1.instance.Content.Load<SpriteFont>("Fonts/Title");
        }

        public void Instantiation()
        {
            _startButton = new Button(
                _buttonSprite,
                Game1.instance.windowCenter - Game1.instance.GetSpriteCenter(_buttonSprite) + new Vector2(0, -30),
                "START",
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
            

            _startButton.Click += SceneManager.Selection;
            _quitButton.Click += SceneManager.Quit;
        }

        public void AddComponentsBehaviour(params MonoBehaviour[] componentsBehaviour)
        {
            foreach (MonoBehaviour component in componentsBehaviour)
            {
                _componentsBehaviour.Add(component);
            }
        }
        #endregion


    }
}
