using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTerrainMaker : MonoBehaviour
{
    public GameObject tempObj;

    private Camera cam;
    private CTerrain terrain;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Hit!");
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit))
            {
                Debug.Log("get hit");
                terrain = hit.collider.gameObject.GetComponent<CTerrain>();

                Instantiate(tempObj, hit.point, Quaternion.identity);
            }
        }
    }
}
