using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rhythm_runner.GameObjects
{
    public class Platform_2 : GameObject
    {
        private int score;

        public override void handleCollision(Player player)
        {
            player.score += score;

            // throw new NotImplementedException();
        }

        public Platform_2(int score, int position) : base("Images//GameObject_sush2.png", position)
        {
            this.score = score;
        }
    }
}
