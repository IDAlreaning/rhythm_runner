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
        private const int SCREEN_STATUS_MENU = 0;
        private const int SCREEN_STATUS_GAME_NORMAL = 1;

        // Controllers
        private MenuController menuController;

        // Program Status Control
        private int screenStatus;

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
                    this.Controls.Clear();

                }
                return;
            }
            // end

            // 遊戲內容
            if (screenStatus == SCREEN_STATUS_GAME_NORMAL)
            {
                GameController.Instance.Action();


                if (GameController.Instance.Action() == GameController.GAME_STATUS_STOP)
                {
                    // 遊戲結束做的事
                    screenStatus = SCREEN_STATUS_MENU;
                    menuController.ShowMenu();
                }
                return;
            }
            //// end


            Invalidate(); // 全部洗掉再印一次，會去觸發OnPaint
        }

        private void Jumping_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
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
