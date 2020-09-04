using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private float minX = 60f;
    private float maxX = 60f;
    Vector2 moveCamera = new Vector2();

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

     
        
        
        moveCamera += gamepad.rightStick.ReadValue();
        moveCamera.y = Mathf.Clamp(moveCamera.y, -60f, 60f);
        
        
        transform.GetChild(0).transform.localEulerAngles = new Vector3(-moveCamera.y,0,0);
        transform.localEulerAngles = new Vector3(0,moveCamera.x,0);

        Vector2 move = gamepad.leftStick.ReadValue();
        Debug.Log(move);
        // 'Move' code here
        var moveDir = (move.x * transform.right + move.y * transform.forward).normalized;
        transform.GetComponent<Rigidbody>().velocity = moveDir * 10f ;
      


    }
}
