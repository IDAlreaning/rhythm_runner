using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rhythm_runner.Controllers;

namespace rhythm_runner.GameObjects
{
    public class Player
    {
        // 人物跳躍時，速度分為JUMP_NORMALLY、JUMPING_HIGHER、JUMP_FARTHER
        public enum State
        {
            JUMP_NORMALLY,
            JUMPING_HIGHER,
            JUMP_FARTHER
        };
        public State state;

        public int pictureOffsetX;
        public int pictureOffsetY;
        public int playerX;
        public int playerY;
        public int playerWidth;
        public int playerHeight;

        public int hp;
        public int score;
        public int speed;
        public GameObject startGameObject;
        public GameObject targetGameObject;
        public Gameform form;
        public Bitmap image_player;
        public int jumpPosition;
        public int startJumpPosition;
        public int endJumpPosition;
        public bool keyPressOfJump;

        public bool currentlyAnimating;


        public Player(string imageName, GameObject startGameObject, GameObject targetGameObject)
        {
            this.startGameObject = startGameObject;
            this.targetGameObject = targetGameObject;

            this.state = State.JUMP_NORMALLY;// 預設一開始是JUMP_NORMALLY
            this.hp = 200;// 一開始的血量
            this.score = 0;// 一開始的分數
            this.speed = 16;// 角色與平台的速度
            this.startJumpPosition = startGameObject.position;// 一開始人物要跳躍時的位置 = 物件一開始的位置
            this.endJumpPosition = targetGameObject.position;// 人物跳下來時的位置 = startGameObject的下個物件位置

            this.playerWidth = 110; // 人物的width
            this.playerHeight = 110;// 人物的heught
            this.playerX = 0;// 人物移動時與上個位置之間所產生的X軸差距
            this.playerY = 0;// 人物移動時與上個位置之間所產生的Y軸差距
            this.pictureOffsetX = 80;// 人物在螢幕上顯示給玩家看的X軸位置
            this.pictureOffsetY = 385;// 人物在螢幕上顯示給玩家看的Y軸位置


            // 人物的圖片生成(playerImageName從Gameform裡面給)
            image_player = new Bitmap(imageName);
        }

        public void drawPlayer(Graphics g)
        {
            AnimateImage();

            ImageAnimator.UpdateFrames();

            g.DrawImage(image_player, pictureOffsetX - playerWidth / 2, pictureOffsetY - playerY, playerWidth, playerHeight);
        }

        public void AnimateImage()
        {
            if (!currentlyAnimating)
            {
                ImageAnimator.Animate(image_player, new EventHandler(OnFrameChanged));
                currentlyAnimating = true;
            }
        }

        public void OnFrameChanged(object sender, EventArgs e)
        {
        }
    }
}
