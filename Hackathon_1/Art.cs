using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Hackathon_1
{
    class Art
    {
        public static string Title = @"██████╗ ██████╗  █████╗  ██████╗  ██████╗ ███╗   ██╗███████╗     ██████╗ ██╗   ██╗███████╗███████╗████████╗
██╔══██╗██╔══██╗██╔══██╗██╔════╝ ██╔═══██╗████╗  ██║██╔════╝    ██╔═══██╗██║   ██║██╔════╝██╔════╝╚══██╔══╝
██║  ██║██████╔╝███████║██║  ███╗██║   ██║██╔██╗ ██║███████╗    ██║   ██║██║   ██║█████╗  ███████╗   ██║   
██║  ██║██╔══██╗██╔══██║██║   ██║██║   ██║██║╚██╗██║╚════██║    ██║▄▄ ██║██║   ██║██╔══╝  ╚════██║   ██║   
██████╔╝██║  ██║██║  ██║╚██████╔╝╚██████╔╝██║ ╚████║███████║    ╚██████╔╝╚██████╔╝███████╗███████║   ██║   
╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═╝ ╚═════╝  ╚═════╝ ╚═╝  ╚═══╝╚══════╝     ╚══▀▀═╝  ╚═════╝ ╚══════╝╚══════╝   ╚═╝   
                                                                                                           ";

        public static void WriteCenter(string text)
        {
            Console.Clear();

            int height = text.Split('\n').Length;
            int width = text.Split('\n').Max(x => x.Length);

            int windowHeight = 58;
            int windowWidth = 120;

            int topPadding = (windowHeight - height) / 2;
            int leftPadding = (windowWidth - width) / 2;

            for (int i = 0; i < topPadding; i++)
                Console.WriteLine();

            foreach(String line in text.Split('\n'))
            {
                for (int i = 0; i < leftPadding; i++)
                    Console.Write(" ");
                Console.WriteLine(line);
            }
        }

        public static void WriteAnimatedCenter(string text, int duration, Action callback, bool skipNewline = false)
        {
            int windowWidth = 120;

            int tick = duration / text.Length;
            int index = 0;

            Timer timer = new Timer(tick);
            timer.Elapsed += new ElapsedEventHandler((object sender, ElapsedEventArgs args) =>
            {
                if(index == 0)
                {
                    int width = text.Length;
                    int leftPadding = (windowWidth - width) / 2;

                    for (int i = 0; i < leftPadding; i++)
                        Console.Write(" ");
                }
                Console.Write(text[index]);
                index++;
                if (index == text.Length)
                {
                    timer.Stop();
                    if(!skipNewline)
                        Console.WriteLine();
                    callback();
                }
            });
            timer.Start();
        }
    }
}
