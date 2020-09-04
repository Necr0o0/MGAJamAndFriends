using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    void Update()
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
        Debug.Log(move);
        // 'Move' code here
        transform.GetComponent<Rigidbody>().velocity = new Vector3(move.x,0.0f,move.y) * 10f;

        Vector2 moveCamera = gamepad.rightStick.ReadValue();
        
        transform.GetChild(0).transform.localEulerAngles += new Vector3(-moveCamera.y,moveCamera.x);


    }
}
