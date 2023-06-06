using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
    class Chat
    {
        private string[] messages { get; set; }
        //private int i {get; set; }
        public Chat()
        {
            //i = 0;
            messages = new string[5];
            addMessage(" ");
            addMessage(" ");
            addMessage(" ");
            addMessage(" ");
            addMessage(" ");
        }

        public void addMessage(string text)
        {

            for(int i = 0; i < messages.Length - 1; i++)
            {
                //i = (i + 1) % messages.Length;
                messages[i] = messages[i + 1];
            }
            messages[4] = text;
        }

        public void writeMessages(string playerName)
        {
            Console.SetCursorPosition(0,15);
            foreach(string message in messages)
            {
                string[] words = message.Split(" ");

                foreach(string word in words)
                {
                    if (word == playerName)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(word + " ");
                        Console.ResetColor();
                    }
                    else if (word.StartsWith("Knight"))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(word + " ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(word + " ");
                    }
                }
                Console.Write("\n");
            }
        }

    }
}
