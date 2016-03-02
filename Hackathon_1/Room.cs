using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon_1
{
    /// <summary>
    /// A room base class.
    /// </summary>
    public class Room
    {
        //The player in the room
        public Player player;
        //The width and height of the room - should be square
        public int width;
        public int height;
        //The squares / items of interest within the room
        public List<Square> squares;
        //The name of the room
        public string name;

        //The handler for messages
        public Action<string> messageHandler;

        //The start position for the player
        public int startPositionX;
        public int startPositionY;

        //The method to call when a player enters the room
        public Action onEntered;

        //The available exits within the room
        public List<Square> exits;

        //Method to call when the player moves
        public Action onMoved;

        /// <summary>
        /// Create a new room.
        /// </summary>
        /// <param name="name">The name of the room</param>
        /// <param name="width">The width of the room - should be equal to height</param>
        /// <param name="height">The height of the room -should be equal to width</param>
        /// <param name="startPositionX">The player's start position</param>
        /// <param name="startPositionY">The player's start position</param>
        /// <param name="squares">The list of available squares of interest</param>
        /// <param name="exits">The list of available exits</param>
        public Room(string name, int width, int height, int startPositionX, int startPositionY, List<Square> squares, List<Square> exits)
        {
            this.name = name;
            this.width = width;
            this.height = height;
            this.squares = squares;
            this.exits = exits;

            this.startPositionX = startPositionX;
            this.startPositionY = startPositionY;
        }

        /// <summary>
        /// Enter the room.
        /// </summary>
        /// <param name="player">The player that entered the room</param>
        public void Enter(ref Player player)
        {
            this.player = player;

            //Move the player to the start position.
            this.player.positionX = this.startPositionX;
            this.player.positionY = this.startPositionY;
        }

        /// <summary>
        /// Leave the room.
        /// </summary>
        public void Leave()
        {
            
        }

        /// <summary>
        /// Move the player relative to its current position.
        /// </summary>
        /// <param name="x">The relative x-position</param>
        /// <param name="y">The relative y-position</param>
        public void Move(int x, int y)
        {
            int movedX = 0;
            int movedY = 0;

            if (this.player.positionX + x < this.width && this.player.positionX + x >= 0)
            {
                movedX += x;
                this.player.positionX += x;
            }

            if (this.player.positionY + y < this.height && this.player.positionY + y >= 0)
            {
                movedY += y;
                this.player.positionY += y;
            }

            //Console.WriteLine(this.player.name.ToUpper() + " moved to square " + String.Format("{0};{1}", this.player.positionX, this.player.positionY));

            //Handle squares of interest
            var squares = this.squares.Where(sq => sq.positionX == this.player.positionX && sq.positionY == this.player.positionY);
            if (squares.Count() > 0)
            {
                //The square currently stepped on
                Square square = squares.FirstOrDefault();
                if (this.messageHandler != null)
                    this.messageHandler(square.enterString);
                if (square.item != null)
                    this.player.items.Add(square.item);
                if (square.onEntered != null)
                    square.onEntered();
                this.squares.Remove(square);
            }

            //Handle exits
            var exits = this.exits.Where(sq => sq.positionX == this.player.positionX && sq.positionY == this.player.positionY);
            if (exits.Count() > 0)
            {
                //The exit currently at - not yet perfected / implemented
                Square exit = exits.FirstOrDefault();
                if(this.onMoved != null)
                    this.onMoved();
                this.squares.Remove(exit);
            }
        }
    }

    /// <summary>
    /// A square of interest.
    /// </summary>
    public struct Square
    {
        /// <summary>
        ///  The optional item in the square.
        /// </summary>
        public Item item;
        /// <summary>
        /// The string displayed when the player enters the square.
        /// </summary>
        public string enterString;
        /// <summary>
        /// The position of the item within the room.
        /// </summary>
        public int positionX;
        /// <summary>
        /// The position of the item within the room.
        /// </summary>
        public int positionY;
        /// <summary>
        /// The optional method called when the player enters the square.
        /// </summary>
        public Action onEntered;
    }
}
