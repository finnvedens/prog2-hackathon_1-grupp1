using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
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
        public static void Play(Sound sound, bool loop = false)
        {
            Uri path;

            switch (sound)
            {
                case Sound.Background:
                    path = (new System.Uri(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"Resources/background.wav")));
                    break;
                case Sound.Move:
                    path = (new System.Uri(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"Resources/move.wav")));
                    break;
                default:
                    return;
            }

            new System.Threading.Thread(() => {
                var c = new System.Windows.Media.MediaPlayer();
                c.Open(path);
                if (loop)
                {
                    c.MediaEnded += (object sender, EventArgs e) =>
                    {
                        System.Windows.Media.MediaPlayer player = sender as System.Windows.Media.MediaPlayer;
                        if (player == null)
                            return;

                        player.Position = new TimeSpan(0);
                        player.Play();
                    };
                }
                
                c.Play();
            }).Start();
        }
    }
}
