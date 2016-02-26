using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon_1
{

    public enum Sound
    {
        Background,
        Move
    }

    public static class Sounds
    {
        public static void Play(Sound sound)
        {
            SoundPlayer player;
            switch (sound)
            {
                case Sound.Background:
                    player = new SoundPlayer(Hackathon_1.Properties.Resources.background);
                    break;
                case Sound.Move:
                    player = new SoundPlayer(Hackathon_1.Properties.Resources.move);
                    break;
                default:
                    return;
            }
            /*var player = new System.Windows.Media.MediaPlayer();
            player.Open(new Uri(label51.Text));
            if (loopPlayer)
                player.MediaEnded += MediaPlayer_Loop;
            player.Play();*/
            player.Play();
        }

        public static void Loop(Sound sound)
        {
            SoundPlayer player;
            switch (sound)
            {
                case Sound.Background:
                    player = new SoundPlayer(Hackathon_1.Properties.Resources.background);
                    break;
                case Sound.Move:
                    player = new SoundPlayer(Hackathon_1.Properties.Resources.move);
                    break;
                default:
                    return;
            }
            player.PlayLooping();
        }
    }
}
