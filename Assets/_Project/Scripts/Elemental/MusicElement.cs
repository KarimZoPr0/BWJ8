using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class MusicElement : ScriptableObject
{
	[Tooltip("The sound that will be played")]
	public AudioClip clip;
}
