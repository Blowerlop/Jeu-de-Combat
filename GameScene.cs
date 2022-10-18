using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.Design.AxImporter;
using System.Data;
using System.Diagnostics;
using System.Runtime.Intrinsics.Arm;
using System.Threading;

namespace Jeu_de_combat
{
    internal class GameScene : MonoBehaviour, IScene
    {
        #region Variables

        private List<MonoBehaviour> _componentsBehaviour = new List<MonoBehaviour>();

        // Button
        private Button _attackButton;
        private Button _defendButton;
        private Button _ultimateButton;
        private Texture2D _buttonSprite;
        private SpriteFont _actionFont;

        public static int difficulty = 2;

        public static Personnage joueur;
        public static Personnage ia;

        private bool isGameFinished = false;

        private SpriteFont _titleFont;

        private string a;
        private string b;

        #endregion

        #region Updates
        public GameScene()
        {
            LoadSprites();
            Instantiation();
            AddComponentsBehaviour(_attackButton, _defendButton, _ultimateButton, joueur, ia);

            Initialize(ref joueur, 1);
            Initialize(ref ia, 2);
            joueur.action = -1;
            SetActions();

            float y = 170;
            joueur.lifePosition = new Vector2(100, y);
            ia.lifePosition = new Vector2(1650, y);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _componentsBehaviour)
                component.Update(gameTime);


            if (isGameFinished == false && joueur.action != -1 && ia.action != -1)
            {
                ResolutionActions();

                if (Deaths())
                {
                    GameResume();
                    isGameFinished = true;
                    SceneManager.End();
                    Print("It's the end of the game !");
                }
                else
                {
                    
                    joueur.action = -1;
                    ia.action = -1;
                    GameResume();
                    SetActions();

                }


                Debug.WriteLine("DISPLAYYYYYYYYYYY");



            }


        }

        private async void zaeaze()
        {
            await Task.Delay(2000);
            
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var component in _componentsBehaviour)
                component.Draw(gameTime, spriteBatch);


            float x = _titleFont.MeasureString($"{joueur.actionA} vs {ia.actionA}").X / 2;
            float y = _titleFont.MeasureString($"{joueur.actionA} vs {ia.actionA}").Y / 2;
            spriteBatch.DrawString(_titleFont, $"{joueur.actionA} vs {ia.actionA}", new Vector2(Game1.instance.windowWidth / 2, 200), new Color(169, 19, 19), 0.0f, new Vector2(x, y), 0.5f, SpriteEffects.None, 0);

