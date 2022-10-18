using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace Jeu_de_combat
{
    public class PlayerSelectionScene : MonoBehaviour, IScene
    {
        #region Variables
        private Texture2D _classTankSprite;
        private Texture2D _classDamagerSprite;
        private Texture2D _classHealerSprite;
        private Texture2D _classMageSprite;


        private Button _buttonTank;
        private Button _buttonDamager;
        private Button _buttonHealer;
        private Button _buttonMage;

        private Button _buttonEasyDifficulty;
        private Button _buttonMediumDifficulty;
        private Button _buttonHardDifficulty;

        private Texture2D _transparentSprite;

        private SpriteFont _buttonFont;

        private List<MonoBehaviour> _componentsBehaviour = new List<MonoBehaviour>();

        private SpriteFont _titleFont;

        private bool hasChoosedDifficulty = false;
        
        #endregion

        #region Updates

        public PlayerSelectionScene()
        {
            LoadSprites();
            Instantiation();
            AddComponentsBehaviour(_buttonTank, _buttonDamager, _buttonHealer, _buttonMage);
        }

        public override void Update(GameTime gameTime)
        {
            if (hasChoosedDifficulty == false)
            {
                _buttonEasyDifficulty.Update(gameTime);
                _buttonMediumDifficulty.Update(gameTime);
                _buttonHardDifficulty.Update(gameTime);
            }
            else
            {
                foreach (var component in _componentsBehaviour)
                    component.Update(gameTime);
            }
            
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (hasChoosedDifficulty == false)
            {
                float x = _titleFont.MeasureString("Choisir une difficulte").X / 2;
                float y = _titleFont.MeasureString("Choisir une difficulte").Y / 2;
                spriteBatch.DrawString(_titleFont, "Choisir une difficulte", new Vector2(Game1.instance.windowWidth / 2, 200), new Color(169, 19, 19), 0.0f, new Vector2(x, y), 1.0f, SpriteEffects.None, 0);

                _buttonEasyDifficulty.Draw(gameTime, spriteBatch);
                _buttonMediumDifficulty.Draw(gameTime, spriteBatch);
                _buttonHardDifficulty.Draw(gameTime, spriteBatch);
            }
            else
            {
                foreach (var component in _componentsBehaviour)
                    component.Draw(gameTime, spriteBatch);
            }

            

            
        }

        #endregion

        #region Methods

        public void LoadSprites()
        {


            _classTankSprite = Game1.instance.Content.Load<Texture2D>("Sprites/btn_Tank");
            _classDamagerSprite = Game1.instance.Content.Load<Texture2D>("Sprites/btn_Damager");
            _classHealerSprite = Game1.instance.Content.Load<Texture2D>("Sprites/btn_Healer");
            _classMageSprite = Game1.instance.Content.Load<Texture2D>("Sprites/btn_Mage");
            _transparentSprite = Game1.instance.Content.Load<Texture2D>("Sprites/Transparent");

            _buttonFont = Game1.instance.Content.Load<SpriteFont>("Fonts/Button");
            _titleFont = Game1.instance.Content.Load<SpriteFont>("Fonts/Title");

            
        }

        public void Instantiation()
        {
            float offsetX = 100;
            float offsetY = 50;


            iaSelectRandomClass();

            _buttonTank = new Button(_classTankSprite, new Vector2(offsetX, offsetY), "", _buttonFont);
            _buttonDamager = new Button(_classDamagerSprite, new Vector2(Game1.instance.windowWidth - _classTankSprite.Width - offsetX, offsetY), "", _buttonFont);
            _buttonHealer = new Button(_classHealerSprite, new Vector2(offsetX, Game1.instance.windowHeight - _classTankSprite.Height - offsetY), "", _buttonFont);
            _buttonMage = new Button(_classMageSprite, new Vector2(Game1.instance.windowWidth - _classTankSprite.Width - offsetX, Game1.instance.windowHeight - _classTankSprite.Height - offsetY), "", _buttonFont);

            _buttonEasyDifficulty = new Button(_transparentSprite, Game1.instance.windowCenter - Game1.instance.GetSpriteCenter(_transparentSprite) + new Vector2(0, -100), "Facile", _buttonFont);
            _buttonMediumDifficulty = new Button(_transparentSprite, Game1.instance.windowCenter - Game1.instance.GetSpriteCenter(_transparentSprite) + new Vector2(0, 0), "Normal", _buttonFont);
            _buttonHardDifficulty = new Button(_transparentSprite, Game1.instance.windowCenter - Game1.instance.GetSpriteCenter(_transparentSprite) + new Vector2(0, 100), "Difficile", _buttonFont);


            //_buttonTank.Click += SelectTank;
            _buttonTank.Click += (_buttonTank, e) => SelectTank(ref GameScene.joueur, SpriteEffects.None);
            _buttonTank.Click += SceneManager.Start;

            _buttonDamager.Click += (_buttonTank, e) => SelectDamager(ref GameScene.joueur, SpriteEffects.None); ;
            _buttonDamager.Click += SceneManager.Start;

            _buttonHealer.Click += (_buttonTank, e) => SelectHealer(ref GameScene.joueur, SpriteEffects.None); ;
            _buttonHealer.Click += SceneManager.Start;

            _buttonMage.Click += (_buttonTank, e) => SelectMage(ref GameScene.joueur, SpriteEffects.None); ;
            _buttonMage.Click += SceneManager.Start;

            _buttonEasyDifficulty.Click += (_bttonEasyDifficulty, e) => GameScene.difficulty = 0; 
            _buttonEasyDifficulty.Click += (_bttonEasyDifficulty, e) => hasChoosedDifficulty = true; 
            _buttonMediumDifficulty.Click += (_buttonMediumDifficulty, e) => GameScene.difficulty = 1; 
            _buttonMediumDifficulty.Click += (_buttonMediumDifficulty, e) => hasChoosedDifficulty = true; 
            _buttonHardDifficulty.Click += (_buttonHardDifficulty, e) => GameScene.difficulty = 2; 
            _buttonHardDifficulty.Click += (_buttonHardDifficulty, e) => hasChoosedDifficulty = true; 
        }

        public void AddComponentsBehaviour(params MonoBehaviour[] componentsBehaviour)
        {
            foreach (MonoBehaviour component in componentsBehaviour)
            {
                _componentsBehaviour.Add(component);
            }
        }
        #endregion


        #region Methods
        /*
        private T GetClass<T>()
        {
            return TypeDescriptor.GetTypeConverter(typeof(T))
        }
        */

        private void SelectTank(ref Personnage personnage, SpriteEffects spriteEffects)
        {
            personnage = new Tank(Vector2.Zero, spriteEffects,  personnage);
        }

        private void SelectDamager(ref Personnage personnage, SpriteEffects spriteEffects)
        {
            personnage = new Damager(Vector2.Zero, spriteEffects, personnage);
        }

        private void SelectHealer(ref Personnage personnage, SpriteEffects spriteEffects)
        {
            personnage = new Healer(Vector2.Zero, spriteEffects, personnage);
        }

        private void SelectMage(ref Personnage personnage, SpriteEffects spriteEffects)
        {
            personnage = new Mage(Vector2.Zero, spriteEffects, personnage);
        }

        private void iaSelectRandomClass()
        {
            Random random = new Random();
            int number = random.Next(0, 4);
            switch (number)
            {
                case 0:
                    SelectTank(ref GameScene.ia, SpriteEffects.FlipHorizontally);
                    break;

                case 1:
                    SelectDamager(ref GameScene.ia, SpriteEffects.FlipHorizontally);
                    break;

                case 2:
                    SelectHealer(ref GameScene.ia, SpriteEffects.FlipHorizontally);
                    break;

                case 3:
                    SelectMage(ref GameScene.ia, SpriteEffects.FlipHorizontally);
                    break;
            }

            
        }

        #endregion
    }
}
