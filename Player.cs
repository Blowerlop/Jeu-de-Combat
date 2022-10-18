using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeu_de_combat
{
    public class Player : MonoBehaviour
    {
        #region Variables

        public Texture2D _playerSprite { get; private set; }
        private Rectangle _playerCollider;
        private Vector2 _position;
        public Vector2 _spriteDefaultPosition { get { return new Vector2(_playerSprite.Width, _playerSprite.Height); } private set { } }
        private SpriteEffects _spriteEffects;

        #endregion

        #region Updates

        public Player(Texture2D texture, Vector2 position, SpriteEffects spriteEffect)
        {
            _playerSprite = texture;
            _position = position;

            _playerCollider = Collider.AddCollider(position, _playerSprite.Width, _playerSprite.Height);
            _spriteEffects = spriteEffect;
        }

        public Player(Texture2D texture, Vector2 position, Vector2 size, SpriteEffects spriteEffect)
        {
            _playerSprite = texture;
            _position = position;
            _spriteEffects = spriteEffect;
            _playerCollider = Collider.AddCollider(position, (int)size.X, (int)size.Y);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_playerSprite, _playerCollider, null, Color.White, 0.0f, Vector2.Zero, _spriteEffects, 1);
        }

        public override void Update(GameTime gameTime)
        {
            
        }
        #endregion
    }
}
