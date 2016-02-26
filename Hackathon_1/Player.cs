using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon_1
{
    public class Player
    {
        public string name;
        public List<Item> items;
        public int positionX = 0;
        public int positionY = 0;
        public int health = 100;

        public Player(string name, List<Item> items)
        {
            this.name = name;
            this.items = items;
        }
    }
}
