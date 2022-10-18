﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeu_de_combat
{
    public abstract class MonoBehaviour
    {
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void Update(GameTime gameTime);
    }
}
