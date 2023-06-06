using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
    internal class Player : Enemy
    {       
        private int xp { get; set; }
        private int threshold { get; set; }
        public int stamina { get; set; }
        private int selectedOption = 0;
        public int maxhp;
        //private static string[] menuOptions;
        public Player(string name, int level) : base(name, level)
        {
            this.name = name;
            this.level = level;
            maxhp = 100;
            hp = 100;
            strength = 10;
            armor = 0;
            dodge = 1;
            regen = 5;
            xp = 0;
            threshold = 100;
        }

        private void lvlup()
        {
            string [] menuOptions = { "hp: " + maxhp, "strength: " + strength, "armor: "+ armor, "dodge: " + dodge+"/50%", "regen: " + regen };
            bool run = true;
            while (run)
            {
                
                if (xp >= threshold)
                {
                    xp -= threshold;
                    threshold = (int)(threshold * 1.5);
                    level++;
                    bool run2 = true;
                    while (run2)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("LEVEL UP!");
                        Console.ResetColor();

                        

                        writeStats(menuOptions);
                        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                        switch (keyInfo.Key)
                        {
                            case ConsoleKey.UpArrow:
                                if (selectedOption > 0)
                                    selectedOption--;
                                break;
                            case ConsoleKey.DownArrow:
                                if (selectedOption < menuOptions.Length - 1)
                                    selectedOption++;
                                break;
                            case ConsoleKey.Enter:
                                run2= false;
                                switch(selectedOption)
                                {
                                    case 0:
                                        maxhp += 20;
                                        break;
                                    case 1: 
                                        strength += 10;
                                        break;
                                    case 2:
                                        armor += 2;
                                        break;
                                    case 3: 
                                        if(dodge <= 50)
                                        {
                                            dodge += 2;
                                        }
                                        else
                                        {
                                            run2 = true;
                                        }
                                        break;
                                    case 4:
                                        regen += 5;
                                        break;
                                }
                                break;
                        }
                    }
                    if (xp < threshold)
                        run = false;
                }
                else
                {
                    run = false;
                }
            }
        }

        private void writeStats(string [] menuOptions)
        {
            for (int i = 0; i < menuOptions.Length; i++)
            {
                Console.Write(menuOptions[i]);
                if (i == selectedOption)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("++");
                }
                else
                {
                    Console.WriteLine(" ");
                }

                Console.ResetColor();
            }
        }

        public void addxp(int n)
        {
            xp += n;
            lvlup();
        }

        public override int attack(float diffdmg)
        {
            Random rnd = new Random();

            int a = (int)Math.Round(strength * 0.9F);
            int b = (int)Math.Round(strength * 1.1F);
            int dmg = rnd.Next(a, b);

            return dmg;
        }

        public int getlevel()
        {
            return level;
        }
    }
}
