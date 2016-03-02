using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon_1
{
    public class Player
    {
        /// <summary>
        /// The in game name of the player.
        /// </summary>
        public string name;
        /// <summary>
        /// The inventory of the player.
        /// </summary>
        public List<Item> items;
        /// <summary>
        /// The current position of the player within the current room.
        /// </summary>
        public int positionX = 0;
        /// <summary>
        /// The current position of the player within the current room.
        /// </summary>
        public int positionY = 0;
        /// <summary>
        /// The current health of the player
        /// </summary>
        public int health = 100;

        /// <summary>
        /// Create a new player
        /// </summary>
        /// <param name="name">The name of the player</param>
        /// <param name="items">The player's inventory</param>
        public Player(string name, List<Item> items)
        {
            this.name = name;
            this.items = items;
        }
    }
}
