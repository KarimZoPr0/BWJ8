using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RoboRyanTron.Unite2017.Elements;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;


public enum Direction
{
   Up,
   Down,
   Left,
   Right
}

public class EvilBlock : MonoBehaviour, ISpawnAble
{
   public GameObject prefab;
   public List<BoxCollider2D> blocks;

   public SlotController slotController;

   public Transform parent;
   private void Awake()
   {
      foreach (Transform child in parent)
      {
         blocks.Add(child.GetComponent<BoxCollider2D>());
          
      }
   }

   private void Update()
   {
   }
   
   public void Spawn()
   {
     
      foreach (var block in blocks.Where(x => x.GetComponent<Elemental>().isUsed == false))
      {
         var randomDirection = Random.Range(0, 4);
         Direction direction = (Direction) randomDirection;

         var blockSize = block.size;

         var blockPosition = block.transform.position;

         const float gap = 0.2f;
         blockPosition = direction switch
         {
            Direction.Up => new Vector3(blockPosition.x, blockPosition.y + (gap + blockSize.y)),
            Direction.Down => new Vector3(blockPosition.x, blockPosition.y - (gap + blockSize.y)),
            Direction.Left => new Vector3(blockPosition.x - (gap + blockSize.x), blockPosition.y),
            Direction.Right => new Vector3(blockPosition.x + (gap + blockSize.x), blockPosition.y),
            _ => blockPosition
         };

         var GO = Instantiate(prefab, blockPosition, Quaternion.identity);
         
         CinemachineShake.Instance.ShakeCamera(2f,.15f);
         StartCoroutine(DestroyGO());

         IEnumerator DestroyGO()
         {
            yield return new WaitForSeconds(5f);
            Destroy(GO);
         }

      }
   }
}