using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
   

    // Update is called once per frame
    void Update()
    {
       if(Gamepad.current.aButton.wasPressedThisFrame) 
        {
            SceneManager.LoadScene("Level" + Random.Range(0,2));
        }
    }
}
