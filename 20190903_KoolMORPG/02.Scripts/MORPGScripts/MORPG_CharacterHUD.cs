using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MORPG_CharacterHUD : MonoBehaviour
{
    [HideInInspector]
    public Text nameText;

    private Transform mainCameraTransform; // main camera
    // Start is called before the first frame update
    void Start()
    {
        mainCameraTransform = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
        nameText.text = PlayerInformation.userData.name;
    }

    // Update is called once per frame
    void Update()
    {
        if (mainCameraTransform)
        {
            Vector3 lookAtPosition = new Vector3(mainCameraTransform.position.x, mainCameraTransform.position.y, mainCameraTransform.position.z);
            transform.LookAt(lookAtPosition);
            transform.Rotate(new Vector3(0f, 180f, 0));
        }
    }
}
