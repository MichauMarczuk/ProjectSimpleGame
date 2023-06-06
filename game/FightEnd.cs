using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
    abstract class FightEnd
    {
        public static bool end(bool b, Player player, int xp)
        {
            Console.Clear();
            if(b)
            {
                player.addxp(xp);
                //player.hp = player.maxhp; nie ma leczenia >:(
                Console.Clear();
                Console.WriteLine("Get ready for next fight!");
                Console.ReadKey();
                Console.Clear();
                return true;
            }
            else
            {
                Console.WriteLine("Game Over!");
                Console.ReadKey();
                return false;
            }
        }
    }
}
