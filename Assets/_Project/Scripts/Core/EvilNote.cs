using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilNote : MonoBehaviour
{
	public Transform target;
	public Projectile prefab;
   public void SpawnEvilNote()
   {
       var GO = Instantiate(prefab, transform.position, Quaternion.identity);
       GO.target = target;
   }
}
