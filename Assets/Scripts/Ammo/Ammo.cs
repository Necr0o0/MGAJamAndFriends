using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My/Ammo")]
public class Ammo : ScriptableObject
{
    [SerializeField] private Color color = default;
    public Color Color => color;
    public List<AmmoEvent> ammoEvents = default;
    public List<AmmoEvent> AmmoEvents => ammoEvents;
}
