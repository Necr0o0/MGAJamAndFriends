using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPooling : MonoBehaviour
{
   public List<GameObject> ObjectPool;

   public GameObject GetObject()
   {
      for (int i = 0; i < ObjectPool.Count; i++)
      {
         if (!ObjectPool[i].activeInHierarchy)
         {
            return ObjectPool[i];
         }
      }
      return new GameObject("BUG");
   }
   
}
