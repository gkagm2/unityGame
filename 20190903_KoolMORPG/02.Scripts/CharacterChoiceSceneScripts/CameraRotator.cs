using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    public float angle = 90;                // 회전 시킬 각도
    public float rotationSpeed = 10.0f;     // 회전 속도

    private float wantedRotationAngle = 0;  // 원하는 회전 각도
    private float currentRotationAngle;     // 현재 회전 각도
    private void Start()
    {
        transform.eulerAngles = Vector3.zero;
    }
    // Update is called once per frame
    void Update()
    {
        // 카메라를 부드럽게 이동시키기 위해 사용. Mathf.LerpAngle(현재 오일러 각, 원하는 오일러 각, 시간)
        currentRotationAngle = Mathf.LerpAngle(transform.eulerAngles.y, wantedRotationAngle, rotationSpeed * Time.deltaTime);
        //Debug.Log("current Rotation Angle : " + currentRotationAngle);

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, currentRotationAngle, transform.eulerAngles.z);
    }

    /// <summary>
    /// 왼쪽으로 회전한다.
    /// </summary>
    public void RotateLeft()
    {
        wantedRotationAngle = transform.eulerAngles.y + angle;
    }
    /// <summary>
    /// 오른쪽으로 회전한다.
    /// </summary>
    public void RotateRight()
    {
        wantedRotationAngle = transform.eulerAngles.y - angle;
    }
}