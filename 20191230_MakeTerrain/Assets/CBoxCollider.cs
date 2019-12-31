using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBoxCollider : MonoBehaviour
{
    [SerializeField]
    protected bool isTrigger;
    Material material;

    public Vector3 center;
    public float radius;

    protected Vector3[] vertices;

    private Vector3 pivot;
    private Vector3[] offsetVec;

    // Start is called before the first frame update
    void Start()
    {
        pivot = transform.position;
        offsetVec = new Vector3[]
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


        vertices = GenerateVertices();
    }

    // Update is called once per frame
    void Update()
    {
        pivot = transform.position;

        DrawLine();
    }

    private void DrawLine()
    {
        
        // Draw collider line
        Debug.DrawLine(transform.position - offsetVec[0], transform.position -offsetVec[1]);
        Debug.DrawLine(transform.position - offsetVec[1], transform.position -offsetVec[2]);
        Debug.DrawLine(transform.position - offsetVec[2], transform.position -offsetVec[3]);
        Debug.DrawLine(transform.position - offsetVec[3], transform.position -offsetVec[0]);

        Debug.DrawLine(transform.position - offsetVec[0], transform.position -offsetVec[4]);
        Debug.DrawLine(transform.position - offsetVec[1], transform.position -offsetVec[5]);
        Debug.DrawLine(transform.position - offsetVec[2], transform.position -offsetVec[6]);
        Debug.DrawLine(transform.position - offsetVec[3], transform.position -offsetVec[7]);

        Debug.DrawLine(transform.position - offsetVec[4], transform.position -offsetVec[5]);
        Debug.DrawLine(transform.position - offsetVec[5], transform.position -offsetVec[6]);
        Debug.DrawLine(transform.position - offsetVec[6], transform.position -offsetVec[7]);
        Debug.DrawLine(transform.position - offsetVec[7], transform.position -offsetVec[4]);
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
            new Vector3(offsetVec[0].x + pivot.x, offsetVec[0].y, offsetVec[0].z + pivot.z), // 0
            new Vector3(offsetVec[1].x + pivot.x, offsetVec[1].y, offsetVec[1].z + pivot.z), // 1
            new Vector3(offsetVec[2].x + pivot.x, offsetVec[2].y, offsetVec[2].z + pivot.z), // 2
            new Vector3(offsetVec[3].x + pivot.x, offsetVec[3].y, offsetVec[3].z + pivot.z), // 3
            new Vector3(offsetVec[4].x + pivot.x, offsetVec[4].y, offsetVec[4].z + pivot.z), // 4
            new Vector3(offsetVec[5].x + pivot.x, offsetVec[5].y, offsetVec[5].z + pivot.z), // 5
            new Vector3(offsetVec[6].x + pivot.x, offsetVec[6].y, offsetVec[6].z + pivot.z), // 6
            new Vector3(offsetVec[7].x + pivot.x, offsetVec[7].y, offsetVec[7].z + pivot.z), // 7
        };
        return newVerts;
    }
}