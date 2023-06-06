using System;
using System.ComponentModel.Design;
using System.Transactions;

namespace game
{
    internal class Program
    {
        private static int selectedOption = 0;
        private static string[] menuOptions = { "Start", "Medium", "Exit" };
        private static int difficulty = 1;
        public static float diffdmg = 1;
        private static bool running = true;

        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            
            while (running)
            {
                Console.Clear();
                DrawMenu();

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
                        ProcessSelectedOption();
                        break;
                    case ConsoleKey.Escape:
                        running = false;
                        break;
                }
            }
        }

        static void DrawMenu()
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

        static void ProcessSelectedOption()
        {
            switch (selectedOption)
            {
                case 0: //start
                    Console.Clear();
                    Play.startGame(diffdmg);
                    break;
                case 1: //difficulty
                    difficulty++;
                    if (difficulty > 2)
                        difficulty = 0;

                    switch (difficulty)
                    {
                        case 0:
                            menuOptions[1] = "Easy";
                            diffdmg = 0.75F;
                            break;
                        case 1:
                            menuOptions[1] = "Medium";
                            diffdmg = 1F;
                            break;
                        case 2:
                            menuOptions[1] = "Hard";
                            diffdmg = 1.25F;
                            break;
                    }
                    // Tutaj możesz umieścić kod do ustawienia poziomu trudności
                    break;
                case 2: //exit
                    running = false;
                    break;
            }

            //Console.WriteLine("Naciśnij dowolny klawisz, aby kontynuować...");
            //Console.ReadKey(true);
        }
    }
}