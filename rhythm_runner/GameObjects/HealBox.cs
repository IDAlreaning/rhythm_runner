using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rhythm_runner.GameObjects
{
    public class HealBox : GameObject
    {
        private int hp;

        public override void handleCollision(Player player)
        {
            player.hp += hp;


            // throw new NotImplementedException();
        }

        public HealBox(int hp, int position):base("Images//GameObject_Healbox.png", position)
        {
            this.hp = hp;
        }
    }
}
