/**********************
 * AudioManager.cs
 * Author: Tash
 * Created: Tuesday 5th November 2013
 * Comments: Audio manager
 * TO PLAY BACKGROUND MUSIC: AudioManager.PlayMusic("Audio\\Hitman.wav");
 * TO PLAY SOUND EFFECTS: 
 * ********************/

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

/// <summary>
/// Manager playback fo music and sound effects
/// </summary>
namespace OpenglTester
{
	public class AudioManager
	{
		static Dictionary<string, SoundEffect> sounds = new Dictionary<string, SoundEffect>(); //contianer for all game sounds
		static Song currentSong; //the current music
		static float soundVolume = 0.1f; //store the sound volume

		/// <summary>
		/// Initialize this AudioManager. (does nothing at the moment)
		/// </summary>
		public static void Initialize()
		{
		}

		/// <summary>
		/// Plays the sound with the given name
		/// </summary>
		/// <param name='name'>
		/// The name of the sound to play
		/// </param>
		public static void PlaySound(string name)
		{
			SoundEffect effect;
			if(sounds.TryGetValue(name, out effect))
			{
				effect.Play(soundVolume, 0.0f, 0.0f);
			}
		}

		/// <summary>
		/// Sets the current background music to the sound with the given name
		/// </summary>
		/// <param name='name'>
		/// The name of the music to play
		/// </param>
		public static void PlayMusic(string name)
		{
			currentSong = null;

			try
			{
				currentSong = Game1.contentManager.Load<Song>(name);
			}
			catch(Exception e)
			{//song not found
				if (currentSong == null)
					throw e;
			}
			MediaPlayer.Play(currentSong);
		}

		/// <summary>
		/// Loads a sound into the sound dictionary for playing.
		/// </summary>
		/// <param name='assetName'>
		/// The filename of the sound (eg. "Audio\\Hitman.wav") 
		/// MUST BE .WAV and MUST BE IN THE AUDIO FOLDER
		/// </param>
		public static void LoadSound(string fileName)
		{
			sounds.Add(fileName, Game1.contentManager.Load<SoundEffect>(fileName));
		}

		/// <summary>
		/// Stops the background music.
		/// </summary>
		public static void StopMusic()
		{
			MediaPlayer.Stop();
		}

		/// <summary>
		/// Sets the sound FX volume.
		/// </summary>
		/// <param name='vol'>
		/// A float volume level between 0.f and 1.f
		/// </param>
		public static void SetSoundFXVolume(float vol)
		{
			soundVolume = vol;
		}

		/// <summary>
		/// Sets the music volume.
		/// </summary>
		/// <param name='volume'>
		/// A float volume level between 0.f and 1.f
		/// </param>
		public static void SetMusicVolume(float volume)
		{
			MediaPlayer.Volume = volume;
		}
	}
}

