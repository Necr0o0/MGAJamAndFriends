using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BombController : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Zara wybuchnie");
        var sequence = DOTween.Sequence();
        sequence.AppendInterval(2f);
        sequence.OnComplete(() =>
        {
            Debug.Log("Wybuch!");
            transform.gameObject.SetActive(false);
        });
        sequence.Play();
    }


}
