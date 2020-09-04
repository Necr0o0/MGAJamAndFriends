using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BombController : MonoBehaviour
{
    [SerializeField]
    private float knockbackPower = 500f;
    [SerializeField]
    private float knockbackRadius = 3f;
    void Start()
    {
        Debug.Log("Zara wybuchnie");
        var sequence = DOTween.Sequence();
        sequence.AppendInterval(2f);
        sequence.OnComplete(() =>
        {
            Debug.Log("Wybuch!");


            foreach (var collider in  Physics.OverlapSphere(transform.position, knockbackRadius))
            {
                Debug.Log("odrzut!");
                if(collider.GetComponent<Rigidbody>())
                    collider.transform.GetComponent<Rigidbody>().AddForce((collider.transform.position- transform.position) * knockbackPower);

                if (collider.transform.GetComponent<PlayerController>())
                {
                    collider.transform.GetComponent<PlayerController>().enabled = false;
                    var sequnenceLost = DOTween.Sequence();
                    sequnenceLost.AppendInterval(1f);
                    sequnenceLost.OnComplete(() =>
                    {
                        collider.transform.GetComponent<PlayerController>().enabled = true;

                    });
                }
                  
            }
            transform.gameObject.SetActive(false);
        });
        sequence.Play();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position,knockbackRadius);
    }
}