            if (isGameFinished == false && joueur.action != -1 && ia.action != -1)
            {
                


            }
        }

        #endregion

        #region Methods

        public void LoadSprites()
        {
            _buttonSprite = Game1.instance.Content.Load<Texture2D>("Sprites/blue_button00");
            _actionFont = Game1.instance.Content.Load<SpriteFont>("Fonts/Action");
            _titleFont = Game1.instance.Content.Load<SpriteFont>("Fonts/Title");
        }

        public void Instantiation()
        {
            float offsetX = 250;
            float offsetY = 50;

            _attackButton = new Button(
                _buttonSprite,
                new Vector2(Game1.instance.windowWidth / 2 - _buttonSprite.Width / 2 - offsetX, Game1.instance.windowHeight - _buttonSprite.Height - offsetY),
                "ATTACK",
                _actionFont
                )
            {
                textColor = Color.White
            };


            _defendButton = new Button(
                _buttonSprite,
                new Vector2(Game1.instance.windowWidth / 2 - _buttonSprite.Width / 2, Game1.instance.windowHeight - _buttonSprite.Height - offsetY),
                "DEFEND",
                _actionFont
                )
            {
                textColor = Color.White
            };

            _ultimateButton = new Button(
                _buttonSprite,
                new Vector2(Game1.instance.windowWidth / 2 - _buttonSprite.Width / 2 + offsetX, Game1.instance.windowHeight - _buttonSprite.Height - offsetY),
                "ULTIMATE",
                _actionFont
                )
            {
                textColor = Color.White
            };


            _attackButton.Click += (_attackButton, e) => SetAction(_attackButton, e, 1);
            //_attackButton.Click += (_attackButton, e) => joueur.Attack(_attackButton, e, ia);

            _defendButton.Click += (_defendButton, e) => SetAction(_attackButton, e, 2);
            //_defendButton.Click += (_defendButton, e) => joueur.Defend(_defendButton, e, ia);

            _ultimateButton.Click += (_ultimateButton, e) => SetAction(_attackButton, e, 3);
            //_ultimateButton.Click += (_ultimateButton, e) => joueur.Ultimate(_attackButton, e, ia);
        }

        public void AddComponentsBehaviour(params MonoBehaviour[] componentsBehaviour)
        {
            foreach (MonoBehaviour component in componentsBehaviour)
            {
                _componentsBehaviour.Add(component);
            }


        }

        private void Initialize(ref Personnage personnage, int turn)
        {
            personnage.turn = turn;
        }

        private void Actions(ref Personnage personnage, int actionPersonnage, ref Personnage cible, int actionCible)
        {

            switch (actionPersonnage)
            {
                case 1:
                    Debug.WriteLine($"{personnage} Attack");
                    personnage.actionA = "Attack";
                    personnage.Attack(cible);
                    break;

                case 2:
                    Debug.WriteLine($"{personnage} Defend");
                    personnage.actionA = "Defend";

                    personnage.Defend(cible);
                    break;

                case 3:
                    Debug.WriteLine($"{personnage} Ultimate");
                    personnage.actionA = "Ultimate";

                    personnage.Ultimate(cible);
                    break;

                default:
                    Actions(ref personnage, actionPersonnage, ref cible, actionCible);
                    break;
            }


        }

        private void ResolutionActions()
        {
            string roleJoueur = joueur.role;
            int actionJoueur = joueur.action;
            string roleIa = ia.role;
            int actionIa = ia.action;


            Actions(ref joueur, actionJoueur, ref ia, actionIa);
            Actions(ref ia, actionIa, ref joueur, actionJoueur);
        }

        private bool Deaths()
        {
            bool isPlayerDead = joueur.IsDead();
            bool isIaDead = ia.IsDead();

            if (isPlayerDead && isIaDead)
            {
                Print("Egalite : Les deux joueurs sont morts");
                EndScene.text = "Egalite !";
                return true;
            }
            else if (isPlayerDead)
            {
                Print("Someone is dead");
                EndScene.text = "Tu es mort !";
                return true;
            }
            else if (isIaDead)
            {
                EndScene.text = "Tu as gagne !";
                return true;
            }
            return false;
        }

        private void SetAction(Personnage personnage)
        {
            int[] actions = new int[3] { 1, 2, 3 };
            Random random = new Random();

            Print("Action IA");
            //int action = actions[random.Next(0, actions.Length)];
            int action = AiChoice(difficulty, ia.role);

            {

                personnage.action = action;

            }
        }

        private void SetAction(object sender, EventArgs ars, int actionNumber)
        {
            Print("Action");

            joueur.action = actionNumber;
        }



        private void SetActions()
        {
            SetAction(ia);
        }

        private void GameResume()
        {
            Print($"Le joueur a {joueur.health} hp");
            Print($"L'IA a {ia.health} hp");
        }

        static public void Print(object? message)
        {
            Debug.WriteLine(message);
        }
        #endregion


        private int AiChoice(int difficulty, string roleIA)
        {
            Random rdm = new Random();
            int PVIA = ia.health;
            int PVJoueur = joueur.health;

        switch (difficulty)
            {
                case 0: //Difficulté Facile -> Entièrement aléatoire
                    return rdm.Next(1, 4);

                case 1: //Difficulté Normale -> Se base sur ses PV + aléatoire

                    if (roleIA == "H") //Si IA Healer
                    {
                        if (PVIA == 3 && rdm.Next(0, 101) < 40)
                            return 3;
                        if (PVIA <= 2 && rdm.Next(0, 101) < 60)
                            return 3;
                        if (PVIA == 4 && rdm.Next(0, 101) < 80)
                            return 1;
                        if (rdm.Next(0, 101) < 60)
                            return 1;
                        return 2;
                    }

                    if (roleIA == "T") //Si IA Tank
                    {
                        if (PVIA >= 4 && rdm.Next(0, 101) < 50)
                            return 3;
                        if (PVIA > 2 && rdm.Next(0, 101) < 30)
                            return 3;
                        if (PVIA <= 2 && rdm.Next(0, 101) < 60)
                            return 2;
                        return 1;
                    }

                    if (roleIA == "D") //Si IA Damager
                    {
                        if (PVIA == 3 && rdm.Next(0, 101) < 45)
                            return 3;
                        if (PVIA >= 2 && rdm.Next(0, 101) < 65)
                            return 1;
                        if (rdm.Next(0, 101) < 65)
                            return 1;
                        return 2;
                    }

                    if (roleIA == "M") //Si IA Mage
                    {
                        if (PVIA == 3 && rdm.Next(0, 101) < 70)
                            return 1;
                        if (PVIA >= 2 && rdm.Next(0, 101) < 50)
                            return 3;
                        if (rdm.Next(0, 101) < 65)
                            return 1;
                        return 2;
                    }
                    break;

                case 2: // Difficulté Difficile -> Se base sur ses PV ainsi que ceux de l'adversaire (+ aléatoire)

                    if (roleIA == "H") //Si IA Healer
                    {
                        if (PVJoueur == 1 && ((PVIA >= 2 && rdm.Next(0, 101) < 85) || (PVIA == 1 && rdm.Next(0, 101) < 50)))
                            return 1;

                        if (PVIA == 3 && rdm.Next(0, 101) < 20)
                            return 3;
                        if (PVIA <= 2 && rdm.Next(0, 101) < 75)
                            return 3;
                        if (PVIA == 4 && rdm.Next(0, 101) < 80)
                            return 1;
                        if (rdm.Next(0, 101) < 45)
                            return 1;
                        return 2;
                    }

                    if (roleIA == "T") //Si IA Tank
                    {
                        if (PVJoueur == 1 && PVIA >= 2 && rdm.Next(0, 101) < 75)
                            return 3;
                        if (PVJoueur == 1 && ((PVIA >= 2 && rdm.Next(0, 101) < 85) || (PVIA == 1 && rdm.Next(0, 101) < 50)))
                            return 1;

                        if (PVIA >= 4 && rdm.Next(0, 101) < 65)
                            return 3;
                        if (PVIA >= 3 && rdm.Next(0, 101) < 30)
                            return 3;
                        if (PVIA == 1 && rdm.Next(0, 101) < 70)
                            return 2;
                        if (PVIA <= 2 && rdm.Next(0, 101) < 60)
                            return 2;
                        return 1;
                    }

                    if (roleIA == "D") //Si IA Damager
                    {
                        if (PVJoueur == 1 && ((PVIA >= 2 && rdm.Next(0, 101) < 85) || (PVIA == 1 && rdm.Next(0, 101) < 70)))
                            return 1;

                        if (PVIA == 3 && rdm.Next(0, 101) < 60)
                            return 3;
                        if (PVIA == 3 && rdm.Next(0, 101) < 90)
                            return 1;
                        if (PVIA >= 2 && rdm.Next(0, 101) < 65)
                            return 1;
                        if (PVIA >= 2 && rdm.Next(0, 101) < 50)
                            return 3;
                        if (rdm.Next(0, 101) < 35)
                            return 1;
                        return 2;
                    }

                    if (roleIA == "M") //Si IA Mage
                    {
                        if (PVJoueur == 1 && PVIA == 1 && rdm.Next(0, 101) < 85)
                            return 3;
                        if (PVJoueur == 1 && PVIA >= 2 && rdm.Next(0, 101) < 75)
                            return 1;


                        if (PVIA == 3 && rdm.Next(0, 101) < 90)
                            return 1;
                        if (PVIA >= 2 && rdm.Next(0, 101) < 50)
                            return 3;
                        if (rdm.Next(0, 101) < 60)
                            return 1;
                        return 2;
                    }
                    break;
            }
            return 2;
        }
    }




}


    


