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

            //Start playing background music
            Sounds.Play(Sound.Background, true);
            
            //Instantiate player
            Player player = new Player("Steve", new List<Item>());

            //Instantiate the first room
            Room first = Rooms.ThreeDooredRoom;
            //Move player to room
            first.Enter(ref player);
            //Handle when the player is moved
            first.onMoved = () =>
            {
                first = Rooms.TwoDoredRoom;
            };

            //Instatiate the statistics screen
            StatsScreen statsScreen = new StatsScreen(ref player);
            //statsScreen.Draw();

            //Instantiate the map screen
            MapScreen mapScreen = new MapScreen(player, first);
            //mapScreen.Draw();

            //Instatiate the title screen and draw it
            TitleScreen titleScreen = new TitleScreen();
            titleScreen.Draw();

            //Instantiate the input screen
            InputScreen inputScreen = new InputScreen();

            //Wait for input before continuing past input screen
            Console.ReadKey();

            //Clear the screen
            Console.Clear();

            //Draw the game UI
            statsScreen.Draw();
            mapScreen.Draw();
            inputScreen.Draw();

            //Main flow
            while (true)
            {
                //Grab input from console
                string input = Console.ReadLine().ToLower();

                //Let input screen draw input
                inputScreen.Handle(input);

                //Handle output from the first room and write it to the input screen
                first.messageHandler = (string message) =>
                {
                    inputScreen.Handle(message);
                };

                //Handle movement
                if (input.Contains("move"))
                {
                    switch (input.Replace("move", "").Trim())
                    {
                        case "left":
                            //Movement relative to current position
                            first.Move(-1, 0);
                            //Play move sound
                            Sounds.Play(Sound.Move);
                            break;
                        case "up":
                            first.Move(0, -1);
                            Sounds.Play(Sound.Move);
                            break;
                        case "right":
                            first.Move(1, 0);
                            Sounds.Play(Sound.Move);
                            break;
                        case "down":
                            first.Move(0, 1);
                            Sounds.Play(Sound.Move);
                            break;
                    }

                    //Redraw game UI
                    statsScreen.Redraw();
                    mapScreen.Redraw();
                } else if (input.Contains("use"))
                {
                    //Get current items from inventory
                    var items = player.items.Where(x => x.name.ToLower() == input.Replace("use", "").Trim());
                    if (items.Count() > 0)
                    {
                        //Handle the use of the item
                        if (items.ElementAt(0).useString != null)
                            inputScreen.Handle(items.ElementAt(0).useString);
                        if (items.ElementAt(0).onUse != null)
                            items.ElementAt(0).onUse(player);
                        player.items.Remove(items.ElementAt(0));
                    } else
                    {
                        //The item was not found
                        inputScreen.Handle("The item '" + input.Replace("use", "").Trim() + "' was not found in your inventory");
                    }

                    //Redraw game UI
                    statsScreen.Redraw();
                    mapScreen.Redraw();
                }

                //Redraw input screen
                inputScreen.Redraw();
            }
        }
    }
}
