using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace Jeu_de_combat
{
    public abstract class Personnage : MonoBehaviour
    {
        #region Variables
        public int health;
        public int damage;
        public int action;
        public int turn;
        public string role;
        protected Rectangle _playerCollider;
        private Vector2 _position;
        //public Vector2 _spriteDefaultPosition { get { return new Vector2(sprite.Width, sprite.Height); } private set { } }
        public SpriteEffects _spriteEffects;
        public Texture2D sprite;
        private SpriteFont _spriteFont;
        public Vector2 lifePosition;

        public Sprite _idle;
        public Sprite _attack;
        public Sprite _defend;
        public Sprite _ultimate;

        public string actionA;

        private bool isAttacking = false;
        private bool isDefending = false;
        public bool isUlting = false;

        #endregion

        #region Updates
        public Personnage(Vector2 position, SpriteEffects spriteEffect, Personnage personnage)
        {
            Debug.WriteLine("1");
            _position = position;

            _spriteEffects = spriteEffect;

            _spriteFont = Game1.instance.Content.Load<SpriteFont>("Fonts/Button");

            

        }





        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(sprite, _playerCollider, null, Color.White, 0.0f, Vector2.Zero,  _spriteEffects, 1);
            if (_idle != null)
            {
                if (isAttacking)
                {
                    _attack.Draw(gameTime, spriteBatch);
                }
                else if (isDefending)
                {
                    _defend.Draw(gameTime, spriteBatch);
                }
                else if (isUlting)
                {
                    _ultimate.Draw(gameTime, spriteBatch);
                }
                else
                {
                    _idle.Draw(gameTime, spriteBatch);
                }
            }
            spriteBatch.DrawString(_spriteFont, $"LIFE : {health}", lifePosition, new Color(169,19,19));
        }

        public override void Update(GameTime gameTime)
        {
            if (_idle != null)
            {
                if (isAttacking)
                {
                    _attack.Update(gameTime);
                }
                else if (isDefending)
                {
                    _defend.Update(gameTime);
                }
                else if (isUlting)
                {
                    _ultimate.Update(gameTime);
                }
                else
                {
                    _idle.Update(gameTime);
                }
            }
        }
        #endregion

        #region Methods
        

        public virtual async void Attack(Personnage joueur)
        {
            isAttacking = true;
            joueur.health -= damage;
            await Task.Delay(1000);

            isAttacking = false;
        }

        public virtual async void Defend(Personnage joueur)
        {
            isDefending = true;

            Debug.WriteLine("Defend");
            if (joueur.action != 3 && joueur.action != 2)
            {
                health += joueur.damage;
            }
            else if (joueur.GetType().Equals(typeof(Tank)) && joueur.action == 3)
            {
                health += joueur.damage;
                if (turn == 2)
                {
                    health -= 1;
                }
            }

            await Task.Delay(1000);
            isDefending = false;


        }

        public virtual void Ultimate(Personnage joueur)
        {
            isUlting = true;
            Debug.WriteLine("Ultimate");
            DoUltimate(joueur);
        }

        protected abstract void DoUltimate(Personnage joueur);

        public bool IsDead()
        {
            if (health <= 0)
            {
                health = 0;
                return true;
            }
            return false;
        }

        #endregion
    }
}

