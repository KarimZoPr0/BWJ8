using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MusicPlayerTest : MonoBehaviour {
    public AudioClip[] clips; //Predefined clips for the stage.
    public AudioClip[] playlist = new AudioClip[4]; //The open slots of the timeline.

    private AudioClip gapClip; //Calculated upon Start
    private float gapLength; //Calculated upon Start

    public Slider slider;
    
    public AudioSource speaker;
    private float baseVolume; //Allocated upon Start

    private int iterationIndex = -1;
    private float playbackLength;

    // Start is called before the first frame update
    void Start()
    {
        gapClip = clips.OrderByDescending(clip => clip.samples).First();
        gapLength = (float)clips.Average(clip => clip.samples);
        if (gapLength == 0) gapLength = 2.0f;
        playbackLength = gapLength;

        slider.value = 0;

        //Some assign-code since I don't manually drag in the clips in the slots.
        playlist[0] = clips[0];
        playlist[1] = clips[1]; 
        playlist[2] = clips[2];
        playlist[3] = clips[3];

        baseVolume = speaker.volume; //Remember the base value of the speaker.
        speaker.loop = false; //We don't want to loop our clips.
        speaker.playOnAwake = false; //We don't wanna start playing when the scene is loaded.
        speaker.Stop(); //Make the speaker stop incase something turned it on at start.

        StartCoroutine(playMusic());
    }

    private IEnumerator playMusic() {
        while (true) {
            if (!speaker.isPlaying) {
                speaker.Stop();
                iterationIndex = (iterationIndex + 1) % playlist.Length;//Increment the iteration index of the playlist.
                AudioClip next = playlist[iterationIndex]; //Get the next clip to play.
                if (next == null) { //If no clip is present, we allocate values fit for a gap.
                    playbackLength = gapLength;
                    speaker.clip = gapClip;
                    speaker.volume = 0.0f;
                } else { //If a clip is present, we allocate values of that clip for playback.
                    playbackLength = next.samples;
                    speaker.clip = next;
                    speaker.volume = baseVolume;
                }
                speaker.Play(); //We start playing our (gap) clip.
            }

            float playbackPosition = speaker.timeSamples / playbackLength; //Calculate the position of our clip.
            double progression = (Mathf.Lerp(0, playbackLength, playbackPosition) / playbackLength) * 0.25f; //Calculate the progression of the clip towards the end. Scaled so it can be used for our slider.
            double offset = iterationIndex * 0.25f; // The offset of our slider. (Determined by how many clips we have played)
            slider.value = (float)(offset + progression); //Set the slider value to display the progression of the entire playlist.
            if (speaker.timeSamples >= playbackLength) { //Stop the speaker to take the gapLength into account.
                speaker.Stop();
            } else {
                yield return new WaitForSecondsRealtime(.25f); //During playback of any clip, we wait 250 milliseconds before looping again.
            }
        }
    }

}