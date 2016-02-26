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

        public Item(string name, string useString)
        {
            this.name = name;
            this.useString = useString;
        }

        public void Use()
        {
            Console.WriteLine("[" + this.name + "] " + this.useString);
        }
    }
}
