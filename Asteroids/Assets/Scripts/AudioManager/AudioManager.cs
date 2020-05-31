using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This class manages the audio for the game
/// </summary>
public static class AudioManager
{

    #region Fields
    static bool initialized = false;
    static AudioSource audioSource;
    static Dictionary<AudioClipName, AudioClip> audioClips = new Dictionary<AudioClipName, AudioClip>();
    static bool audio = true;
    #endregion

    #region Properties
    /// <summary>
    /// Returns whether the AudioManager has been initialized
    /// </summary>
    public static bool Initialized
    {
        get { return initialized; }
    }

    /// <summary>
    /// Returns whether audio has been enabled via the button
    /// </summary>
    public static bool IsSound
    {
        get { return audio; }
    }

    #endregion

    #region Methods
    /// <summary>
    /// Initializes the Audio Manager with various Audio clips from the enumeration
    /// </summary>
    /// <param name="source"></param>
    public static void Initialize(AudioSource source)
    {
        audio = true;
        initialized = true;
        audioSource = source;
        audioClips.Add(AudioClipName.LaserGun, Resources.Load<AudioClip>(@"Audio\LaserGun"));
        audioClips.Add(AudioClipName.GameOver, Resources.Load<AudioClip>(@"Audio\GameOver"));
        audioClips.Add(AudioClipName.AsteroidDeath, Resources.Load<AudioClip>(@"Audio\AsteroidDeath"));
        audioClips.Add(AudioClipName.LevelUp, Resources.Load<AudioClip>(@"Audio\LevelUp"));  
        audioClips.Add(AudioClipName.Click, Resources.Load<AudioClip>(@"Audio\Click"));  
    }

    /// <summary>
    /// Plays an Audio clip
    /// </summary>
    /// <param name="name"></param>
    public static void Play(AudioClipName name)
    {
        if (audio)
        {
            audioSource.PlayOneShot(audioClips[name]);
        }
    }

    /// <summary>
    /// Public method to toggle audio on or off via the button
    /// </summary>
    /// <param name="mute"></param>
    public static void ChangeAudioToMute(bool mute)
    {
        if (mute)
        {
            audio = false;
        } else
        {
            audio = true;
        }
    }

    #endregion

}
