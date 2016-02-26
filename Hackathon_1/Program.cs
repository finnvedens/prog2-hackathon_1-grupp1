using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon_1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Setup Console Window
            Console.SetBufferSize(120, 58);
            Console.SetWindowSize(120, 58);
            Console.Title = "Dragon's Quest Ultra HD 2016";

            Art.WriteCenter(Art.Title);

            Art.WriteAnimatedCenter("The boy who lost his cat.", 1000, () =>
            {
                Console.WriteLine();
                Art.WriteAnimatedCenter("You lost your cat and you ran after it.", 1000, () =>
                {
                    Art.WriteAnimatedCenter("It ran towards a castle and you fell through a trapdoor.", 1000, () =>
                    {
                        Art.WriteAnimatedCenter("When you got up you saw nothing but darkness.", 1000, () =>
                        {
                            Art.WriteAnimatedCenter("You must find your cat.", 1000, () =>
                            {
                                Art.WriteAnimatedCenter("It is here where your story begins.", 1000, () =>
                                {
                                    Console.WriteLine("\n\n\n");
                                    Art.WriteAnimatedCenter("PRESS ENTER TO BEGIN", 1000, () =>
                                    {

                                    }, true);
                                });
                            });
                        });

                    });
                });
            });

            Console.ReadKey();
        }
    }
}
