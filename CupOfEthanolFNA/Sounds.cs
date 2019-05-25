using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace LackingPlatforms
{
    public static class Sounds
    {
        private static SoundEffect[] sounds;
        private static Song[] songs;
        public const int numberOfSongs = 4;

        public static void LoadSounds(ContentManager Content)
        {
            sounds = new SoundEffect[]
            {
                Content.Load<SoundEffect>(@"Sounds\footstep_dirta"),
                Content.Load<SoundEffect>(@"Sounds\footstep_dirtb"),
                Content.Load<SoundEffect>(@"Sounds\footstep_dirtc"),
                Content.Load<SoundEffect>(@"Sounds\player_jump"),
                Content.Load<SoundEffect>(@"Sounds\lazer"),
                Content.Load<SoundEffect>(@"Sounds\coaster_collect")
            };

            songs = new Song[]
            {
                //Content.Load<Song>(@"Sounds\music_a"),
                //Content.Load<Song>(@"Sounds\music_b"),
                //Content.Load<Song>(@"Sounds\music_c"),
                //Content.Load<Song>(@"Sounds\music_d")
            };

            MediaPlayer.IsShuffled = true;
            

        }

        public static void Play(int index)
        {
            sounds[index].Play();
        }

        /// <summary>
        /// Checks if the sound is within range before playing it and also pans it.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pan"></param>
        public static void Play(int index, float pan)
        {
            if (pan > 1 || pan < -1)
                sounds[index].Play(1, 0, pan);
        }

        /// <summary>
        /// plays a random sound in the list
        /// </summary>
        /// <param name="index"></param>
        public static void Play(int[] index)
        {
            Random r = new Random();
            int i = r.Next(0, index.Length - 1);
            sounds[index[i]].Play();
        }

        public static void PlayBGM(int index)
        {
            //MediaPlayer.Play(songs[index]);
        }

        public static void StopBGM()
        {
            //MediaPlayer.Stop();
        }
    }
}
