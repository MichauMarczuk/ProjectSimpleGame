using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace game
{
    class Play
    {
        private static string[] menuOptions = { "Attack", "Heal"};
        static int selectedOption = 0;
        static int enemyamount;
        static int xp=0;
        private static bool gamerun = true;
        
        static Chat alert = new Chat();
        public static void startGame(float diffdmg)
        {

            Console.WriteLine("Name: ");
            string playername = Console.ReadLine();
            if(playername == "")
            {
                playername = "BasicDude";
            }
            Player player = new Player(playername , 1);
            Console.Clear();
            while (gamerun)
            {

                Random r = new Random();
                enemyamount = r.Next(1, 4);
                List<Enemy> enemies = createEnemies(diffdmg, player, enemyamount);

                while(player.hp > 0 && enemyamount > 0)
                {
                        int damage;
                        Random dodge = new Random();
                        int dodgevalue;

                        bool run = true;
                        while (run)
                        {
                            drawFight(enemyamount, enemies, player);
                            //drawAim();
                            alert.writeMessages(player.name);
                            drawMenu();

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
                                switch (selectedOption)
                                {
                                    case 0:
                                        bool run2 = true;
                                        int selectedOption2 = 0;
                                        while (run2)
                                        {
                                            Console.Clear();
                                            drawFight(enemyamount, enemies, player);
                                            drawAim(selectedOption2);
                                            alert.writeMessages(player.name);
                                            drawMenu();

                                            ConsoleKeyInfo keyInfo2 = Console.ReadKey(true);
                                            switch (keyInfo2.Key)
                                            {
                                                case ConsoleKey.LeftArrow:
                                                    if (selectedOption2 > 0)
                                                        selectedOption2--;
                                                    break;
                                                case ConsoleKey.RightArrow:
                                                    if (selectedOption2 < enemyamount - 1)
                                                        selectedOption2++;
                                                    break;
                                                case ConsoleKey.Enter:
                                                    dodgevalue = dodge.Next(1, 101);
                                                    if (dodgevalue > player.dodge)
                                                    {
                                                        damage = player.attack(diffdmg);
                                                        enemies[selectedOption2].hp -= damage;
                                                        alert.addMessage(player.name + " attacked " + enemies[selectedOption2].name + " for " + damage + "!");

                                                        if (enemies[selectedOption2].hp <= 0)
                                                        {
                                                            alert.addMessage(player.name + " killed " + enemies[selectedOption2].name + "!");
                                                            removeEnemyFromList(enemies, enemies[selectedOption2]);
                                                        
                                                        }
                                                    }
                                                    else
                                                        alert.addMessage(player.name + " missed.");
                                                    run2 = false;
                                                    break;
                                            }
                                            Console.Clear();
                                        }
                                        break;
                                    case 1:
                                        int healed;
                                        if (player.maxhp < player.hp + player.regen)
                                        {
                                            healed = player.maxhp - player.hp;
                                            player.hp = player.maxhp;
                                        }
                                        else
                                        {
                                            player.hp += player.regen;
                                            healed = player.regen;
                                        }
                                        alert.addMessage(player.name + " healed himself for " + healed);
                                        break;
                                }
                                run = false;
                                    break;
                            }
                            Console.Clear();
                        }
                    
                        //akcje przeciwników
                        for(int i = 0; i < enemyamount; i++)
                        {
                            enemies[i].setWait();
                            if (enemies[i].getWait() <=0)
                            {


                                dodgevalue = dodge.Next(0, 101);
                                switch (enemies[i].getIntent())
                                {
                                    case "Attack":
                                        if (dodgevalue > player.dodge)
                                        {
                                            damage = enemies[i].attack(diffdmg);
                                            player.hp -= damage;
                                            alert.addMessage(enemies[i].name+" attacked "+player.name+" for "+ damage + "!");
                                        }
                                        else
                                        {
                                            alert.addMessage(enemies[i].name+" missed.");
                                        }
                                        enemies[i].setIntent();
                                        break;
                                    case "  Heal":
                                        enemies[i].defend();
                                        alert.addMessage(enemies[i].name + " healed himself for " + enemies[i].regen);
                                        enemies[i].setIntent();
                                        break;
                                    case " Super":
                                        if (dodgevalue > player.dodge)
                                        {
                                            damage = enemies[i].strongattack(diffdmg);
                                            player.hp -= damage;
                                            alert.addMessage(enemies[i].name + " attacked " + player.name + " for " + damage + "!");
                                        } else
                                            alert.addMessage(enemies[i].name + " missed.");
                                        enemies[i].setIntent();
                                        break;
                                }
                            }
                        }

                }
                if(enemyamount == 0)
                {
                    gamerun = FightEnd.end(true, player, xp);
                }
                else if(player.hp <= 0)
                {
                    gamerun = FightEnd.end(false, player, xp);
                }
                else
                {
                    Console.WriteLine("error");
                }
            }
        }

        static public void removeEnemyFromList(List<Enemy> enemies, Enemy enemy)
        {
            enemies.Remove(enemy);
            xp += enemy.level*50;
            enemyamount--;
        }
        static void drawAim(int s)
        {
            int l = 37;
            for(int i = 0; i< s; i++)
            {
                l += 15;
            }
            Console.SetCursorPosition(l, 12);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("^");

            Console.ResetColor();
        }
        static void drawFight(int i, List<Enemy> enemies, Player player)
        {
            drawPlayer(player);
            
            drawEnemy(i, enemies);
            
        }

        static void drawPlayer(Player player)
        {
            Console.SetCursorPosition(3, 1);
            Console.WriteLine("hp: "+player.hp);
            Console.WriteLine("      .-.\r\n    __|=|__\r\n   (_/`-`\\_)\r\n   //\\___/\\\\\r\n   <>/   \\<>\r\n    \\|_._|/\r\n     <_I_>\r\n      |||\r\n     /_|_\\");
        }

        static void drawEnemy(int enemyAmount, List<Enemy> enemies)
        {
            int left = 30;


            for (int i = 0; i < enemyAmount; i++)
            {
                string enemyIntent = enemies[i].getIntent();
                int top = 0;

                Console.SetCursorPosition(left + 3, top);
                Console.WriteLine(enemyIntent + " " + enemies[i].getWait()); ;
                top++;
                Console.SetCursorPosition(left + 3, top);
                Console.WriteLine("hp: " + enemies[i].hp);
                top++;
                Console.SetCursorPosition(left, top);
                top++;
                Console.WriteLine("      .-.");
                Console.SetCursorPosition(left, top);
                top++;
                Console.WriteLine("    __|=|__");
                Console.SetCursorPosition(left, top);
                top++;
                Console.WriteLine("   (_/`-`\\_)");
                Console.SetCursorPosition(left, top);
                top++;
                Console.WriteLine("   //\\___/\\\\");
                Console.SetCursorPosition(left, top);
                top++;
                Console.WriteLine("   <>/   \\<>");
                Console.SetCursorPosition(left, top);
                top++;
                Console.WriteLine("    \\|_._|/");
                Console.SetCursorPosition(left, top);
                top++;
                Console.WriteLine("     <_I_>");
                Console.SetCursorPosition(left, top);
                top++;
                Console.WriteLine("      |||");
                Console.SetCursorPosition(left, top);
                top++;
                Console.WriteLine("     /_|_\\");
                left += 15;
            }
        }

        static void drawMenu()
        {
            for (int i = 0; i < menuOptions.Length; i++)
            {
                if (i == selectedOption)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(">");
                }
                else
                {
                    Console.Write(" ");
                }

                Console.WriteLine(menuOptions[i]);
                Console.ResetColor();
            }

        }

        static List<Enemy> createEnemies(float diffdmg, Player player, int r)
        {
            
            List<Enemy> enemies = new List<Enemy>();
            for (int i = 0; i < r; i++)
            {
                string enemyname = "Knight" + (i + 1);
                int enemylevel;
                if (player.getlevel() == 1)
                    enemylevel = 1;
                else
                {
                    if (diffdmg > 1)
                    {
                        enemylevel = player.level + 1;
                    }
                    else if (diffdmg < 1)
                    {
                        enemylevel = player.level - 1;
                    }
                    else
                    {
                        enemylevel = player.level;
                    }
                }
                enemies.Add(new Enemy(enemyname, enemylevel));
            }

            return enemies;
        }
    }
}
