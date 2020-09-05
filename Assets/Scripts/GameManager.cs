using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;
    public ObjectPool bombPool;
    void Awake()
    {
        singleton = this;
    }
    
}
