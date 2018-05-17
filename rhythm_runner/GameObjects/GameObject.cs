using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rhythm_runner.Controllers;

namespace rhythm_runner.GameObjects
{
    abstract public class GameObject : IComparable
    {
        public int position; // objectX
        public int objectY;
        public int objectWidth;
        public int objectHeight;
        public bool canJumpOrNot;
        public Bitmap image_GameObject;
        public GameObject nextGameObject;
        public Gameform form;

        // 從GameController裡面給出來的position存進各個GameObject
        public GameObject(string ImageName, int position)
        {
            this.position = position;
            this.objectY = 440;
            this.objectWidth = 96;
            this.objectHeight = 96;
            this.canJumpOrNot = false;
            image_GameObject = SourceController.getImage(ImageName);

        }
        abstract public void handleCollision(Player player);

        public bool collide(Player player)
        {
            if (player.targetGameObject == this)
            {
                handleCollision(player);

                canJumpOrNot = true;
                player.startGameObject = this;
                player.targetGameObject = this.nextGameObject;
                player.state = Player.State.JUMP_NORMALLY;
            }

            // 回傳跳躍成功&&人物目前速度為JUMP_NORMALLY
            return canJumpOrNot && player.state == Player.State.JUMP_NORMALLY;
        }

        public void drawGameObject(Graphics g,Player player)
        {
            if (position - player.playerX < 1100 && position - player.playerX > -100)
            {
                int iCanSeeYou = position - player.playerX + player.pictureOffsetX;// 玩家在螢幕上看到的人物位置 = 現在
                g.DrawImage(image_GameObject, iCanSeeYou - objectWidth / 2, objectY, objectWidth, objectHeight);
            }
        }

        public bool checkCollided(Player player)
        {
            return Math.Abs(player.playerX - position - player.playerWidth/3) < (player.playerWidth / 2 + objectWidth / 2);
        }

        public int CompareTo(object obj)
        {
            GameObject gameObject = obj as GameObject;

            return this.position.CompareTo(gameObject.position);
        }
    }
}
