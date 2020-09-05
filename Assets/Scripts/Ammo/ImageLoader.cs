using System.Collections.Generic;
using UnityEngine;

public class ImageLoader : MonoBehaviour
{
    private List<Texture2D> textures = new List<Texture2D>();

    private void Awake()
    {
        LoadFromResources();
    }

    private void LoadFromResources()
    {
        foreach (Texture2D tex in Resources.LoadAll("Images/"))
        {
            textures.Add(tex);
        }
    }

    public Texture2D GetRandomTexture()
    {
        return textures[Random.Range(0, textures.Count)];
    }
}
