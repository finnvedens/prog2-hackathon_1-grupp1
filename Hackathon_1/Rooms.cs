using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon_1
{
    static class Rooms
    {
        public static Room ThreeDooredRoom = new Room("Three doored room", 8, 8, 0, 3, new List<Square>()
        {
            new Square()
            {
                positionX = 1,
                positionY = 2,
                enterString = "You have stumbled upon a dead body. It smells familiar. It smells like cat."
            }, new Square()
            {
                positionX = 5,
                positionY = 1,
                enterString = "You have found a torch. Congratufuckinglations. You must feel important now.",
                item = new Item("Torch", "You lit up the room. The torched burned down and hurt you.", (Player player) =>
                {
                    player.health -= 25;
                })
            }, new Square()
            {
                positionX = 2,
                positionY = 6,
                enterString = "You found a rock. It is grey and looks like a rock. It was tasteless.",
                item = new Item("Rock", "You threw away the rock. It bounced back right at you.", (Player player) =>
                {
                    player.health -= 2;
                })
            }, new Square()
            {
                positionX = 4,
                positionY = 7,
                enterString = "You tripped on a barrel containing dolls. Three seconds later you were all done and so where they."
            }
        });
    }
}
