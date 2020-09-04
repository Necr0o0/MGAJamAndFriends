using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    void FixedUpdate()
    {
        var gamepad = Gamepad.current;
        if (gamepad == null)
        {
            Debug.Log("No Controller");
            return; // No gamepad connected.
        }

        var mouse = Mouse.current;
        
        if (mouse == null)
        {
            Debug.Log("No Mouse!");
            return; // No gamepad connected.
        }   

        if (gamepad.rightTrigger.wasPressedThisFrame)
        {
            Debug.Log("Shoot!");
        }

        Vector2 move = gamepad.leftStick.ReadValue();
        // 'Move' code here
        transform.GetComponent<Rigidbody>().velocity = move;

    }
}
