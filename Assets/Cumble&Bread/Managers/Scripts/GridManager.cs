using System;using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GridManager : MonoBehaviour
{
    [Header("Grid settings")]
    [SerializeField] private int _gridSize = 1;
    [SerializeField] private GameObject _hexPrefab;
    [SerializeField] private GameObject _map;
    private Hex[] _grid;
    
    [Header("Resources settings")]
    [SerializeField] private ResourcesSO[] _resources;
    [SerializeField] private ResourcesSO _ports;

    private void Start()
    {
        LayoutGrid();
        _grid = _map.transform.GetComponentsInChildren<Hex>();
        GenerateRandomTerrain();
    }

    private void LayoutGrid()
    {
        for (int q = -_gridSize; q <= _gridSize; q++)
        {
            for (int r = -_gridSize; r <= _gridSize; r++)
            {
                int s = -q - r;
                if(Mathf.Abs(s) > _gridSize) continue;
                if(q == r && Mathf.Abs(q) >= _gridSize) continue;
                   
                //Debug.Log($"q{q}, r{r}, s{s}");
                
                GameObject hex = Instantiate(_hexPrefab, _map.transform);
                hex.name = $"q{q}, r{r}, s{-q-r}";
                hex.transform.position = GetHexPositionFromCoordinate(new Vector2Int(q, r));
            }
        }
    }

    private Vector3 GetHexPositionFromCoordinate(Vector2Int coordinate)
    {
        int column = coordinate.x;
        int row = coordinate.y;

        float size = 0.5f;
        float width = Mathf.Sqrt(3) * size;
        float height = 2 * size;
        
        float xPosition;
        float yPosition;
        float horizontalDistance;
        float verticalDistance;
        
        bool shouldOffset;
        float offset;
        
        shouldOffset = row != 0;

        horizontalDistance = width;
        verticalDistance = height * (3f/4f);

        offset = (shouldOffset) ? width / 2f : 0;
        
        xPosition = (column * horizontalDistance) + (row * offset);
        yPosition = (row * verticalDistance);
        
        return new Vector3(xPosition, 0, -yPosition);
    }

    private void GenerateRandomTerrain()
    {
        GeneratePorts();

        for (int i = 0; i < _resources.Length; i++)
        {
            GenerateRandomResurce(_resources[i]);
        }
    }

    private void GeneratePorts()
    {
        string PortNorth = $"q{(int)(_gridSize/2f)}, r{-_gridSize}, s{(int)(-(_gridSize/2f) + _gridSize)}";
        string PortSouth = $"q{(int)(-(_gridSize/2f))}, r{_gridSize}, s{(int)((_gridSize/2f) - _gridSize)}";
        string PortEast = $"q{_gridSize}, r{0f}, s{-_gridSize}";
        string PortWest = $"q{-_gridSize}, r{0f}, s{_gridSize}";

        int count = 0;

        for (int i = 0; i < _grid.Length; i++)
        {
            if(count >= 4) break;
            
            if (_grid[i].gameObject.name == PortNorth)
            {
                _grid[i].ChangeMaterial(_ports.GridMaterial);
                _grid[i].Init = true;
                count++;
                continue;
            }
            
            if (_grid[i].gameObject.name == PortSouth)
            {
                _grid[i].ChangeMaterial(_ports.GridMaterial);
                _grid[i].Init = true;
                count++;
                continue;
            }
            
            if (_grid[i].gameObject.name == PortEast)
            {
                _grid[i].ChangeMaterial(_ports.GridMaterial);
                _grid[i].Init = true;
                count++;
                continue;
            }
            
            if (_grid[i].gameObject.name == PortWest)
            {
                _grid[i].ChangeMaterial(_ports.GridMaterial);
                _grid[i].Init = true;
                count++;
                continue;
            }
        }
    }

    private void GenerateRandomResurce(ResourcesSO resource)
    {
        for (int i = 0; i < resource.GenerationAmount; i++)
        {
            Hex selectedHex = null;
            do
            {
                selectedHex = _grid[Random.Range(0, _grid.Length)];
                
            } while (selectedHex.Init == true);
    
            selectedHex.ChangeMaterial(resource.GridMaterial);
            selectedHex.Init = true;
        }
    }
}
