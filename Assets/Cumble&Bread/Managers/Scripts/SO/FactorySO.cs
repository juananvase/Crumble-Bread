using UnityEngine;

[CreateAssetMenu(fileName = "Factory", menuName = "Scriptable Objects/Factory")]
public class FactorySO : ScriptableObject
{
    [field: SerializeField] public int InventorySpace { get; private set; }
    [field: SerializeField] public int Utility { get; private set; }
    
}
