using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager
{

    #region Fields
    static bool initialized = false;
    static AudioSource audioSource;
    static Dictionary<AudioClipName, AudioClip> audioClips = new Dictionary<AudioClipName, AudioClip>();
    #endregion

    #region Properties
    /// <summary>
    /// Returns whether the AudioManager has been initialized
    /// </summary>
    public static bool Initialized
    {
        get { return initialized; }
    }
    #endregion

    #region Methods
    public static void Initialize(AudioSource source)
    {
        initialized = true;
        audioSource = source;
        audioClips.Add(AudioClipName.Ow, Resources.Load<AudioClip>(@"Audio\Ow"));
    }

    public static void Play(AudioClipName name)
    {
        audioSource.PlayOneShot(audioClips[name]);
    }

    #endregion
}
