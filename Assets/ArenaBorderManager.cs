using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArenaBorderManager : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
