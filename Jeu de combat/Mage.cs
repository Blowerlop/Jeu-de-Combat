using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeu_de_combat
{
    public class Mage : Personnage
    {


        public Mage(Vector2 position, SpriteEffects spriteEffect, Personnage personnage) : base(position, spriteEffect, personnage)
        {
            Console.WriteLine("--- Mage ---");
            role = "T";
            health = 3;
            damage = 2;


            sprite = Game1.instance.Content.Load<Texture2D>("Sprites/Mage");
            _playerCollider = Collider.AddCollider(position, sprite.Width, sprite.Height);



            string className = this.GetType().Name;
            _idle = new Sprite(true, 1920, 1080) { img = Game1.instance.Content.Load<Texture2D>($"{className}/{className}_Idle"), sprteff = spriteEffect };
            _idle.AddAnim("Idle", new List<byte>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 }, 0.06f, true);

            _attack = new Sprite(true, 1920, 1080) { img = Game1.instance.Content.Load<Texture2D>($"{className}/{className}_Attack"), sprteff = spriteEffect };
            _attack.AddAnim("Attack", new List<byte>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34 }, 0.02f, true);

            _defend = new Sprite(true, 1920, 1080) { img = Game1.instance.Content.Load<Texture2D>($"{className}/{className}_Def"), sprteff = spriteEffect };
            _defend.AddAnim("Defend", new List<byte>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34 }, 0.02f, true);

            _ultimate = new Sprite(true, 1920, 1080) { img = Game1.instance.Content.Load<Texture2D>($"{className}/{className}_Ulti"), sprteff = spriteEffect };
            _ultimate.AddAnim("Ultimate", new List<byte>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34 }, 0.02f, true);


            _idle.PlayAnim("Idle");
            _attack.PlayAnim("Attack");
            _defend.PlayAnim("Defend");
            _ultimate.PlayAnim("Ultimate");



        }

        protected override async void DoUltimate(Personnage joueur)
        {
            isUlting = true;
            health++;
            damage--;
            Attack(joueur);
            damage++;
            await Task.Delay(1100);
            isUlting = false;
        }
    }
}
