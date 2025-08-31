using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class HexRenderer : MonoBehaviour
{
    private Mesh _mesh;
    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;
    
    private List<Face> _faces;
    
    [SerializeField] public Material material;
    [SerializeField] public float innerSize;
    [SerializeField] public float outerSize;
    [SerializeField] public float height;
    [SerializeField] public bool isFlatTopped;
    
    private void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>(); 
        _meshRenderer = GetComponent<MeshRenderer>();
        
        _mesh = new Mesh();
        _mesh.name = "Hex";
        
        _meshFilter.mesh = _mesh;
        _meshRenderer.material = material;
    }

    private void OnEnable()
    {
        DrawMesh();
    }

    private void OnValidate()
    {
        if(Application.isPlaying) DrawMesh();
    }

    private void DrawMesh()
    {
        DrawFaces();
        CombineFaces();
    }
    
    private void DrawFaces()
    {
        _faces = new List<Face>();
        
        //top faces
        for (int point = 0; point < 6; point++)
        {
            _faces.Add(CreateFace(innerSize, outerSize, height / 2f, height / 2f, point, false));
        }
        
        //Bottom faces
        for (int point = 0; point < 6; point++)
        {
            _faces.Add(CreateFace(innerSize, outerSize, -height / 2f, -height / 2f, point, true));
        }
        
        //Outer faces
        for (int point = 0; point < 6; point++)
        {
            _faces.Add(CreateFace(outerSize, outerSize, height / 2f, -height / 2f, point, true));
        }
        
        //Inner faces
        for (int point = 0; point < 6; point++)
        {
            _faces.Add(CreateFace(innerSize, innerSize, height / 2f, -height / 2f, point, false));
        }
    }

    private Face CreateFace(float innerRad, float outerRad, float heightA, float heightB, int point, bool reverse = false)
    {
        Vector3 pointA = GetPoint(innerRad, heightB, point);
        Vector3 pointB = GetPoint(innerRad, heightB, (point < 5) ? point + 1 : 0);
        Vector3 pointC = GetPoint(outerRad, heightA, (point < 5) ? point + 1 : 0);
        Vector3 pointD = GetPoint(outerRad, heightA, point);
        
        List<Vector3> vertices = new List<Vector3>() {pointA, pointB, pointC, pointD};
        List<int> triangles = new List<int>() {0,1,2,2,3,0};
        List<Vector2> uvs = new List<Vector2>() {new Vector2(0,0), new Vector2(1,0), new Vector2(1,1), new Vector2(0,1)};

        if (reverse)
        {
            vertices.Reverse();
        }

        return new Face(vertices, triangles, uvs);
    }

    protected Vector3 GetPoint(float size, float height, int index)
    {
        float angle_deg = isFlatTopped ? 60 * index: 60 * index-30;
        float angle_rad = Mathf.PI / 180f * angle_deg;
        return new Vector3((size * Mathf.Cos(angle_rad)), height, (size * Mathf.Sin(angle_rad)));
    }

    private void CombineFaces()
    {
        List<Vector3> vertices = new List<Vector3>();
        List<int> tris = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        for (int i = 0; i < _faces.Count; i++)
        {
            //Add vertices
            vertices.AddRange(_faces[i].Vertices);
            uvs.AddRange(_faces[i].UVs);
            
            //Offset the triangles
            int offset = (4 * i);
            foreach (int triangles in _faces[i].Triangles)
            {
                tris.Add(triangles + offset);
            }
        }
        
        _mesh.vertices = vertices.ToArray();
        _mesh.triangles = tris.ToArray();
        _mesh.uv = uvs.ToArray();
        _mesh.RecalculateNormals();
    }
    
}

public struct Face
{
    public List<Vector3> Vertices { get; private set; }
    public List<int> Triangles { get; private set; }
    public List<Vector2> UVs { get; private set; }

    public Face(List<Vector3> vertices, List<int> triangles, List<Vector2> uvs)
    {
        this.Vertices = vertices;
        this.Triangles = triangles;
        this.UVs = uvs;
    }
}
