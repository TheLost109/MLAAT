using UnityEngine;

/// <summary>
/// A script that plays the intro <see cref="AudioClip"/> and, after <see cref="introToLoopDelay"/> seconds,
/// plays the loop <see cref="AudioClip"/> on repeat. To acquire these <see cref="AudioClip"/>s, I used Audacity and
/// manually cut them from a source mp3 myself. That's pretty inefficient if you have a lot of these to do, if you're
/// bad at it, or if you can't figure out Audacity. In that case, use the paid version (IntroLoop), which is much, much
/// nicer.
///
/// Based on a useful post from 5argon at:
///     https://discussions.unity.com/t/513979
/// 5argon is also the author of IntroLoop:
///     https://discussions.unity.com/t/611791
///     https://exceed7.com/introloop/
/// </summary>
public class AudioWithIntro : MonoBehaviour
{

    public AudioSource introSource, loopSource;

    public AudioClip introClip, loopClip;

    /// <summary>
    /// If true, plays the sequence of clips when the script <see cref="Awake"/>ns. Otherwise, you'll have to manually
    /// execute <see cref="Play"/> from an external source.
    /// </summary>
    public bool playOnAwake;

    /// <summary>
    /// Time in seconds between the intro and the loop. Zero should be seamless, but if there's a gap in the clips, you
    /// might want this to be negative.
    /// </summary>
    public double introToLoopDelay = 0;

    /// <summary>
    /// <see cref="Play"/>s if appropriate.
    /// </summary>
    private void Awake()
    {
        if (playOnAwake)
            Play();
    }

    /// <summary>
    /// Plays the intro <see cref="AudioClip"/> and then plays the loop <see cref="AudioClip"/> after. Delay time for
    /// the loop is introClip.Length + <see cref="introToLoopDelay"/>.
    /// </summary>
    public void Play()
    {

        // Stop previous
        introSource.Stop();
        loopSource.Stop();

        // Get timing right
        double batonPassTime = AudioSettings.dspTime + introClip.length + introToLoopDelay;

        // Play intro
        introSource.PlayOneShot(introClip);
        introSource.SetScheduledEndTime(batonPassTime);

        // Schedule loop
        loopSource.clip = loopClip;
        loopSource.loop = true;
        loopSource.PlayScheduled(batonPassTime);
    }

    public void Stop()
    {
        introSource.Stop();
        loopSource.Stop();
    }

    public void Mute()
    {
        introSource.volume = 0;
        loopSource.volume = 0;
    }

    public void UnMute()
    {
        introSource.volume = 1;
        loopSource.volume = 1;
    }
}
