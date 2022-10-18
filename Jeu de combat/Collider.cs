using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeu_de_combat
{
    public static class Collider
    {
        public static Rectangle AddCollider(Vector2 origin, int width, int height)
        {
            return new Rectangle((int)origin.X, (int)origin.Y, width, height);
        }

        public static bool Collide(Rectangle collider1, Rectangle collider2)
        {
            return collider1.Intersects(collider2);
        }
    }
}
