using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class BombController : MonoBehaviour
{
    [SerializeField]
    private float knockbackPower = 500f;
    [SerializeField]
    private float knockbackRadius = 3f;
    void OnEnable()
    {
        var rb = transform.GetComponent<Rigidbody>();
        float randX = Random.Range(-1f, 1f);
        float randY = Random.Range(-1f, 1f);
        float randZ = Random.Range(-1f, 1f);

        rb.angularVelocity = new Vector3(randX, randY, randZ) *100f;
        rb.AddForce(transform.GetChild(0).forward * 1000f);
        
        
        
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
