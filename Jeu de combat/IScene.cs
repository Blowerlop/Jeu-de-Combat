using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeu_de_combat
{
    public interface IScene
    {
        public abstract void LoadSprites();

        public abstract void Instantiation();

        public abstract void AddComponentsBehaviour(params MonoBehaviour[] componentsBehaviour);


    }
}
