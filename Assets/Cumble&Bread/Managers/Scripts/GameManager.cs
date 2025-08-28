using System;using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ResourcesSO _wheat;
    [SerializeField] private ResourcesSO _eggs;
    [SerializeField] private ResourcesSO _fruits;
    [SerializeField] private ResourcesSO _milk;
    [SerializeField] private ResourcesSO _cities;
    [SerializeField] private GameObject _map;
    [SerializeField] private Grid[] _grids;

    private void Start()
    {
        _grids = _map.transform.GetComponentsInChildren<Grid>();
        GenerateRandomTerrain();
    }

    private void GenerateRandomTerrain()
    {
        GenerateRandomResurce(_wheat);
        GenerateRandomResurce(_eggs);
        GenerateRandomResurce(_fruits);
        GenerateRandomResurce(_milk);
        GenerateRandomResurce(_cities);
    }

    private void GenerateRandomResurce(ResourcesSO resource)
    {
        for (int i = 0; i < resource.Amount; i++)
        {
            Grid selectedGrid = null;
            do
            {
                selectedGrid = _grids[Random.Range(0, _grids.Length)];
                
            } while (selectedGrid.Init == true);

            selectedGrid.ChangeMaterial(resource.GridMaterial);
            selectedGrid.Init = true;
        }
    }
}
