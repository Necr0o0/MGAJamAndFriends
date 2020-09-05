using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "My/Color Palette")]
public class AmmoColorPallette : ScriptableObject
{
    public List<Ammo> allAmmo;

    public Ammo GetAmmoFromColor(Color color)
    {
        return allAmmo.FirstOrDefault(x => x.Color == color);
    }
}
