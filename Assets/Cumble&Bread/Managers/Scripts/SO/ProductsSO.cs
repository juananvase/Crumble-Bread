using UnityEngine;

[CreateAssetMenu(fileName = "Products", menuName = "Scriptable Objects/Products")]
public class ProductsSO : ScriptableObject
{
    [field: SerializeField] public int Price { get; private set; }
    [field: SerializeField] public GameObject[] Recipe { get; private set; }
}
