using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MORPG_PlayerViewColliderArea : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // TODO (장현명) : 총돌 테스트용
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("충돌 발생 : " + other.name);
    }
}
