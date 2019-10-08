using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    public Transform player = null;
    public Vector3 camPosition;
    public Vector3 camRotation;

    public float camPositionX;
    public float camPositionZ;
    public float camShakingRange;
    public float maxRange = 0.5f;
    public float minRange = -0.5f;
    public float shakeTime = 0.2f;
    public float timer;

    public bool isShaking = false;

    private Transform tr;

    void Start()
    {
        tr = GetComponent<Transform>();
    }
    
    void Update()
    {
        CameraView();
        CameraShaking();
    }

    public void FindPlayer()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    public void CameraView()
    {
        if (player == null) return;
        tr.position = player.position + camPosition;
        tr.rotation = Quaternion.Euler(camRotation);
    }

    public void CameraShaking() //TODO 백종규 : 카메라 쉐이킹
    {
        //if (!isShaking) return;
        //Vector3 curCamPoisition = transform.position;

        //camShakingRange = Random.Range(minRange, maxRange);

        //if(isShaking)
        //{
        //    transform.position = new Vector3(transform.position.x + camShakingRange, transform.position.y + camShakingRange, transform.position.z + camShakingRange);
        //}

        if(isShaking)
        {
            timer += Time.deltaTime;
            camShakingRange = Random.Range(minRange, maxRange);
            tr.position = new Vector3(tr.position.x + camShakingRange, tr.position.y + camShakingRange, tr.position.z + camShakingRange);
            if(timer >= shakeTime)
            {
                timer = 0f;
                isShaking = false;
            }
        }
    }

    public IEnumerator ShakeProcess()
    {
        isShaking = true;
        yield return new WaitForSeconds(shakeTime);
        isShaking = false;
    }
}
