using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;
    public ObjectPool bombPool;
    public ObjectPool explosionPool;
    
    void Awake()
    {
        singleton = this;
    }
}
