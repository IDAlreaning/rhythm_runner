using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using rhythm_runner.GameObjects;

namespace rhythm_runner.Controllers
{
    public class GameController
    {
        static public GameController Instance;

        public const int GAME_STATUS_IN_PROGRESS = 1;
        public const int GAME_STATUS_STOP = 0;


        public List<GameObject> gameObjects;
        public bool hasJumped;
        public bool hasCollided;
        public Random random;
        public int amountOfTempo;
        public int amountOfObjects;
        static public int distanceOfObjectes = 300;
        static public int heightOfJump = 200;
        public int distanceOfShowObject;

        public int platformScore;
        public int healboxHp;
        public int ObstracleMinusHp;
        public int ObstracleMinusScore;

        // Music
        public System.Windows.Media.MediaPlayer backGroundMusic;
        public System.Media.SoundPlayer playerJumpMusic;
        public System.Media.SoundPlayer obstacleMusic;
        private System.IO.DirectoryInfo dir;


        public Player player;
        public BackGroundController background;
        public MenuController menuController;
        public Gameform form;

        public bool isJumping;

        // Game Status
        public int gameStatus; // 考量未來加入其餘狀態因此使用int


        public GameController(Gameform form)
        {
            this.form = form;
            this.menuController = new MenuController(form);
            dir = new System.IO.DirectoryInfo(System.Windows.Forms.Application.StartupPath).Parent.Parent;
            this.gameObjects = new List<GameObject>();
            this.hasJumped = false;
            this.hasCollided = false;

            // Set Game Status
            this.gameStatus = GAME_STATUS_STOP;

            this.amountOfTempo = 5;
            this.amountOfObjects = 5;

            this.platformScore = 10;
            this.healboxHp = 20;
            this.ObstracleMinusHp = 40;
            this.ObstracleMinusScore = 25;

            random = new Random();


            this.isJumping = false;


            createListGameObject();

            createMedia();


            player = new Player("Images//GameObject_Player.gif", gameObjects[0], gameObjects[1]);
            background = new BackGroundController("Images//gameBackground_First_left.png", "Images//gameBackground_First_right.png");

        }

        public void createListGameObject()
        {
            List<int> positionOfSheet = new List<int>();

            for (int i = 0; i < amountOfTempo; i++)
            {
                positionOfSheet.Add(i);
            }

            for (int i = 0; i < amountOfObjects; i++)
            {
                int positionIndex = random.Next(0, positionOfSheet.Count);
                int position = positionOfSheet[positionIndex] * distanceOfObjectes;
                positionOfSheet.RemoveAt(positionIndex);


                switch (random.Next(0, 5))
                {
                    case 0:
                        gameObjects.Add(new Obstacle(ObstracleMinusHp, ObstracleMinusScore, position));
                        break;
                    case 1:
                        gameObjects.Add(new HealBox(healboxHp, position));
                        break;
                    case 2:
                        gameObjects.Add(new Obstacle(ObstracleMinusHp, ObstracleMinusScore, position));
                        break;
                    default:
                        gameObjects.Add(new Platform(platformScore, position));
                        break;
                }
            }

            gameObjects.Sort();

            for (int i = 0; i < gameObjects.Count; i++)
            {
                if (i + 1 != gameObjects.Count)
                {
                    if (gameObjects[i] is Obstacle && gameObjects[i + 1] is Obstacle)
                    {
                        int doubleObstacle = gameObjects[i + 1].position;
                        if (i % 3 == 0)
                        {
                            gameObjects[i + 1] = new HealBox(healboxHp, doubleObstacle);
                        }
                        else
                        {
                            gameObjects[i + 1] = new Platform(platformScore, doubleObstacle);
                        }
                    }
                }
            }

            for (int i = 0; i < gameObjects.Count; i++)
            {
                if (i + 1 < gameObjects.Count)
                {
                    gameObjects[i].nextGameObject = gameObjects[i + 1];
                }
            }
        }

        public void draw(PaintEventArgs e)
        {
            Graphics g = e.Graphics; // 使用graphics來繪圖

            if (form.screenStatus == Gameform.SCREEN_STATUS_MENU)
            {
                background.drawMenu(g);
            }

            if (form.screenStatus == Gameform.SCREEN_STATUS_HOWTO)
            {
                background.drawMenu(g);

                background.drawHowToPlay(g);
            }

            if (form.screenStatus == Gameform.SCREEN_STATUS_GAME_NORMAL)
            {
                background.backGroundX_1 = background.backGroundX_1 % 6144;
                background.drawBackGround_1(g);

                background.backGroundX_2 = background.backGroundX_2 % 6144;
                background.drawBackGround_2(g);

                foreach (GameObject gameObject in gameObjects)
                {
                    gameObject.drawGameObject(g, player);
                }
                player.drawPlayer(g);
            }
        }

        private void createMedia()
        {
            backGroundMusic = new System.Windows.Media.MediaPlayer();
            obstacleMusic = new System.Media.SoundPlayer();
            playerJumpMusic = new System.Media.SoundPlayer();

            backGroundMusic.Open(new System.Uri(dir.FullName + "\\Audios\\" + "sound_background_Unity.wav"));

            playerJumpMusic.SoundLocation = dir.FullName + "\\Audios\\" + "sound_collision_Keypress.wav";
            playerJumpMusic.Load();

            obstacleMusic.SoundLocation = dir.FullName + "\\Audios\\" + "sound_collision_Obstacle.wav";
            obstacleMusic.Load();
        }

        public int Action()
        {

            int distanceOfJumping = player.endJumpPosition - player.startJumpPosition;

            player.playerX += player.speed * distanceOfJumping / distanceOfObjectes;

            background.backGroundX_1 += player.speed / 2 * distanceOfJumping / distanceOfObjectes;
            background.backGroundX_2 += player.speed / 2 * distanceOfJumping / distanceOfObjectes;

            if (player.playerX > player.startGameObject.position)
            {
                player.startJumpPosition = player.startGameObject.position;
                player.endJumpPosition = player.targetGameObject.position;
            }

            int x = player.playerX - player.startJumpPosition;

            GameObject currentObject = null;

            foreach (GameObject gameObject in gameObjects)
            {
                if (gameObject.checkCollided(player))
                {
                    currentObject = gameObject;
                    break;
                }
            }

            if (currentObject != null)
            {
                if (currentObject.collide(player))
                {
                    if (player.keyPressOfJump == true)
                    {
                        if (!(currentObject is Obstacle))
                        {
                            player.state = Player.State.JUMP_FARTHER;
                            player.targetGameObject = player.targetGameObject.nextGameObject;
                            player.keyPressOfJump = false;
                        }
                    }
                }
            }
            else
            {
                player.keyPressOfJump = false;
            }

            float center = distanceOfJumping / 2;
            float a = heightOfJump / (center * center);

            player.playerY = (int)(-(x - center) * (x - center) * a) + heightOfJump;



            if (player.hp <= 0 || player.startGameObject == gameObjects[amountOfObjects - 1])
            {
                gameStatus = GAME_STATUS_STOP;
                GameController.Instance.backGroundMusic.Close();

                player.hp = 200;
                player.score = 0;
            }

            return gameStatus;
        }


        private void HP_Click(object sender, EventArgs e)
        {

        }

        private void SCORE_Click(object sender, EventArgs e)
        {

        }
    }
}
