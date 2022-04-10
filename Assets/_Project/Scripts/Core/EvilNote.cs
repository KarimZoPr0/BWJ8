using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class EvilNote : MonoBehaviour
{
	public Transform target;
	public Projectile prefab;
	public int count;
	public bool usePool;
	public float delay;

	private ObjectPool<Projectile> _pool;

	private void Start()
	{
		_pool = new ObjectPool<Projectile>(() => Instantiate(prefab, transform.position, Quaternion.identity), projectile =>
			{
				projectile.gameObject.SetActive(true);

			}, projectile =>
			{
				projectile.gameObject.SetActive(false);
			}, projectile =>
			{
				Destroy(projectile.gameObject);
			}, false, 10,20);
		
	}


	[ContextMenu("Spawn EvilNote")]
   public void StartSpawning()
   {
	   StartCoroutine(SpawnEvilNote());
   }


   public IEnumerator SpawnEvilNote()
   {
	   for (int i = 0; i < count; i++)
	   {
		   var GO = usePool ? _pool.Get() : Instantiate(prefab, transform.position, Quaternion.identity);
		   GO.target = target;

		   CinemachineShake.Instance.ShakeCamera(8,.3f); 
		   KillProjectile(GO);

		   yield return new WaitForSeconds(delay);
	   }
   }

   private void KillProjectile(Projectile GO)
   {
	  if(usePool) _pool.Release(GO);
	  else Destroy(GO.gameObject, 5f);
   }
}
