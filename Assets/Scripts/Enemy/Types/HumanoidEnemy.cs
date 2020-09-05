using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class HumanoidEnemy : Enemy
{
    [Space]
    [SerializeField] private float waitTime = 2f;
    [SerializeField] private float walkingTime = 3f;
    
    [Space]
    [SerializeField] PlayerFinder playerFinder;
    
    private WaitForSeconds yieldWait;
    private WaitForSeconds yieldWalk;

    protected override void Awake()
    {
        playerFinder = new PlayerFinder();

        yieldWait = new WaitForSeconds(waitTime);
        yieldWalk = new WaitForSeconds(walkingTime);
        
        base.Awake();
    }

    protected override IEnumerator Movement()
    {
        while (playerFinder.Player is null)
        {
            movementToDo = null;
            yield return yieldWait;
            if (playerFinder.TryToFindPlayer(transform))
                break;
            transform.Rotate(Vector3.up, Random.Range(-200, 200), Space.Self);
            movementToDo = MoveWithoutTarget;
            yield return yieldWalk;
            if (playerFinder.TryToFindPlayer(transform))
                break;
        }

        movementToDo = MoveToTarget;
    }

    protected override void MoveWithoutTarget()
    {
        rb.MovePosition(transform.position + transform.forward * (Time.fixedDeltaTime * speed));
    }

    protected override void MoveToTarget()
    {
        Vector3 distance = transform.position - playerFinder.Player.position;
        Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(distance.normalized), Time.deltaTime * 10f);
        rb.MovePosition(transform.position - distance.normalized * (speed * Time.fixedDeltaTime));
    }
}
