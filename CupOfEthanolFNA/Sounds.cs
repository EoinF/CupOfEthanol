using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System.Linq;
using Microsoft.Xna.Framework;

namespace LackingPlatforms
{
    public static class Sounds
    {
        private static Dictionary<String, SoundEffect> sounds;
        private static Dictionary<String, Song> songs;

        public static void LoadSounds(ContentManager Content)
        {
			sounds = new Dictionary<string, SoundEffect>();
			sounds.Add("footstep_dirta", Content.Load<SoundEffect>(@"Sounds\footstep_dirta"));
			sounds.Add("footstep_dirtb", Content.Load<SoundEffect>(@"Sounds\footstep_dirtb"));
			sounds.Add("footstep_dirtc", Content.Load<SoundEffect>(@"Sounds\footstep_dirtc"));
			sounds.Add("player_jump", Content.Load<SoundEffect>(@"Sounds\player_jump"));
			sounds.Add("collect_coaster", Content.Load<SoundEffect>(@"Sounds\collect_coaster"));
			sounds.Add("collect_key", Content.Load<SoundEffect>(@"Sounds\collect_key"));

			sounds.Add("jump_evil", Content.Load<SoundEffect>(@"Sounds\jump_evil"));
			sounds.Add("lazer", Content.Load<SoundEffect>(@"Sounds\lazer"));
			sounds.Add("hurt_robot", Content.Load<SoundEffect>(@"Sounds\hurt_robot"));

			songs = new Dictionary<string, Song>();

			songs.Add("bensound-acousticbreeze", Content.Load<Song>(@"Sounds\bensound-acousticbreeze.ogg"));
			songs.Add("bensound-pianomoment", Content.Load<Song>(@"Sounds\bensound-pianomoment.ogg"));
			songs.Add("bensound-scifi", Content.Load<Song>(@"Sounds\bensound-scifi.ogg"));
			songs.Add("bensound-november", Content.Load<Song>(@"Sounds\bensound-november.ogg"));
			songs.Add("bensound-dance", Content.Load<Song>(@"Sounds\bensound-dance.ogg"));
			songs.Add("bensound-adventure", Content.Load<Song>(@"Sounds\bensound-adventure.ogg"));
		}

        public static void Play(string soundName)
        {
            sounds[soundName].Play();
        }

        /// <summary>
        /// Checks if the sound is within range before playing it and also pans it.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pan"></param>
        public static void Play(string soundName, Vector2 soundOrigin)
		{
			float volume = Math.Max(1 - (Vector2.Distance(soundOrigin, PPlayer.Player.sqobject.Position) / 600f), 0);
			float pan = (soundOrigin.X - PPlayer.Player.sqobject.Position.X) / 500f;

			if (pan <= 1 && pan >= -1)
                sounds[soundName].Play(volume, 0, pan);
        }

        /// <summary>
        /// plays a random sound in the list
        /// </summary>
        /// <param name="index"></param>
        public static void Play(string[] soundNames)
        {
            Random r = new Random();
            int i = r.Next(0, soundNames.Length - 1);
            sounds[soundNames[i]].Play();
        }

		public static void PlayBGM(string songName)
		{
			Song song;
			bool isSongFound = songs.TryGetValue(songName, out song);

			if (isSongFound)
			{
				MediaPlayer.Play(song);
			}
			else
			{
				// No song found, play random song
				PlayBGM(MainMethod.rand.Next(0, songs.Count - 1));
			}
			MediaPlayer.Volume = 0.1f;
			MediaPlayer.IsRepeating = true;

		}

		public static void PlayBGM(int index)
        {
			MediaPlayer.Play(songs.Values.ToArray()[index]);
			MediaPlayer.Volume = 0.2f;
			MediaPlayer.IsRepeating = true;
        }

        public static void StopBGM()
		{
			MediaPlayer.Stop();
		}
    }
}
