using System;
using UnityEngine;

[Serializable]
public class PlayerFinder
{
    [SerializeField] private float sightRadius = 5f;
    [SerializeField] private float sightAngle = 90f;

    [SerializeField] private Transform tmpPlayerRef = default;
    
    public Transform Player { get; private set; }

    public bool TryToFindPlayer(Transform origin)
    {
        // some kind of Player.transform
        if (!tmpPlayerRef)
            return false;
        Transform playerTransform = tmpPlayerRef;
        Vector3 distance = playerTransform.position - origin.position;
        if (distance.magnitude > sightRadius)
            return false;
        float angle = Vector3.Angle(origin.forward, distance);
        if (Mathf.Abs(angle) > sightAngle)
            return false;

        Player = playerTransform;
        return true;
    }
}
