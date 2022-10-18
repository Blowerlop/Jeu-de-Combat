using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeu_de_combat
{
    public static class SceneManager
    {
        #region Methods

        public static void ChangeState(MonoBehaviour scene)
        {
            Game1.instance._nextScene = scene;
        }

        public static void Start(object sender, EventArgs e)
        {
            Debug.WriteLine("StartScene");
            ChangeState(new GameScene());
        }
        
        public static void Menu()
        {
            ChangeState(new MenuScene());
        }

        public static void Selection(object sender, EventArgs e)
        {
            ChangeState(new PlayerSelectionScene());
        }

        public static void End()
        {
            ChangeState(new EndScene());
        }

        public static void Quit(object sender, EventArgs e)
        {
            Game1.instance.Exit();
        }

        #endregion
    }
}
