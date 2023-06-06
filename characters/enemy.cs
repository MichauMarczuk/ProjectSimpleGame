using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace game
{
    internal class Enemy
    {
        public string name { get; set; }
        public int level { get; set; }
        public int hp { get; set; }
        public int strength { get; set; }
        public int regen { get; set; }
        public int armor { get; set; }
        public int dodge { get; set; }
        private int intent { get; set; }
        private int wait { get; set; }
        
        public Enemy(string name, int level)
        {
            this.name = name;
            this.level = level;
            if(level == 1)
            {
                hp = 100;
                strength = 10;
                armor = 2;
            }
            else
            {
                hp = (int)(100 +Math.Pow(1.8, level));   
                strength = (int)(10 + Math.Pow(1.8, level));
                armor = (int)(2 + Math.Pow(1.8, level));
            }
            regen = hp / 10;
            dodge = 10;
            setIntent();
        }

        public virtual int attack(float diffdmg) {
            Random rnd = new Random();

            int a = (int)Math.Round(strength * diffdmg * 0.9F);
            int b = (int)Math.Round(strength * diffdmg * 1.1F);
            int dmg = rnd.Next(a,b);

            return dmg;
        }

        public int strongattack(float diffdmg)
        {
            Random rnd = new Random();

            int a = (int)Math.Round(2*strength * diffdmg * 0.9F);
            int b = (int)Math.Round(2*strength * diffdmg * 1.1F);
            int dmg = rnd.Next(a, b);

            return dmg;
        }

        public void defend()
        {
            hp += regen;
        }

        public void setIntent()
        {
            Random r = new Random();
            int a = r.Next(0, 100);
            if (a > 80)
            {
                intent = 2;
                wait = 5;
            }
            else if(a > 60)
            {
                intent = 1;
                wait = 2;
            }
            else
            {
                intent = 0;
                wait = 3;
            }
        }

        public void setWait()
        {
            wait -= 1;
        }

        public int getWait()
        {
            return wait;
        }

        public string getIntent()
        {
            string s="";
            switch (intent)
            {
                case 0:
                    s = "Attack";
                    break;
                case 1:
                    s = "  Heal";
                    break;
                case 2:
                    s = " Super";
                    break;
            }
            return s;
        }
    }
}
