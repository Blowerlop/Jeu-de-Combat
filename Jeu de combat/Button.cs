using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.ComponentModel;
using System;
using SharpDX.Direct2D1;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;
using SharpDX.Direct3D9;

namespace Jeu_de_combat
{
    public class Button : MonoBehaviour
    {
        #region Variables

        // Button
        private Texture2D _buttonSprite;
        public Vector2 position { get; private set; }
        public Rectangle buttonCollider { get; private set; }
        private bool _isHoveringButton;

        // Text
        private string _text;
        private SpriteFont _textFont;
        public Color textColor { get; set; } = Color.Black;

        // Mouse data
        public MouseState _currentMouseState;
        public MouseState _previousMouseState;

        // Click event
        public event EventHandler Click;

        #endregion

        #region Updates

        public Button(Texture2D texture, Vector2 position, string text, SpriteFont font, Vector2 size)
        {
            _buttonSprite = texture;
            this.position = position;
            _text = text;
            _textFont = font;
            
            buttonCollider = Collider.AddCollider(position, (int)size.X, (int)size.Y);
        }

        public Button(Texture2D texture, Vector2 position, string text, SpriteFont font)
        {
            _buttonSprite = texture;
            this.position = position;
            _text = text;
            _textFont = font;
            
            buttonCollider = Collider.AddCollider(position, _buttonSprite.Width, _buttonSprite.Height);
        }

        public override void Update(GameTime gameTime)
        {
            GetMouseState();
            
            Vector2 mousePosition = new Vector2(_currentMouseState.X, _currentMouseState.Y);
            Rectangle mouseCollider = Collider.AddCollider(mousePosition, 1, 1);

            _isHoveringButton = false;

            if (Collider.Collide(mouseCollider, buttonCollider))
            {
                _isHoveringButton = true;

                if (Clicked())
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var colour = Color.White;

            if (_isHoveringButton)
                colour = Color.Gray;

            spriteBatch.Draw(_buttonSprite, buttonCollider, colour);

            if (string.IsNullOrEmpty(_text) == false)
            {
                CenterText(spriteBatch);
            }
        }

        #endregion

        #region Methods

        private void GetMouseState()
        {
            _previousMouseState = _currentMouseState;
            _currentMouseState = Mouse.GetState();
        }

        private bool Clicked()
        {
            return _currentMouseState.LeftButton == ButtonState.Released && _previousMouseState.LeftButton == ButtonState.Pressed;
        }

        private void CenterText(SpriteBatch spriteBatch)
        {
            float x = (buttonCollider.X + (buttonCollider.Width / 2)) - (_textFont.MeasureString(_text).X / 2);
            float y = (buttonCollider.Y + (buttonCollider.Height / 2)) - (_textFont.MeasureString(_text).Y / 2);

            spriteBatch.DrawString(_textFont, _text, new Vector2(x, y), textColor);
        }
        #endregion
    }
}
