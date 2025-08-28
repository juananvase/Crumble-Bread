using System;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [field: SerializeField] public bool Init { get; set; } = false;

    public void ChangeMaterial(Material material)
    {
        GetComponentInChildren<MeshRenderer>().material = material;
    }
}

public enum GridType
{
    Grass,
    Milk,
    Fruit,
    Egg,
    Wheat,
    City,
    Port,
    Path
}
