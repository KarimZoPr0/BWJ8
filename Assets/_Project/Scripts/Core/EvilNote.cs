using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilNote : MonoBehaviour
{
	public Transform target;
	public Projectile prefab;
	public int count;
	public float Force;
	
	[ContextMenu("Spawn EvilNote")]
   public void SpawnEvilNote()
   {

	   for (int i = 0; i < count; i++)
	   {

		   var GO = Instantiate(prefab, transform.position, Quaternion.identity);
		   GO.target = target;

		   CinemachineShake.Instance.ShakeCamera(2f,.15f);
	   }
   }
}
