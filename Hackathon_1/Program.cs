using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon_1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Setup Console Window
            Console.SetBufferSize(120, 35);
            Console.SetWindowSize(120, 35);
            Console.Title = "Dragon's Quest Ultra HD 2016";

            Sounds.Loop(Sound.Background);

            Player player = new Player("Steve", new List<Item>());

            Room first = Rooms.ThreeDooredRoom;
            first.Enter(ref player);

            StatsScreen statsScreen = new StatsScreen(ref player);
            //statsScreen.Draw();

            MapScreen mapScreen = new MapScreen(player, first);
            //mapScreen.Draw();

            TitleScreen titleScreen = new TitleScreen();
            titleScreen.Draw();

            InputScreen inputScreen = new InputScreen();

            Console.ReadKey();

            Console.Clear();

            statsScreen.Draw();
            mapScreen.Draw();
            inputScreen.Draw();

            while (true)
            {
                string input = Console.ReadLine().ToLower();

                inputScreen.Handle(input);

                first.messageHandler = (string message) =>
                {
                    inputScreen.Handle(message);
                };

                if (input.Contains("move"))
                {
                    switch (input.Replace("move", "").Trim())
                    {
                        case "left":
                            first.Move(-1, 0);
                            Sounds.Play(Sound.Background);
                            break;
                        case "up":
                            first.Move(0, -1);
                            Sounds.Play(Sound.Background);
                            break;
                        case "right":
                            first.Move(1, 0);
                            Sounds.Play(Sound.Background);
                            break;
                        case "down":
                            first.Move(0, 1);
                            Sounds.Play(Sound.Background);
                            break;
                    }

                    statsScreen.Redraw();
                    mapScreen.Redraw();
                } else if (input.Contains("use"))
                {
                    var items = player.items.Where(x => x.name.ToLower() == input.Replace("use", "").Trim());
                    if (items.Count() > 0)
                    {
                        if (items.ElementAt(0).useString != null)
                            inputScreen.Handle(items.ElementAt(0).useString);
                        if (items.ElementAt(0).onUse != null)
                            items.ElementAt(0).onUse(player);
                        player.items.Remove(items.ElementAt(0));
                    } else
                    {
                        inputScreen.Handle("The item '" + input.Replace("use", "").Trim() + "' was not found in your inventory");
                    }

                    statsScreen.Redraw();
                    mapScreen.Redraw();
                }

                inputScreen.Redraw();
            }
        }
    }
}
