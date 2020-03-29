using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelManager : MonoBehaviour
{
 
    public static ModelManager instance;
    public EMode mode;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    public GameObject model;
    private CameraMovement cam;
    
    public void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<CameraMovement>();
    }

    public void OnClick_ModelCreateBtn()
    {
        mode = EMode.modelCreate;
        cam.modelObj = model; 
    }
}
public enum EMode
{
    normal,
    modelCreate
}