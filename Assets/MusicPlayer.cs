using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MusicPlayer : MonoBehaviour
{
    public List<Slot> slots = new();
    public List<AudioClip> slotClips = new();
    public List<AudioClip> emptyCLips = new();

    private AudioClip gapClip; //Calculated upon Start
    public List<AudioClip> playingClips = new();
    private float gapLength; //Calculated upon Start

    public Slider slider;
    
    public AudioSource speaker;
    private float baseVolume = 1;//Allocated upon Start

    public int iterationIndex = -1;
    private float playbackLength;
    public UnityEvent OnRightSlot;
    public UnityEvent OnWrongSlot;
    public UnityEvent OnEmptySlot;
    public UnityEvent OnSlotComplete;
    public UnityEvent OnEvilMusican;
    public UnityEvent OnGoodMusician;
    
    private IEnumerator Coroutine;
    private void Update()
    {
    }

    public void StartSlotMusic()
    {
        StartCoroutine(Coroutine);
    }
    
    public void StopSlotMusic()
    {
        ResetMusic();
        StopCoroutine(Coroutine);
    }

    private void ResetMusic()
    {
        speaker.Stop();
        progression = 0;
        offset = 0;
        playbackLength = 0;
        playbackPosition = 0;
        iterationIndex = -1;
        speaker.timeSamples = 0;
        slider.value = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (var slot in slots)
        {
            slotClips.Add(slot.musicElements[0].clip);
        }


        //Some assign-code since I don't manually drag in the clips in the slots.

        baseVolume = speaker.volume; //Remember the base value of the speaker.
        speaker.loop = false; //We don't want to loop our clips.
        speaker.playOnAwake = false; //We don't wanna start playing when the scene is loaded.
        speaker.Stop(); //Make the speaker stop incase something turned it on at start.

        Coroutine = playMusic();
        StartCoroutine(Coroutine);
    }

    public bool stop;
    private double progression;
    private AudioClip next = null;
    private double offset;
    private float playbackPosition;
    
    private IEnumerator playMusic() {
        while (true) {
            if (!speaker.isPlaying) {
                if (iterationIndex == slots.Count - 1)
                {
                    if (playingClips.SequenceEqual(slotClips))
                    {
                        OnSlotComplete?.Invoke();
                        break;
                    }
                    playingClips.Clear();
                }

                if (iterationIndex >= 0)
                {
                    
                    if (slots[iterationIndex].currentElement != null)
                    {
                        var currentMusic = slots[iterationIndex].currentElement.Element;
                        if(slotClips[iterationIndex] != currentMusic.clip)
                        {
                            OnEvilMusican?.Invoke();
                        }
                        else
                        {
                            slots[iterationIndex].currentElement.ShakeNote();
                            OnGoodMusician?.Invoke();
                        }
                    }
                }
                
                
                
                speaker.Stop();
                iterationIndex = (iterationIndex + 1) % slots.Count;//Increment the iteration index of the playlist.
              
                var currentElement = slots[iterationIndex].currentElement;

                if (currentElement == null)
                {
                    //If no clip is present, we allocate values fit for a gap.
                    speaker.clip = emptyCLips[iterationIndex];
                    playbackLength = speaker.clip.samples;
                    OnEmptySlot?.Invoke();
                }
                else
                {
                    //If a clip is present, we allocate values of that clip for playback.
                    var currentMusic = currentElement.Element;
                    playbackLength = currentMusic.clip.samples;
                    speaker.volume = baseVolume;
                    speaker.clip = currentMusic.clip;
                    playingClips.Add(speaker.clip);
                    
                    if (slotClips[iterationIndex] == currentMusic.clip)
                    {
                        OnRightSlot?.Invoke();
                    }
                    else
                    {
                        OnWrongSlot?.Invoke();
                    }
                }
                

                speaker.Play(); //We start playing our (gap) clip.
                
                
            }
          
            playbackPosition = speaker.timeSamples / playbackLength; //Calculate the position of our clip.
            progression = (Mathf.Lerp(0f, playbackLength, playbackPosition) / playbackLength) * 0.25f; //Calculate the progression of the clip towards the end. Scaled so it can be used for our slider.
            offset = iterationIndex * 0.25f; // The offset of our slider. (Determined by how many clips we have played)
            slider.value = (float)(offset + progression); //Set the slider value to display the progression of the entire playlist.
            if (speaker.timeSamples < playbackLength) yield return new WaitForSecondsRealtime(.01f);

        }
    }

}