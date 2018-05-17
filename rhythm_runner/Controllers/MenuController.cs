using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rhythm_runner.Controllers
{
    public class MenuController
    {
        // Static variable
        public const int buttonWidth = 300;
        public const int buttonHeight = 60;


        public const int MENU_STATUS_HOWTO_CLICKED = 2;
        public const int MENU_STATUS_START_CLICKED = 1;
        public const int MENU_STATUS_WAIT = 0;

        // Parent Panel
        public Gameform form;
        public GameController gameController;

        // Menu Object
        public Button play_startBtn;
        public Button howTo_startBtn;
        public Button close_startBtn;

        // Menu Status
        public int menuStatus;


        public MenuController(Gameform form)
        {
            // Set Parent Form
            this.form = form;

            // Set Menu Status
            this.menuStatus = MENU_STATUS_WAIT;
        }


        public void ShowMenu()
        {
            // clear form
            form.Controls.Clear();
            // end

            // set up button
            play_startBtn = new Button();
            howTo_startBtn = new Button();
            close_startBtn = new Button();

            play_startBtn.Location = new System.Drawing.Point(form.ClientSize.Width - (int)(buttonWidth * 1.3), 100 + (form.ClientSize.Height - buttonHeight) / 3);
            howTo_startBtn.Location = new System.Drawing.Point(form.ClientSize.Width - (int)(buttonWidth * 1.3), 100 + (form.ClientSize.Height - buttonHeight) / 2);
            close_startBtn.Location = new System.Drawing.Point(form.ClientSize.Width - (int)(buttonWidth * 1.3), 100 + (form.ClientSize.Height - buttonHeight) / 3 * 2);


            play_startBtn.Size = new System.Drawing.Size(buttonWidth, buttonHeight);
            howTo_startBtn.Size = new System.Drawing.Size(buttonWidth, buttonHeight);
            close_startBtn.Size = new System.Drawing.Size(buttonWidth, buttonHeight);


            play_startBtn.Text = "START GAME";
            howTo_startBtn.Text = "HOW TO PLAY";
            close_startBtn.Text = "LEAVE";


            play_startBtn.Click += new System.EventHandler(play_startBtn_Click);
            howTo_startBtn.Click += new System.EventHandler(howTo_startBtn_Click);
            close_startBtn.Click += new System.EventHandler(close_startBtn_Click);

            // Assign an image to the button.
            play_startBtn.Image = Image.FromFile("Images//button.png");
            howTo_startBtn.Image = Image.FromFile("Images//button.png");
            close_startBtn.Image = Image.FromFile("Images//button.png");

            play_startBtn.Font = new Font("Arial", 16);
            howTo_startBtn.Font = new Font("Arial", 16);
            close_startBtn.Font = new Font("Arial", 16);

            // Give the button a flat appearance.
            play_startBtn.FlatStyle = FlatStyle.Flat;
            howTo_startBtn.FlatStyle = FlatStyle.Flat;
            close_startBtn.FlatStyle = FlatStyle.Flat;

            // add object
            form.Controls.Add(play_startBtn);
            form.Controls.Add(howTo_startBtn);
            form.Controls.Add(close_startBtn);
        }

        public void play_startBtn_Click(object sender, EventArgs e)
        {
            form.screenStatus = Gameform.SCREEN_STATUS_MENU;
            menuStatus = MENU_STATUS_START_CLICKED;

            GameController.Instance.playerJumpMusic.Play();
            GameController.Instance.backGroundMusic.Play();

            // throw new NotImplementedException();
        }

        
        public void main_startBtn_Click(object sender, EventArgs e)
        {
            //menuStatus = MENU_STATUS_WAIT;
            GameController.Instance.playerJumpMusic.Play();
            form.screenStatus = Gameform.SCREEN_STATUS_MENU;
            ShowMenu();
            // throw new NotImplementedException();
        }

        public void howTo_startBtn_Click(object sender, EventArgs e)
        {
            form.screenStatus = Gameform.SCREEN_STATUS_HOWTO;
            GameController.Instance.playerJumpMusic.Play();
            // throw new NotImplementedException();
        }

        public void close_startBtn_Click(object sender, EventArgs e)
        {
            form.Close();
            GameController.Instance.playerJumpMusic.Play();

            // throw new NotImplementedException();
        }

        public int GetMenuStatus()
        {
            int result = menuStatus;
            menuStatus = MENU_STATUS_WAIT;
            return result;
        }
    }
}
