using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Magazine : MonoBehaviour
{
    [SerializeField] private Renderer weaponScreen;
    [SerializeField] private AmmoColorPallette pallette = default;
    [SerializeField] private ImageLoader imageLoader = default;
    [SerializeField] private Material bombInsideMaterial;
    
    public int MagazineSize => size.x * size.y;

    private Texture2D currentTexture;
    private Texture2D originalImage;
    private List<Ammo> magazine = new List<Ammo>();
    private Vector2Int size = new Vector2Int(8, 8);
    private int currentIndex;
    private static readonly int Texture2DB07Fc10B = Shader.PropertyToID("Texture2D_B07FC10B");

    [SerializeField] private Material weaponBombMaterial;

    public void Initialize(Action onMagazineLoaded)
    {
        imageLoader = GameManager.singleton.GetComponent<ImageLoader>();
        StartCoroutine(LoadNewTexture(new WaitForSeconds(0.05f), onMagazineLoaded));
    }

    public Ammo Shoot()
    {
        Ammo ret = magazine[currentIndex];
        magazine[currentIndex] = null;
        int x = currentIndex % size.x;
        int y = size.y - 1 - currentIndex / size.y;
        currentTexture.SetPixel(x, y, FadeColor(ret.Color));
        currentTexture.Apply();

        //SetTextureToRenderer();
        currentIndex++;
        Color color = magazine[currentIndex].Color;
        weaponBombMaterial.SetColor("Color_96BF9729",color);
        
        return ret;
    }

    public IEnumerator LoadNewTexture(YieldInstruction ammoReloadTime, Action onComplete = null)
    {
        originalImage = imageLoader.GetRandomTexture();
        size = new Vector2Int(originalImage.width, originalImage.height);
        magazine.Clear();
        currentIndex = 0;
        
        currentTexture = new Texture2D(size.x, size.y, originalImage.format, false);
        currentTexture.filterMode = FilterMode.Point;
        SetTextureToRenderer();
        

        for (int y = 0; y < size.y; y++)
        {
            for (int x = size.x - 1; x >= 0; x--)
            {
                currentTexture.SetPixel(x, y, originalImage.GetPixel(x, y));
                magazine.Insert(0, pallette.GetAmmoFromColor(currentTexture.GetPixel(x, y)));
                if (!(ammoReloadTime is null))
                {
                    currentTexture.Apply();
                    yield return ammoReloadTime;
                }
            }
        }
        
        currentTexture.Apply();
        onComplete?.Invoke();
        yield return null;
    }

    private void GenerateTexture()
    {
        magazine.Clear();
        currentIndex = 0;
        var texture2 = Resources.Load<Texture2D>("Pyrka");
        currentTexture = new Texture2D(size.x, size.y);
        for (int y = size.y -1 ; y >= 0; y--)
        {
            for (int x = 0; x < size.x; x++)
            {
                Ammo randomAmmo = RandomAmmo();
                magazine.Add(randomAmmo);
                currentTexture.SetPixel(x, y, texture2.GetPixel(x,y));
            }
        }
    }

    private Ammo RandomAmmo()
    {
        return pallette.allAmmo[Random.Range(0, pallette.allAmmo.Count)];
        //return new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }

    private Color FadeColor(Color original)
    {
        return new Color(0,0,0,0);
    }

    private void SetTextureToRenderer()
    {
        var block = new MaterialPropertyBlock();
        
        block.SetTexture(Texture2DB07Fc10B, currentTexture);
        
        weaponScreen.SetPropertyBlock(block);
    }
}
