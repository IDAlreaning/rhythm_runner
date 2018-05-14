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
        public Bitmap backGround_ImageName_1;
        public Bitmap backGround_ImageName_2;

        public int backGroundX_1;
        public int backGroundX_2;
        public int backGroundY;
        public int backGroundWidth;
        public int backGroundHeight;

        public BackGroundController(string imageName_1, string imageName_2)
        {
            this.backGroundX_1 = 0;
            this.backGroundX_2 = 3072;
            this.backGroundY = 0;
            this.backGroundWidth = 3072;
            this.backGroundHeight = 768;

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
    }
}
