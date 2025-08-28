using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Resources", menuName = "Scriptable Objects/Resources")]
public class ResourcesSO : ScriptableObject
{
    [field: SerializeField] public int Amount { get; private set; }
    [field: SerializeField] public Material GridMaterial { get; private set; }
    [field: SerializeField] public GameObject Structure { get; private set; }
}
