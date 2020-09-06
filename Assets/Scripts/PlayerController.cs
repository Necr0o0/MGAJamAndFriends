using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public WeaponController weapon;
    [SerializeField] private PlayerInput _playerInput;

    [Space]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lerpIntensity = 3f;
    [SerializeField] private float maxVelocity = 20f;
    [SerializeField] private float jumpPower = 10f;
    private bool isGrounded;
    
    public static Transform TransformComponent { get; private set; }
    
    private Vector2 moveCamera;
    private Rigidbody rb;
    private Gamepad gamepad;
    private new Transform camera;
    private new Transform transform;
    private Vector3 inputMove = Vector2.zero;
    private Vector3 moveDir = Vector2.zero;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        transform = ((Component) this).transform;
        TransformComponent = transform;
        camera = transform.GetChild(0);
        gamepad = Gamepad.current;
        GameManager.singleton.joinedPlayers++;
    }

    private void OnCollisionEnter(Collision other)
    {
        isGrounded = true;
    }

    void Update()
    {
        if (gamepad == null)
        {
            return; // No gamepad connected.
        }

        if (gamepad.rightTrigger.wasPressedThisFrame)
        {
           weapon.Shoot();
           camera.DOShakePosition(0.1f, 0.6f , 0, 0);

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

        Vector2 input = gamepad.leftStick.ReadValue();
        inputMove = (input.x * transform.right + input.y * transform.forward).normalized;
    }

    private void FixedUpdate()
    {
        moveDir = Vector3.Lerp(moveDir, inputMove, Time.deltaTime * lerpIntensity);
        
        rb.MovePosition(transform.position + moveDir * (Time.fixedDeltaTime * speed));

        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity) + moveDir * (Time.fixedDeltaTime * speed);
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
    
    void ScreenShake(float distance)
    {
        float maxRadius = 30f;
        var str = maxRadius / distance;
        str = Mathf.Clamp(str, 0.5f, 2);

        camera.DOShakePosition(0.2f, 1f * str, 30, 10);
    }

    public void ChangeRect(int allPlayers)
    {
        if (!camera)
            camera = GetComponent<Transform>().GetChild(0);
        Camera cameraComponent = camera.GetComponent<Camera>();
        Rect cameraRect = new Rect();
        cameraRect.width = 1;
        if (allPlayers == 1)
        {
            cameraRect.height = 1;
        }

        if (allPlayers == 2)
        {
            cameraRect.height = 0.5f;
            if (_playerInput.playerIndex == 1)
                cameraRect.y = 0.5f;
        }

        cameraComponent.rect = cameraRect;
    }
}
