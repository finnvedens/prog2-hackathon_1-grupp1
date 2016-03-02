using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon_1
{
    public class Screen
    {
        protected int width;
        protected int height;

        protected int _top;
        protected int _left;

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

        public virtual void Draw()
        {
            this.Clear();
        }

        public void Clear()
        {
            for (int y = 0; y < this.height; y++)
            {
                this.GoToPoint(0, y);
                string output = new string(' ', this.width);
                Console.Write(output);
            }
        }

        public void Redraw()
        {
            this.Draw();
        }

        protected void WriteTitle(string title)
        {
            int leftPadding = (width - title.Length) / 2;

            Console.SetCursorPosition(this.left + leftPadding, this.top);
            Console.Write(title);
        }

        protected void WriteCenter(string text, int line, int duration = 0)
        {
            int leftPadding = (width - text.Length) / 2;
            int tick = duration / text.Length;

            Console.SetCursorPosition(this.left + leftPadding, this.top + line);

            for (int i = 0; i < text.Length; i++)
            {
                Console.Write(text[i]);
                System.Threading.Thread.Sleep(tick);
            }
        }

        protected void WriteCenter(string text, int duration = 0)
        {
            int topPadding = (height - text.Split('\n').Length) / 2;
            int leftPadding = (width - text.Split('\n').Max(x => x.Length)) / 2;
            int tick = duration / text.Length;

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

        protected void WriteLeft(string text, int line)
        {
            Console.SetCursorPosition(this.left, this.top + line);
            Console.Write(text);
        }

        protected void GoToRow(int row)
        {
            Console.SetCursorPosition(Console.CursorTop, this.top + row);
        }

        protected void GoToColumn(int column)
        {
            Console.SetCursorPosition(this.left + column, Console.CursorTop);
        }

        protected void GoToPoint(int column, int row)
        {
            GoToRow(row);
            GoToColumn(column);
        }
    }

    public class StatsScreen : Screen
    {
        Player player;

        public StatsScreen(ref Player player)
        {
            this.player = player;

            this.width = 55;
            this.height = 15;

            this.left = 60;
            this.top = 0;
        }

        public override void Draw()
        {
            base.Draw();

            this.WriteTitle("Player Stats");

            this.WriteLeft("Name: " + player.name, 3);
            this.WriteLeft("Health: " + player.health, 4);
            this.WriteLeft("Items (" + player.items.Count + "): " + string.Join(", ", player.items.Select(x => x.name).ToArray()), 5);
        }
    }

    public class MapScreen : Screen
    {
        Room room;
        Player player;

        public MapScreen(Player player, Room room)
        {
            this.player = player;
            this.room = room;

            this.width = 55;
            this.height = 15;

            this.left = 0;
            this.top = 0;
        }

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

    public class TitleScreen : Screen
    {
        public TitleScreen()
        {
            this.width = 119;
            this.height = 34;

            this.left = 0;
            this.top = 0;
        }

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

    public class InputScreen : Screen
    {
        List<string> messages = new List<string>();

        public InputScreen(List<string> messages = null)
        {
            this.width = 119;
            this.height = 16;

            this.top = 16;
            this.left = 0;

            if (messages != null)
                this.messages = messages;
        }

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

        public void Handle(string input)
        {
            if (this.messages.Count > 10)
                this.messages.RemoveAt(0);
            this.messages.Add(input);
        }
    }
}
