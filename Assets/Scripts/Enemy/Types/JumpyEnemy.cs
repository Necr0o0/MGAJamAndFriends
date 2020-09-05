using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpyEnemy : Enemy
{
    [Space]
    [SerializeField] private float waitTime = 2f;
    
    [Space]
    [SerializeField] PlayerFinder playerFinder;

    private WaitForSeconds yieldWait;

    protected override void Awake()
    {
        playerFinder = new PlayerFinder();

        yieldWait = new WaitForSeconds(waitTime);

        base.Awake();
    }

    protected override IEnumerator Movement()
    {
        while (true)
        {
            yield return yieldWait;
            if (playerFinder.TryToFindPlayer(transform))
                break;
            else
                MoveWithoutTarget();
        }

        while (true)
        {
            MoveToTarget();
            yield return yieldWait;
        }
    }

    protected override void MoveWithoutTarget()
    {
        Vector2 randPos = Random.insideUnitCircle;
        transform.rotation = Quaternion.LookRotation(new Vector3(randPos.x, 0, randPos.y));
        
        Jump();
    }

    protected override void MoveToTarget()
    {
        Vector3 distance = playerFinder.Player.position - transform.position;
        distance.y = 0;
        transform.rotation = Quaternion.LookRotation(distance);
        
        Jump();
    }

    private void Jump()
    {
        rb.AddForce((Vector3.up + transform.forward) * speed, ForceMode.Impulse);
    }
}
