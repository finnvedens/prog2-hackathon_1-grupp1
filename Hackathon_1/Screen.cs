using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon_1
{
    /// <summary>
    /// A screen for displaying in-game content.
    /// </summary>
    public class Screen
    {
        //The width of the screen in characters.
        protected int width;
        //The height of the screen in characters.
        protected int height;

        //The offset from the topmost position.
        protected int _top;
        //The offset from the leftmost position.
        protected int _left;

        /// <summary>
        /// The offset from the window's topmost position.
        /// </summary>
        protected int top
        {
            get
            {
                return this._top;
            }
            set
            {
                this._top = value + 1;
            }
        }

        /// <summary>
        /// The offset from the window's leftmost position.
        /// </summary>
        protected int left
        {
            get
            {
                return this._left;
            }
            set
            {
                this._left = value + 1;
            }
        }

        /// <summary>
        /// Draw the screen.
        /// </summary>
        public virtual void Draw()
        {
            this.Clear();
        }

        /// <summary>
        /// Clear the screen.
        /// </summary>
        public void Clear()
        {
            for (int y = 0; y < this.height; y++)
            {
                this.GoToPoint(0, y);
                string output = new string(' ', this.width);
                Console.Write(output);
            }
        }

        /// <summary>
        /// Redraw the screen.
        /// </summary>
        public void Redraw()
        {
            //Syntax sugar
            this.Draw();
        }

        /// <summary>
        /// Write a title for the screen.
        /// </summary>
        /// <param name="title">The screen's title.</param>
        protected void WriteTitle(string title)
        {
            //Calculate padding
            int leftPadding = (width - title.Length) / 2;

            //Write the title
            Console.SetCursorPosition(this.left + leftPadding, this.top);
            Console.Write(title);
        }

        /// <summary>
        /// Write out a text, horizontally aligned to the center.
        /// </summary>
        /// <param name="text">Text to write out.</param>
        /// <param name="line">Which line to write it on.</param>
        /// <param name="duration">The duration of the animation. 0 if no animation is wanted.</param>
        protected void WriteCenter(string text, int line, int duration = 0)
        {
            //Calculate padding and write ticks
            int leftPadding = (width - text.Length) / 2;
            int tick = duration / text.Length;

            //Chose write position
            Console.SetCursorPosition(this.left + leftPadding, this.top + line);

            //Write the string out
            for (int i = 0; i < text.Length; i++)
            {
                Console.Write(text[i]);
                System.Threading.Thread.Sleep(tick);
            }
        }

        /// <summary>
        /// Write the text out in the center of the screen.
        /// </summary>
        /// <param name="text">The text to write.</param>
        /// <param name="duration">The duration for the animation. 0 if no animation is wanted.</param>
        protected void WriteCenter(string text, int duration = 0)
        {
            //Caluclate paddings and ticks
            int topPadding = (height - text.Split('\n').Length) / 2;
            int leftPadding = (width - text.Split('\n').Max(x => x.Length)) / 2;
            int tick = duration / text.Length;

            //Animate the text being written
            int row = 0;
            foreach (String line in text.Split('\n'))
            {
                this.GoToPoint(leftPadding, topPadding + row);

                for (int i = 0; i < line.Length; i++)
                {
                    Console.Write(line[i]);
                    System.Threading.Thread.Sleep(tick);
                }

                row++;
            }
        }

        /// <summary>
        /// Write out left-aligned text.
        /// </summary>
        /// <param name="text">The text to write</param>
        /// <param name="line">The line to write the text on</param>
        protected void WriteLeft(string text, int line)
        {
            Console.SetCursorPosition(this.left, this.top + line);
            Console.Write(text);
        }

        /// <summary>
        /// Go to a specific row for writing.
        /// </summary>
        /// <param name="row">The row to go to</param>
        protected void GoToRow(int row)
        {
            Console.SetCursorPosition(Console.CursorTop, this.top + row);
        }

        /// <summary>
        /// Go to a specific column for writing.
        /// </summary>
        /// <param name="column">The column to go to</param>
        protected void GoToColumn(int column)
        {
            Console.SetCursorPosition(this.left + column, Console.CursorTop);
        }

        /// <summary>
        /// Go to a specific point for writing.
        /// </summary>
        /// <param name="column">The column to go to</param>
        /// <param name="row">The row to go to</param>
        protected void GoToPoint(int column, int row)
        {
            GoToRow(row);
            GoToColumn(column);
        }
    }

    /// <summary>
    /// The stats screen.
    /// </summary>
    public class StatsScreen : Screen
    {
        Player player;

        /// <summary>
        /// Create a new stats screen.
        /// </summary>
        /// <param name="player">The player for which to display stats</param>
        public StatsScreen(ref Player player)
        {
            this.player = player;

            this.width = 55;
            this.height = 15;

            this.left = 60;
            this.top = 0;
        }

        /// <summary>
        /// Overwridden draw function to display stats.
        /// </summary>
        public override void Draw()
        {
            base.Draw();

            this.WriteTitle("Player Stats");

            this.WriteLeft("Name: " + player.name, 3);
            this.WriteLeft("Health: " + player.health, 4);
            this.WriteLeft("Items (" + player.items.Count + "): " + string.Join(", ", player.items.Select(x => x.name).ToArray()), 5);
        }
    }

    /// <summary>
    /// The map screen.
    /// </summary>
    public class MapScreen : Screen
    {
        Room room;
        Player player;

        /// <summary>
        /// Create a new map screen.
        /// </summary>
        /// <param name="player">The player for which to display the current position</param>
        /// <param name="room">The room for which to display the map</param>
        public MapScreen(Player player, Room room)
        {
            this.player = player;
            this.room = room;

            this.width = 55;
            this.height = 15;

            this.left = 0;
            this.top = 0;
        }

        /// <summary>
        /// The overridden draw function to draw the map.
        /// </summary>
        public override void Draw()
        {
            base.Draw();

            this.WriteTitle(this.room.name);

            int paddingLeft = (this.width - (this.room.width * 2)) / 2 + 1;
            int paddingTop = (this.height - this.room.height) / 2;

            this.GoToPoint(paddingLeft - 2, paddingTop - 1);
            Console.Write("\\--");
            for (int x = 0; x < this.room.width; x++)
                Console.Write("--");
            this.GoToPoint(paddingLeft + this.room.width * 2, paddingTop - 1);
            Console.Write("/");

            for (int y = 0; y < this.room.height; y++)
            {
                this.GoToRow(y + paddingTop);
                this.GoToColumn(paddingLeft - 2);

                Console.Write("| ");

                for (int x = 0; x < this.room.width; x++)
                {
                    if (this.player.positionX == x && this.player.positionY == y)
                        Console.Write("O ");
                    else if (this.room.squares.Where(sq => sq.positionX == x && sq.positionY == y).Count() > 0)
                        Console.Write("# ");
                    else if (this.room.exits.Where(sq => sq.positionX == x && sq.positionY == y).Count() > 0)
                        Console.Write("+ ");
                    else
                        Console.Write(". ");
                }

                Console.Write("|");
            }

            this.GoToPoint(paddingLeft - 2, paddingTop + this.room.height);
            Console.Write("/--");
            for (int x = 0; x < this.room.width; x++)
                Console.Write("--");
            this.GoToPoint(paddingLeft + this.room.width * 2, paddingTop + this.room.height);
            Console.Write("\\");

            this.GoToPoint(paddingLeft - 2, paddingTop + this.room.height + 2);
            Console.Write(String.Format("Position: {0}:{1}", this.player.positionX, this.player.positionY));
        }
    }

    /// <summary>
    /// The title screen.
    /// </summary>
    public class TitleScreen : Screen
    {
        /// <summary>
        /// Create a new title screen.
        /// </summary>
        public TitleScreen()
        {
            this.width = 119;
            this.height = 34;

            this.left = 0;
            this.top = 0;
        }

        /// <summary>
        /// The overridden draw function to draw the title screen.
        /// </summary>
        public override void Draw()
        {
            base.Draw();

            this.WriteCenter(@"██████╗ ██████╗  █████╗  ██████╗  ██████╗ ███╗   ██╗███████╗     ██████╗ ██╗   ██╗███████╗███████╗████████╗
██╔══██╗██╔══██╗██╔══██╗██╔════╝ ██╔═══██╗████╗  ██║██╔════╝    ██╔═══██╗██║   ██║██╔════╝██╔════╝╚══██╔══╝
██║  ██║██████╔╝███████║██║  ███╗██║   ██║██╔██╗ ██║███████╗    ██║   ██║██║   ██║█████╗  ███████╗   ██║   
██║  ██║██╔══██╗██╔══██║██║   ██║██║   ██║██║╚██╗██║╚════██║    ██║▄▄ ██║██║   ██║██╔══╝  ╚════██║   ██║   
██████╔╝██║  ██║██║  ██║╚██████╔╝╚██████╔╝██║ ╚████║███████║    ╚██████╔╝╚██████╔╝███████╗███████║   ██║   
╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═╝ ╚═════╝  ╚═════╝ ╚═╝  ╚═══╝╚══════╝     ╚══▀▀═╝  ╚═════╝ ╚══════╝╚══════╝   ╚═╝   
                                                                                                           ");
            this.WriteCenter("The boy who lost his cat.", Console.CursorTop, 1000);
            Console.WriteLine();
            this.WriteCenter("You lost your cat and you ran after it.", Console.CursorTop, 1000);
            this.WriteCenter("It ran towards a castle and you fell through a trapdoor.", Console.CursorTop, 1000);
            this.WriteCenter("When you got up you saw nothing but darkness.", Console.CursorTop, 1000);
            this.WriteCenter("You must find your cat.", Console.CursorTop, 1000);
            this.WriteCenter("It is here where your story begins.", Console.CursorTop, 1000);
            Console.WriteLine();
            this.WriteCenter("PRESS ENTER TO BEGIN", Console.CursorTop, 1000);
        }
    }

    /// <summary>
    /// The input screen.
    /// </summary>
    public class InputScreen : Screen
    {
        List<string> messages = new List<string>();

        /// <summary>
        /// Create a new input screen with the chosen messages.
        /// </summary>
        /// <param name="messages">The list of messages to display</param>
        public InputScreen(List<string> messages = null)
        {
            this.width = 119;
            this.height = 16;

            this.top = 16;
            this.left = 0;

            if (messages != null)
                this.messages = messages;
        }

        /// <summary>
        /// The overridden draw function to display the input screen.
        /// </summary>
        public override void Draw()
        {
            base.Draw();

            for (int i = 0; i < messages.Count; i++)
                this.WriteLeft(messages[i], i);

            this.GoToPoint(1, 16);
            Console.Write(new string(' ', this.width));
            this.GoToPoint(1, 16);
            Console.Write("ENTER YOUR COMMAND: ");
        }

        /// <summary>
        /// Handle input / draw input.
        /// </summary>
        /// <param name="input"></param>
        public void Handle(string input)
        {
            //Control that no more than 10 messages will be written
            if (this.messages.Count > 10)
                this.messages.RemoveAt(0);
            this.messages.Add(input);
        }
    }
}
