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

    /// <summary>
    /// The sounds currently implemented
    /// </summary>
    public enum Sound
    {
        //Background sound
        Background,
        //Movement sound
        Move
    }

    /// <summary>
    /// Class to handle sounds
    /// </summary>
    public static class Sounds
    {
        /// <summary>
        /// Play a sound.
        /// </summary>
        /// <param name="sound">The Sound.sound to be played</param>
        /// <param name="loop">Whether or not the sound should be looped.</param>
        public static void Play(Sound sound, bool loop = false)
        {
            Uri path;

            //Retrieve the path of the sound
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

            //Play the sound
            new System.Threading.Thread(() => {
                var c = new System.Windows.Media.MediaPlayer();
                c.Open(path);
                //Loop the sound
                if (loop && sound == Sound.Background)
                {
                    Task.Delay(47998).ContinueWith(_ =>
                    {
                        Play(sound, loop);
                    });
                }
                
                c.Play();
            }).Start();
        }
    }
}
