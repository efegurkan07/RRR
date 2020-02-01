using System;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public enum CollectibleColor
    {
        RED,
        BLUE,
        YELLOW
    }

    [SerializeField]
    private CollectibleColor color;

    public CollectibleColor Color
    {
        get => color;
        set => color = value;
    }

    private void OnTriggerEnter(Collider other)
    {
        throw new NotImplementedException();
    }
}
