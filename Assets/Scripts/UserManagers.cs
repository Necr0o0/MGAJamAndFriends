using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UserManagers : MonoBehaviour
{
    private List<PlayerController> players = new List<PlayerController>();

    public void NewPlayer(PlayerInput input)
    {
        players.Add(input.gameObject.GetComponent<PlayerController>());
        RecalculateScreen();
    }

    public void RemovePlayer(PlayerInput input)
    {
        players.Remove(input.gameObject.GetComponent<PlayerController>());
        RecalculateScreen();
    }

    void RecalculateScreen()
    {
        foreach (PlayerController player in players)
        {
            player.ChangeRect(players.Count);
        }
    }
}
