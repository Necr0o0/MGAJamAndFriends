using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private float initialLife = 100;
    
    [Space]
    [SerializeField] protected float speed = 2f;

    private float _life;
    public float Life
    {
        get => _life;
        private set
        {
            _life = value;
            if (_life < 0)
                Destroy();
        }
    }
    
    protected Rigidbody rb;
    protected new Transform transform;
    protected Action movementToDo;

    protected virtual void Awake()
    {
        _life = initialLife;
        
        rb = GetComponent<Rigidbody>();
        transform = GetComponent<Transform>();
        
        StartCoroutine(Movement());
    }
    
    private void FixedUpdate()
    {
        movementToDo?.Invoke();
    }

    protected abstract IEnumerator Movement();

    protected abstract void MoveWithoutTarget();
    
    protected abstract void MoveToTarget();

    private void DealDamage(float damage)
    {
        Life -= damage;
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
