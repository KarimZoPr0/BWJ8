using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


[CreateAssetMenu]
public class MusicElement : ScriptableObject
{
	[Tooltip("The sound that will be played")]
	public Sprite noteSprite;
	public AudioClip clip;
}
