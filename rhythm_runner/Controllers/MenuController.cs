using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rhythm_runner.Controllers
{
    class MenuController
    {
        // Static variable
        private const int buttonWidth = 100;
        private const int buttonHeight = 100;

        public const int MENU_STATUS_START_CLICKED = 1;
        public const int MENU_STATUS_WAIT = 0;

        // Parent Panel
        public Gameform form;

        // Menu Object
        public Button startBtn;

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
            startBtn = new Button();
            startBtn.Location = new System.Drawing.Point((form.ClientSize.Width - buttonWidth) / 2,
                (form.ClientSize.Height - buttonHeight) / 2);
            startBtn.Size = new System.Drawing.Size(buttonWidth, buttonHeight);
            startBtn.Text = "Start Game";
            startBtn.Click += new System.EventHandler(StartBtn_Click);

            // add object
            form.Controls.Add(startBtn);
        }

        public void StartBtn_Click(object sender, EventArgs e)
        {
            menuStatus = MENU_STATUS_START_CLICKED;
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
