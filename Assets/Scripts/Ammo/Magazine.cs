﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using UnityEngine.InputSystem;

public class Magazine : MonoBehaviour
{
    [SerializeField] private RawImage image = default;
    [SerializeField] private AmmoColorPallette pallette = default;
    [SerializeField] private Vector2Int size = new Vector2Int(8, 8);
    
    private Texture2D texture;
    public List<Ammo> magazine = new List<Ammo>();
    private int currentIndex;

    private void Awake()
    {
        GenerateTexture();
        texture.filterMode = FilterMode.Point;
        texture.Apply();
        image.texture = texture;
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
            Shoot();
    }

    public Ammo Shoot()
    {
        Ammo ret = magazine[currentIndex];
        magazine[currentIndex] = null;
        texture.SetPixel(currentIndex%size.x, size.y - 1 - currentIndex / size.x, Color.clear);
        texture.Apply();
        currentIndex++;
        return ret;
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
}
