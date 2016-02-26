using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon_1
{
    public class Room
    {
        public Player player;
        public int width;
        public int height;
        public List<Square> squares;
        public string name;

        public int startPositionX;
        public int startPositionY;

        public Action onEntered;

        public Room(string name, int width, int height, int startPositionX, int startPositionY, List<Square> squares, Action onEntered)
        {
            this.name = name;
            this.width = width;
            this.height = height;
            this.squares = squares;

            this.startPositionX = startPositionX;
            this.startPositionY = startPositionY;

            this.onEntered = onEntered;
        }

        public void Enter()
        {
            this.player.positionX = this.startPositionX;
            this.player.positionY = this.startPositionY;
            Art.WriteCenter("YOU HAVE ENTERED " + this.name.ToUpper());
            onEntered();
        }

        public void Leave()
        {
            Art.WriteCenter("YOU LEFT " + this.name.ToUpper());
        }

        public void Move(int x, int y)
        {
            int movedX = 0;
            int movedY = 0;

            if(this.player.positionX + x < this.width && this.player.positionX + x >= 0)
            {
                movedX += x;
                this.player.positionX += x;
            }

            if (this.player.positionY + y < this.height && this.player.positionY + y >= 0)
            {
                movedY += y;
                this.player.positionY += y;
            }

            Console.WriteLine(this.player.name.ToUpper() + " moved to square " + String.Format("{0};{1}", this.player.positionX, this.player.positionY));

            Square square = this.squares.Where(sq => sq.positionX == this.player.positionX && sq.positionY == this.player.positionY).FirstOrDefault();

            square.onEntered();
            this.player.items.Add(square.item);
            this.squares.Remove(square);
        }
    }

    public struct Square
    {
        public Item item;
        public string enterString;
        public int positionX;
        public int positionY;
        public Action onEntered;
    }
}
