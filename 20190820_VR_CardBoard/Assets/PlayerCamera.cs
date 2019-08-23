using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

    public GameObject rayPointer;

    RaycastHit hit;

    public void GetHitPosition()
    {
        if (Physics.Raycast(rayPointer.transform.position, rayPointer.transform.forward, out hit))
        {
            if (hit.transform.tag.Equals("Zombie"))
            {
                Vector3 hitPoint = hit.point;
                Quaternion rot = Quaternion.LookRotation(hit.normal);
                hit.transform.gameObject.GetComponent<ZombieControl>().FlashEffect(hitPoint, rot);
            }
        }
    }
}
