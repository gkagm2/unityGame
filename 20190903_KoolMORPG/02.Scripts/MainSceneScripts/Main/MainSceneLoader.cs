using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MainScene을 시작 할 때 초기화를 위한 클래스
/// </summary>
public class MainSceneLoader : MonoBehaviour
{
    public GameObject popupManager;

    public GameObject loadingPanel;
    private void Awake()
    {
        popupManager.SetActive(true);
        loadingPanel.SetActive(true);
    }
}
