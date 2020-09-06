using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    void Explosion()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(transform.localScale * Random.Range(1, 5), Random.Range(0.1f, 0.6f)));
        sequence.AppendInterval(Random.Range(0.1f, 0.5f));
        sequence.Append(  transform.DOScale(Vector3.zero, Random.Range(0.1f, 0.5f)).SetEase(Ease.InOutCubic));
        sequence.OnComplete(() =>
        {
            transform.gameObject.SetActive(false);
        });
    }
}
