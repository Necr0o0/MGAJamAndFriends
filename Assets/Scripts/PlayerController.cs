using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public WeaponController weapon;

    [Space]
    [SerializeField] private float maxVelocity = 20f;
    [SerializeField] private float jumpPower = 10f;
    
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

        if (gamepad.aButton.wasPressedThisFrame)
        {
            rb.velocity += Vector3.up * jumpPower;
        }
        
        moveCamera += gamepad.rightStick.ReadValue();
        moveCamera.y = Mathf.Clamp(moveCamera.y, -60f, 60f);
        
        
        camera.localEulerAngles = new Vector3(-moveCamera.y,0,0);
        transform.localEulerAngles = new Vector3(0,moveCamera.x,0);

        Vector2 move = gamepad.leftStick.ReadValue();
        var moveDir = (move.x * transform.right + move.y * transform.forward).normalized;
        rb.velocity = moveDir * 10f;
    }
}
