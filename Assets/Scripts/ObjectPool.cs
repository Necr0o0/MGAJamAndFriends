using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject ObjectToPool = default;
    private List<GameObject> ObjectPooling = new List<GameObject>();

    public void Awake()
    {
        for (int i = 0; i< 20; i++)
        {
           var poolObj = Instantiate(ObjectToPool, transform);
           ObjectPooling.Add(poolObj);
        }
    }

    public GameObject GetObject(Vector3 position)
    {
        for (int i = 0; i < ObjectPooling.Count; i++)
        {
            if (!ObjectPooling[i].activeInHierarchy)
            {
                ObjectPooling[i].transform.position = position;
                ObjectPooling[i].transform.rotation = Quaternion.identity;
                ObjectPooling[i].SetActive(true);
                return ObjectPooling[i];
            }
        }

        return Instantiate(ObjectToPool,transform);
    }
    
    public void TurnOff(GameObject toShut)
    {
        for (int i = 0; i < ObjectPooling.Count; i++)
        {
            if (ObjectPooling[i].Equals(toShut))
            {
                ObjectPooling[i].SetActive(false);
            }
        }
    }
}
