using rhythm_runner.Controllers;

namespace rhythm_runner.GameObjects
{
    public class Obstacle : GameObject
    {
        private int hp;
        private int score;

        public override void handleCollision(Player player)
        {
            GameController.Instance.obstacleMusic.Play();
            player.hp -= hp;
            player.score -= score;


            // throw new NotImplementedException();
        }

        // 將碰撞時所產生的hp變化、score變化存入
        // 將list生成時，產生給物件的位置存入
        public Obstacle(int hp, int score, int position) : base("Images//GameObject_wasabi.png", position)
        {
            this.hp = hp;
            this.score = score;
        }
    }
}
