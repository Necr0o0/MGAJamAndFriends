using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public WeaponController weapon;

    [Space]
    [SerializeField] private float maxVelocity = 20f;
    [SerializeField] private float jumpPower = 10f;
    private bool isGrounded;
    
    public static Transform TransformComponent { get; private set; }
    
    private Vector2 moveCamera;
    private Rigidbody rb;
    private Gamepad gamepad;
    private new Transform camera;
    private new Transform transform;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        transform = ((Component) this).transform;
        TransformComponent = transform;
        camera = transform.GetChild(0);
    }

    private void OnCollisionEnter(Collision other)
    {
        isGrounded = true;
    }

    void Update()
    {
        gamepad = Gamepad.current;
        if (gamepad == null)
        {
            return; // No gamepad connected.
        }

        if (gamepad.rightTrigger.wasPressedThisFrame)
        {
           weapon.Shoot();
        }

        if (gamepad.aButton.wasPressedThisFrame && isGrounded)
        {
            rb.velocity += Vector3.up * jumpPower;
            isGrounded = false;
        }
        
        moveCamera += gamepad.rightStick.ReadValue();
        moveCamera.y = Mathf.Clamp(moveCamera.y, -60f, 60f);
        
        
        camera.localEulerAngles = new Vector3(-moveCamera.y,0,0);
        transform.localEulerAngles = new Vector3(0,moveCamera.x,0);

        Vector2 move = gamepad.leftStick.ReadValue();
        var moveDir = (move.x * transform.right + move.y * transform.forward).normalized;
        rb.AddForce( moveDir*0.1f ,ForceMode.VelocityChange);
        
        rb.AddForce(Vector3.down *2f);

        rb.velocity = Vector3.ClampMagnitude(rb.velocity, 10f);
    }

    void Explosion()
    {
        transform.GetComponent<PlayerController>().enabled = false;
        var sequnenceLost = DOTween.Sequence();
        sequnenceLost.AppendInterval(1f);
        sequnenceLost.OnComplete(() =>
        { 
            transform.GetComponent<PlayerController>().enabled = true;
        });
    }
    
    void ScreenShake()
    {
        camera.DOShakePosition(0.2f, 1f, 30, 10);
    }
}
