using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon_1
{
    public class Item
    {
        public string name;
        public string useString;
        public Action<Player> onUse;

        public Item(string name, string useString, Action<Player> onUse)
        {
            this.name = name;
            this.useString = useString;
            this.onUse = onUse;
        }
    }
}
