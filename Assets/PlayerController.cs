using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private float minX = 60f;
    private float maxX = 60f;
    private Vector2 moveCamera;
    private Rigidbody rb;
    private Gamepad gamepad;
    private Transform camera;
    private Transform player;
    public WeaponController weapon;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        camera = transform.GetChild(0);
        player = transform;
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
        
        moveCamera += gamepad.rightStick.ReadValue();
        moveCamera.y = Mathf.Clamp(moveCamera.y, -60f, 60f);
        
        
        camera.localEulerAngles = new Vector3(-moveCamera.y,0,0);
        player.localEulerAngles = new Vector3(0,moveCamera.x,0);

        Vector2 move = gamepad.leftStick.ReadValue();
        var moveDir = (move.x * player.right + move.y * player.forward).normalized;
        rb.velocity = moveDir * 10f ;
      


    }
}
