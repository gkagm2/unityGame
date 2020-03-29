using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMovement : MonoBehaviour
{
    public enum ECameraMode
    {
        Camera3D,
        Camera2D
    }
    public Transform yawPivot; // left, right
    public Transform pitchPivot; // up, down
    
    [HideInInspector]
    public GameObject modelObj;
    public ECameraMode eCamMode;
    
    // camera rotate
    public float minAngleX;
    public float maxAngleX;
    
    public float distance2D;

    public float moveSpeed = 5f;
    public float rotSpeed = 5f;
    public float wheelSpeed;

    private Vector3 prev3DPosition;
    private Quaternion prev3DRotation;
    private Quaternion prev3DYawRotation;
    private Quaternion prev3DPitchRotation;

    private Camera cam;
    private GameObject clickedObject;
    private bool isObjectClicked;

    private float mouseX;
    private float mouseY;

    // double click
    private bool isClick;
    private float clickCoolTime;
    private float clickCount;

    private float xRotation = 0f;
    private float yRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();

        transform.LookAt(yawPivot);
        isObjectClicked = false;
        prev3DPosition = transform.position;
        prev3DRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        Wheel();

        if (eCamMode == ECameraMode.Camera2D)
        {
            switch (ModelManager.instance.mode)
            {
                case EMode.normal:
                    // Move
                    if (Input.GetMouseButton(1))
                    {
                        Move2D();
                    }

                    if (Input.GetMouseButtonDown(0))
                    {
                        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit))
                        {
                            if (hit.collider.gameObject.tag.Equals("Model")) // 모델을 클릭했을 경우.
                            {
                                isClick = true;
                                ++clickCount;
                                if (clickCount >= 2)
                                {
                                    isObjectClicked = true;
                                    clickedObject = hit.collider.gameObject;
                                }
                            }
                        }
                    }

                    if (Input.GetMouseButton(0))
                    {
                        if (isObjectClicked == true)
                        {
                            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                            RaycastHit hit;
                            if (Physics.Raycast(ray, out hit))
                            {
                                if (hit.collider.gameObject.name.Equals("Plane")) // Plane을 클랙했을 경우.
                                {
                                    if (clickedObject == null)
                                    {
                                        Debug.LogError("clickedObject가 널이네 ");
                                        break;
                                    }
                                    clickedObject.transform.parent.position = hit.point;
                                }
                            }
                        }
                    }

                    if (Input.GetMouseButtonUp(0))
                    {
                        if (isObjectClicked == true)
                        {
                            isObjectClicked = false;
                            clickedObject = null;
                        }
                    }
                    break;

                case EMode.modelCreate:
                    if (Input.GetMouseButtonDown(0))
                    {
                        CreateModel();
                    }
                    break;
            }
            if (Input.GetMouseButton(1))
            {
                Move2D();
            }
        }
        else if (eCamMode == ECameraMode.Camera3D) // 3d
        {
            if (Input.GetMouseButton(0))
                Move3D();

            if (Input.GetMouseButton(1))
                Rotate3D();
        }

        // double click
        if (isClick == true)
        {
            clickCoolTime += Time.deltaTime;
            if (clickCoolTime >= 0.7f)
            {
                clickCount = 0;
                clickCoolTime = 0;
                isClick = false;
            }
        }
    }

    // Common
    private void Wheel()
    {
        float mouseScroll = Input.GetAxis("Mouse ScrollWheel") * wheelSpeed;

        if (cam.fieldOfView <= 20.0f && mouseScroll < 0) // 줄임
        {
            cam.fieldOfView = 20.0f;
        }
        else if (cam.fieldOfView >= 60.0f && mouseScroll > 0)
        {
            cam.fieldOfView = 60.0f;
        }
        else
        {
            cam.fieldOfView += mouseScroll;
        }
    }

    // 2D mode
    public void Move2D()
    {
        mouseX = Input.GetAxis("Mouse X") * moveSpeed * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * moveSpeed * Time.deltaTime;
        yawPivot.Translate(new Vector3(-mouseX, 0f, -mouseY), Space.Self);
    }

    private void CreateModel()
    {
        if (EventSystem.current.IsPointerOverGameObject()) // UI 클릭 시
            return;
        Ray ray0 = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray0, out hit, Mathf.Infinity))
        {
            
            if(hit.collider.name == "Plane")
            {
                if(modelObj == null)
                {
                    Debug.LogWarning("오브젝트 설정이 안됨.");
                    return;
                }
                Instantiate(modelObj, hit.point, Quaternion.LookRotation(hit.point));
            }
        }
        ModelManager.instance.mode = EMode.normal;
    }
    
    // 3D mode
    private void Move3D()
    {
        mouseX = Input.GetAxis("Mouse X") * moveSpeed * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * moveSpeed * Time.deltaTime;
        yawPivot.Translate(new Vector3(-mouseX, 0f, -mouseY), Space.Self);
    }

    private void Rotate3D()
    {
        yRotation += Input.GetAxis("Mouse X") * rotSpeed * Time.deltaTime;
        xRotation += Input.GetAxis("Mouse Y") * rotSpeed * Time.deltaTime;
        if (xRotation < minAngleX)
            xRotation = minAngleX;
        else if (xRotation > maxAngleX)
            xRotation = maxAngleX;
        yawPivot.rotation = Quaternion.Euler(0, transform.rotation.y + yRotation, 0);
        pitchPivot.rotation = Quaternion.Euler(transform.rotation.x + xRotation, transform.rotation.y + yRotation, 0);
    }

    public void OnClick_ChangeCamera3DBtn()
    {
        eCamMode = ECameraMode.Camera3D;

        yawPivot.rotation = prev3DYawRotation;
        pitchPivot.rotation = prev3DPitchRotation;
        transform.localPosition = prev3DPosition;
        transform.localRotation = prev3DRotation;
    }

    public void OnClick_ChangeCamera2DBtn()
    {
        eCamMode = ECameraMode.Camera2D;

        prev3DYawRotation = yawPivot.rotation;
        prev3DPitchRotation = pitchPivot.rotation;

        yawPivot.rotation = Quaternion.Euler(0, 0, 0);
        pitchPivot.rotation = Quaternion.Euler(0, 0, 0);
        // 카메라 전환
        transform.position = new Vector3(yawPivot.position.x, distance2D, yawPivot.position.z);
        transform.rotation = Quaternion.Euler(90, 0, 0);
    }
}