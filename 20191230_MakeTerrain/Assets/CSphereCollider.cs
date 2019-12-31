using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSphereCollider : MonoBehaviour
{
    public float fScale =.5f;

    private Vector3 pivot;

    public List<Vector3> vertices;
    public float radius = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        vertices = new List<Vector3>();
        float heading;
        for(int a =0; a <360; a += 360 / 30)
        {
            heading = a * Mathf.Deg2Rad;
            vertices.Add(new Vector3(Mathf.Cos(heading) * radius, Mathf.Sin(heading) * this.radius, transform.position.z));

        }
        for(int i=0;i< vertices.Count -1; ++i)
        {
            Debug.DrawLine(vertices[i], vertices[i + 1]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < vertices.Count - 1; ++i)
        {
            Debug.DrawLine(vertices[i], vertices[i + 1]);
        }
    }
    private void OnDrawGizmos()
    {

    }
}
