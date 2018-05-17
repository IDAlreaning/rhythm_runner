using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using rhythm_runner.Controllers;
using rhythm_runner.GameObjects;

namespace rhythm_runner
{
    public partial class Gameform : Form
    {
        public BackGroundController background;

        // Status Define
        public const int SCREEN_STATUS_MENU = 0;
        public const int SCREEN_STATUS_GAME_NORMAL = 1;
        public const int SCREEN_STATUS_HOWTO = 2;
        public const int SCREEN_STATUS_SCORING = 3;
        public int showScore;

        // Controllers
        public MenuController menuController;

        // Program Status Control
        public int screenStatus;

        public Gameform()
        {
            // Init Program Status
            screenStatus = SCREEN_STATUS_MENU;
            // end


            GameController.Instance = new GameController(this);
            menuController = new MenuController(this);

            InitializeComponent();

            this.Size = new Size(1024, 768);
            DoubleBuffered = true; // 將原本的物件保留一下，等下一個出現後再消失

            menuController.ShowMenu();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            
            GameController.Instance.draw(e);
        }

        private void move_Tick(object sender, EventArgs e)
        {
            // 主選單
            if (screenStatus == SCREEN_STATUS_MENU)
            {
                if (menuController.GetMenuStatus() == MenuController.MENU_STATUS_START_CLICKED)
                {
                    screenStatus = SCREEN_STATUS_GAME_NORMAL;
                    GameController.Instance.gameStatus = GameController.GAME_STATUS_IN_PROGRESS;
                    this.Controls.Clear();
                }
            }
            // end


            // 遊戲內容
            if (screenStatus == SCREEN_STATUS_GAME_NORMAL)
            {
                if (GameController.Instance.Action() == GameController.GAME_STATUS_STOP)
                {
                    // 遊戲結束做的事
                    if (GameController.Instance.drawWhat == GameController.ESC)
                    {
                        screenStatus = SCREEN_STATUS_MENU;
                        menuController.ShowMenu();
                    }

                    if (GameController.Instance.drawWhat == GameController.GAMEOVER)
                    {
                        screenStatus = SCREEN_STATUS_SCORING;
                        menuController.ShowMenu();

                    }

                    GameController.Instance.gameObjects.Clear();
                    GameController.Instance = new GameController(this);
                    GameController.Instance.reStart();
                }
            }
            // end

            Invalidate(); // 全部洗掉再印一次，會去觸發OnPaint
        }

        private void Jumping_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {

                if (screenStatus == SCREEN_STATUS_GAME_NORMAL)
                {
                    GameController.Instance.gameStatus = GameController.GAME_STATUS_STOP;
                    GameController.Instance.drawWhat = GameController.ESC;
                    GameController.Instance.backGroundMusic.Stop();
                }
            }
        }

        private void Gameform_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                if (GameController.Instance.player.state == Player.State.JUMP_NORMALLY)
                {
                    GameController.Instance.playerJumpMusic.Play();
                    GameController.Instance.player.keyPressOfJump = true;
                }
            }
        }


    }
}
