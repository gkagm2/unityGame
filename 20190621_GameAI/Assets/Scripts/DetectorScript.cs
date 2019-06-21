using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorScript : MonoBehaviour {
    [Tooltip("감지 대상의 Object")]
    [SerializeField] GameObject[] targetObj;

    public bool isDetect = false;
    public GameObject detectedTargetObj = null;

    private void LateUpdate()
    {
        isDetect = false;
        
    }

    private void OnTriggerStay(Collider other)
    {
        for (int i = 0; i < targetObj.Length; ++i)
        {
            if (other.gameObject.Equals(targetObj[i]))
            {
                detectedTargetObj = other.gameObject;
                isDetect = true;
                Debug.Log("(trigger) serached target : " + targetObj[i].name);
            }

        }
    }
}
