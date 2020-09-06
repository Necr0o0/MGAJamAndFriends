using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ArenaBorderManager : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            Debug.Log(other.gameObject.GetComponent<PlayerInput>().playerIndex);
            SceneManager.LoadScene("GameOver");
        }
    }
}
