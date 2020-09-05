using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;
    public ObjectPool bombPool;
    public ObjectPool explosionPool;
    public int joinedPlayers= 0;
    
    void Awake()
    {
        singleton = this;
    }

    void OnPlayerJoined()
    {
        Debug.LogWarning("PLAYER JOINED");
    }
}
