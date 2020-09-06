using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CheckForPlayers : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (InputDevice.all.Count > 1)
        {
            Debug.LogWarning(InputDevice.all.Count);
            SceneManager.LoadScene("Level1");
        }
    }
}
