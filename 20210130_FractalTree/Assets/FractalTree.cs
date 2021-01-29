using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FractalTree : MonoBehaviour
{
    public bool bDraw = true;

    public void DrawFixedView(Vector3 curPos, int order, float length, float angle)
    {
        Vector3 dvec = Vector3.zero;
        dvec.x = length * Mathf.Sin(angle);
        dvec.y = length * Mathf.Cos(angle);
        Debug.DrawLine(curPos, curPos + dvec);
        if (order > 0)
        {
            DrawFixedView(curPos + dvec, order - 1, length * 0.8f, angle - 0.5f);
            DrawFixedView(curPos + dvec, order - 1, length * 0.8f, angle + 0.5f);
        } 
    }

    public void DrawRandomTreeView(Vector3 curPos, int order, float length, float angle)
    {
        Vector3 dvec = Vector3.zero;
        dvec.x = length * Mathf.Sin(angle);
        dvec.y = length * Mathf.Cos(angle);
        Debug.DrawLine(curPos, curPos + dvec);

        if (order > 0)
        {
            DrawFixedView(curPos + dvec, order - 1, length * Random.Range(0.5f, 0.8f), angle - Random.Range(0.0f, 1.0f));
            DrawFixedView(curPos + dvec, order - 1, length * Random.Range(0.5f, 0.8f), angle + Random.Range(0.0f, 1.0f));
        }
    }

    private void OnDrawGizmos()
    {
        if (bDraw)
        {
            // 고정된 값
            DrawFixedView(new Vector3(-500, 0, 0), 8, 202, 0);

            // 랜덤 값
            DrawRandomTreeView(new Vector3(500, 0, 0), 8, 202, 0);
            bDraw = false;
        }
    }
}