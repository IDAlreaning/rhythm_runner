using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rhythm_runner.Controllers
{
    public class BackGroundController
    {
        static public BackGroundController Instance;


        public Bitmap backGround_ImageName_1;
        public Bitmap backGround_ImageName_2;
        public Bitmap Menu_ImageNam = new Bitmap("Images//Menu_backgroundAndRunner.png");
        public Bitmap HowToPlay_ImageNam = new Bitmap("Images//HowToPlay.png");

        public int backGroundX_1;
        public int backGroundX_2;
        public int backGroundY;
        public int backGroundWidth;
        public int backGroundHeight;

        public int MenuX;
        public int MenuY;
        public int MenuWidth;
        public int MenuHeight;

        public BackGroundController(string imageName_1, string imageName_2)
        {
            this.backGroundX_1 = 0;
            this.backGroundX_2 = 3072;
            this.backGroundY = 0;
            this.backGroundWidth = 3072;
            this.backGroundHeight = 768;

            MenuX = 0;
            MenuY = 0;
            MenuWidth = 1024;
            MenuHeight = 768;

            backGround_ImageName_1 = new Bitmap(imageName_1);
            backGround_ImageName_2 = new Bitmap(imageName_2);
        }

        public void drawBackGround_1(Graphics g)
        {
            g.DrawImage(backGround_ImageName_1, -backGroundX_1 + 3072, backGroundY, backGroundWidth, backGroundHeight);
        }

        public void drawBackGround_2(Graphics g)
        {
            g.DrawImage(backGround_ImageName_2, -backGroundX_2 + 3072, backGroundY, backGroundWidth, backGroundHeight);
        }

        public void drawMenu(Graphics g)
        {
            g.DrawImage(Menu_ImageNam, MenuX, MenuY, MenuWidth, MenuHeight);
        }

        public void drawHowToPlay(Graphics g)
        {
            g.DrawImage(HowToPlay_ImageNam, 80, (int)(MenuHeight / 2.5), 500, 303);
        }
    }
}
