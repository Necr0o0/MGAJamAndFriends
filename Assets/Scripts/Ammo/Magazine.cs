using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Magazine : MonoBehaviour
{
    [SerializeField] private RawImage image = default;
    [SerializeField] private AmmoColorPallette pallette = default;
    [SerializeField] private ImageLoader imageLoader = default;
    [SerializeField] private Vector2Int size = new Vector2Int(8, 8);
    public int MagazineSize => size.x * size.y;

    private Texture2D texture;
    private List<Ammo> magazine = new List<Ammo>();
    private int currentIndex;

    private void Start()
    {
        LoadNewTexture();
        texture.filterMode = FilterMode.Point;
        texture.Apply();
        image.texture = texture;
    }

    public Ammo Shoot()
    {
        Ammo ret = magazine[currentIndex];
        magazine[currentIndex] = null;
        int x = currentIndex % size.x;
        int y = size.y - 1 - currentIndex / size.y;
        texture.SetPixel(x, y, FadeColor(ret.Color));
        texture.Apply();
        image.texture = texture;
        currentIndex++;
        return ret;
    }

    private void LoadNewTexture()
    {
        Texture2D newTexture = imageLoader.GetRandomTexture();
        
        texture = new Texture2D(newTexture.width, newTexture.height, newTexture.format, false);
        Graphics.CopyTexture(newTexture, texture);
        
        size = new Vector2Int(newTexture.width, newTexture.height);
        magazine.Clear();
        currentIndex = 0;

        for (int y = size.y - 1; y >= 0; y--)
        {
            for (int x = 0; x < size.x; x++)
            {
                magazine.Add(pallette.GetAmmoFromColor(texture.GetPixel(x, y)));
            }
        }
    }
    
    private void GenerateTexture()
    {
        magazine.Clear();
        currentIndex = 0;
        texture = new Texture2D(size.x, size.y);
        for (int y = size.y -1 ; y >= 0; y--)
        {
            for (int x = 0; x < size.x; x++)
            {
                Ammo randomAmmo = RandomAmmo();
                magazine.Add(randomAmmo);
                texture.SetPixel(x, y, randomAmmo.Color);
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
        return Color.black;
    }
}
