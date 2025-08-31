using System;
using UnityEngine;

public class Hex : MonoBehaviour
{
    [field: SerializeField] public bool Init { get; set; } = false;
    
    [SerializeField] private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    public void ChangeMaterial(Material material)
    {
        _meshRenderer.material = material;
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
