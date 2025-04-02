using UnityEngine;
using System.Collections.Generic;

public class BackgroundMusicController : MonoBehaviour
{
    public List<AudioClip> BackgroundAudioClips;
    private AudioSource audioSource;
    private int currentAudioIndex;
    public bool isPlaying;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayBackgroundBeat() 
    {
        if (BeatManager.Instance.beatCounter % 2 == 0 && isPlaying) 
        {
            // Play the current clip and loop to the next
            audioSource.PlayOneShot(BackgroundAudioClips[currentAudioIndex]);

            // Increment index and loop back when reaching the end
            currentAudioIndex = (currentAudioIndex + 1) % BackgroundAudioClips.Count;
        }
    }
}
