using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My/Ammo")]
public class Ammo : ScriptableObject
{
    [SerializeField] private Color color;
    public Color Color => color;

    [SerializeField] private List<AmmoEvent> ammoEvents;
    public List<AmmoEvent> AmmoEvents => ammoEvents;
}
