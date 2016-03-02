using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon_1
{
    public class Item
    {
        /// <summary>
        /// The name of the item.
        /// </summary>
        public string name;
        /// <summary>
        /// The string displayed when the item is used.
        /// </summary>
        public string useString;
        /// <summary>
        /// The method called when the item is used.
        /// </summary>
        public Action<Player> onUse;

        /// <summary>
        /// Create a new item.
        /// </summary>
        /// <param name="name">The name of the item</param>
        /// <param name="useString">The string displayed when the item is used</param>
        /// <param name="onUse">The method called when the item is used</param>
        public Item(string name, string useString, Action<Player> onUse)
        {
            this.name = name;
            this.useString = useString;
            this.onUse = onUse;
        }
    }
}
