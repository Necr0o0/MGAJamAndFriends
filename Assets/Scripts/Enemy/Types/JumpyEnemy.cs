using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpyEnemy : Enemy
{
    [Space]
    [SerializeField] private float waitTime = 2f;
    
    [Space]
    [SerializeField] PlayerFinder playerFinder;

    public Renderer pyraRender;
    //Color_DCC0BD5

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

    public void PyraBomb(BombController bombController)
    { 
        
        var pyraBomb = transform.GetComponent<BombController>();
        transform.GetChild(0).gameObject.SetActive(true);
        pyraBomb.enabled = true;
        
        var block = new MaterialPropertyBlock();
        
        block.SetColor("Color_ACC3A391", bombController.ammo.Color);
        
        pyraRender.SetPropertyBlock(block);
        
        pyraBomb.PyraInit(bombController.ammo);
       
       
        
    }
}
