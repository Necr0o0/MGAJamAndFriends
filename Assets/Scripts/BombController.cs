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
        rb.velocity = Vector3.zero;
        transform.DORestart();
        transform.DOScale(1.5f, 0.2f).SetLoops(-1, LoopType.Yoyo);
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
                
                collider.SendMessage("Explosion",SendMessageOptions.DontRequireReceiver);
                collider.SendMessage("ScreenShake",SendMessageOptions.DontRequireReceiver);

            }
            foreach (var collider in  Physics.OverlapSphere(transform.position, knockbackRadius*10))
            {
             
                collider.SendMessage("ScreenShake",   Vector3.Magnitude(transform.position - collider.transform.position),SendMessageOptions.DontRequireReceiver);
            }

            GameManager.singleton.explosionPool.GetObject(transform.position);
            transform.gameObject.SetActive(false);
        });
        sequence.Play();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position,knockbackRadius);
    }
}
