using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCube : MonoBehaviour
{

    protected MeshFilter meshFilter;
    protected MeshRenderer meshRenderer;

    private Mesh mesh;

    private Vector3[] vertices;
    private int[] triangles;

    // Start is called before the first frame update
    void Start()
    {
        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshRenderer = gameObject.AddComponent<MeshRenderer>();

        mesh = new Mesh();
        meshFilter.mesh = mesh;

        mesh.name = "CubeMesh";
        vertices = GenerateVertices(); //vertices 생성
        triangles = GenerateTriangles(); // triangles 생성

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

    }

    private void Update()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }

    /// <summary>
    /// Cube vertices
    /// </summary>
    /// <returns>new vertices</returns>
    private Vector3[] GenerateVertices()
    {

        // 8개의 vertice가 필요
        Vector3[] newVerts = new Vector3[]
        {
            new Vector3(-.5f,-.5f,-.5f), // 0
            new Vector3(-.5f, .5f,-.5f), // 1
            new Vector3( .5f, .5f,-.5f), // 2
            new Vector3( .5f,-.5f,-.5f), // 3
            new Vector3(-.5f,-.5f, .5f), // 4
            new Vector3(-.5f, .5f, .5f), // 5
            new Vector3( .5f, .5f, .5f), // 6
            new Vector3( .5f,-.5f, .5f), // 7
        };
        return newVerts;
    }

    /// <summary>
    /// Cube triangles
    /// </summary>
    /// <returns>new triangles</returns>
    private int[] GenerateTriangles()
    {
        int[] newTriangles = new int[3*12]
        {
            // left
            4,5,0,
            0,5,1,

            //right
            3,2,7,
            7,2,6,

            // bottom
            4,0,7,
            7,0,3,

            // top
            1,5,2,
            2,5,6,

            // front
            0,1,3,
            3,1,2,

            // back
            5,4,6,
            6,4,7,
        };
        return newTriangles;
    }
}
