using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBox : MonoBehaviour
{
    private Mesh mesh;

    private Vector3[] vertices;
    private int[] triangles;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        mesh.name = "CBoxMesh";

        vertices = new Vector3[8];

        DrawMesh();
        UpdateMesh();

        
    }

    private void DrawMesh()
    {
        
       
    }

    private void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMesh();
    }
}
