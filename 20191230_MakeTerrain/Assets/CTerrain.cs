using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTerrain : MonoBehaviour
{
    public int xSize = 4;
    public int zSize = 4;


    private Mesh mesh;
    private CTerrainCollider terrainCollider;

    private Vector3[] vertices;
    private int[] triangles;
    
    private float height;
    void Start()
    {
        mesh = new Mesh();
        mesh.name = "CTerrainMesh";
        GetComponent<MeshFilter>().mesh = mesh;
        
        vertices = new Vector3[xSize * zSize];
        
        DrawMesh();
        UpdateMesh();

        terrainCollider.DrawCollider(xSize, zSize);
    }

    void Update()
    {
        //DrawMesh();
        //UpdateMesh();
    }

    public void RaiseTerrainMesh(float range)
    {

    }
    
    private void DrawMesh()
    {
        for (int x = 0; x < zSize; ++x)
        {
            for (int z = 0; z < zSize; ++z)
            {
                vertices[(xSize * x) + z] = new Vector3(x, 0, z);
            }
        }

        
        triangles = new int[zSize * xSize * 6];

        int tris = 0;
        int _x;

        for (int xVertex = 0; xVertex < xSize - 1; ++xVertex) {
            _x = xVertex * xSize;
            for (int zVertex = 0; zVertex < zSize - 1; ++zVertex)
            {
                triangles[tris + 0] = _x + zVertex + 0;
                triangles[tris + 1] = _x + zVertex + 1;
                triangles[tris + 2] = _x + zVertex + zSize;
                triangles[tris + 3] = _x + zVertex + zSize;
                triangles[tris + 4] = _x + zVertex + 1;
                triangles[tris + 5] = _x + zVertex + 1 + zSize;

                tris += 6;
                UpdateMesh();
            }
        }
    }

    private void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }

    // 랜덤 높이 세팅
    private void SetRandomHeight(float _randomOffset = 2f)
    {
        float currentVerticesY;

        for (int x = 0; x < zSize; ++x)
        {
            for (int z = 0; z < zSize; ++z)
            {
                currentVerticesY = vertices[(xSize * x) + z].y;
                vertices[(xSize * x) + z].y = Random.Range(currentVerticesY, currentVerticesY + _randomOffset);
            }
        }
    }
    
    //private void OnDrawGizmos()
    //{
    //    if (vertices == null)
    //    {
    //        Gizmos.color = Color.red;
    //        for (int x = 0; x < xSize; ++x)
    //        {
    //            for (int z = 0; z < zSize; ++z)
    //            {

    //                Gizmos.DrawSphere(new Vector3(x, 0, z), .1f);
    //            }
    //        }
    //        return;
    //    }

    //    Gizmos.color = Color.gray;

    //    for (int i= 0; i < vertices.Length; ++i)
    //    {
    //        Gizmos.DrawSphere(vertices[i], .1f);
    //    }
    //}
}


public class CTerrainCollider
{
    public Vector3[] vertices;
    public int[] triangles;

    public void DrawCollider(int xSize, int zSize)
    {
        for (int x = 0; x < zSize; ++x)
        {
            for (int z = 0; z < zSize; ++z)
            {
                vertices[(xSize * x) + z] = new Vector3(x, 0, z);
            }
        }
    }
}