using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class BombController : MonoBehaviour
{
    [SerializeField] private new Renderer renderer = default;
    
    [Space]
    [SerializeField] private float knockbackRadius = 3f;

    private Ammo ammo;
    private static readonly int BaseColor = Shader.PropertyToID("Color_F3AAB12B");

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
        
        var sequence = DOTween.Sequence();
        sequence.AppendInterval(2f);
        sequence.OnComplete(Boom);
        sequence.Play();
    }

    public void Initialize(Ammo ammo)
    {
        this.ammo = ammo;
        var block = new MaterialPropertyBlock();
        
        block.SetColor(BaseColor, ammo.Color);
        
        renderer.SetPropertyBlock(block);
    }

    private void Boom()
    {
        AmmoActions.UseEvents(transform.position, ammo.ammoEvents);
        
        foreach (var collider in  Physics.OverlapSphere(transform.position, knockbackRadius))
        {
            collider.SendMessage("Explosion",SendMessageOptions.DontRequireReceiver);
        }
        foreach (var collider in  Physics.OverlapSphere(transform.position, knockbackRadius*10))
        {
             
            collider.SendMessage("ScreenShake", Vector3.Magnitude(transform.position - collider.transform.position),SendMessageOptions.DontRequireReceiver);
        }

        GameManager.singleton.explosionPool.GetObject(transform.position);
        transform.gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position,knockbackRadius);
    }
}
